// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil.Cil;

namespace IlOptimizer.CodeAnalysis
{
    // This is a node which defines a basic block of instructions, for use in a control flow graph.

    public sealed class InstructionNode
    {
        #region Constructors
        private InstructionNode()
        {
            Children = ImmutableHashSet.Create<InstructionNode>();
            Instructions = ImmutableArray.Create<Instruction>();
            Parents = ImmutableHashSet.Create<InstructionNode>();
            Properties = new Hashtable();
        }
        #endregion

        #region Properties
        public ImmutableHashSet<InstructionNode> Children { get; private set; }

        public int InDegree
        {
            get
            {
                return Parents.Count;
            }
        }

        public ImmutableArray<Instruction> Instructions { get; private set; }

        public int OutDegree
        {
            get
            {
                return Children.Count;
            }
        }

        public ImmutableHashSet<InstructionNode> Parents { get; private set; }

        public Hashtable Properties { get; }
        #endregion

        #region Static Methods
        internal static InstructionNode Create(Instruction rootInstruction)
        {
            // This is essentially a depth-first traversal of the nodes that dynamically
            // adds the parents, children, and instructions during the traversal. We use
            // a stack-based algorithm to track pending nodes, rather than using recursion,
            // so that we can process much more complex methods.

            // We have two maps that are used to track the internal state of the graph:
            //  * instructionNodeMap: Contains a map of every instruction to the node it belongs to
            //  * firstInstructionMap: Contains a map of the first instruction for each node

            // For each non-branching instruction, we just add the next instruction to the node
            // currently being processed. But only after checking if it is the first instruction
            // of another node (any other instruction should not have been processed yet). When it
            // is the first instruction of another node, we add that node as a child of the current
            // node and move to process the next node in the stack.

            // When we reach a branching instruction we create a new node for each of the target
            // instruction(s) and push them onto the stack. Like with the non-branching instructions,
            // we check if it is the first instruction of existing node and similarly add it as a child
            // of the current node. We then move to process the other target instruction(s) for the
            // branch before finally moving to process the next node in the stack. Additionally, for
            // a node that is not already the first instruction of another node, we check if it has
            // been processed already. If so, we then move that instruction, and any subsequent
            // instructions from the existing node to a new node and make the new node a child
            // of the existing node. We finally move all children of the existing node to be children
            // of the existing node to ensure the directed links remain correct.

            Debug.Assert(rootInstruction != null);
            Debug.Assert(rootInstruction.Offset == 0);

            var rootNode = new InstructionNode();
            var instructionNodeMap = new Dictionary<Instruction, InstructionNode>();
            var firstInstructionMap = new Dictionary<Instruction, InstructionNode>();
            var pendingNodes = new Stack<InstructionNode>();

            rootNode.Instructions = rootNode.Instructions.Add(rootInstruction);

            instructionNodeMap.Add(rootInstruction, rootNode);
            firstInstructionMap.Add(rootInstruction, rootNode);

            pendingNodes.Push(rootNode);

            do
            {
                var node = pendingNodes.Pop();
                var instruction = node.Instructions.Last();

                do
                {
                    var opCode = instruction.OpCode;

                    switch (opCode.FlowControl)
                    {
                        case FlowControl.Branch:
                        {
                            // Unconditional branches specify the next instruction as their operand and
                            // will cause an unprocessed instruction to push a new node onto the stack.

                            ProcessBranchTarget((Instruction)(instruction.Operand), node);
                            instruction = null;
                            break;
                        }

                        case FlowControl.Cond_Branch:
                        {
                            if (opCode.Code == Code.Switch)
                            {
                                // Switch statements are special in that they can have more than two
                                // child nodes. We need to process each of the normal nodes plus the
                                // instruction at the next logical offset.

                                var nextInstructions = (Instruction[])(instruction.Operand);

                                foreach (var nextInstruction in nextInstructions)
                                {
                                    ProcessBranchTarget(nextInstruction, node);
                                }
                            }
                            else
                            {
                                // Conditional branches should always have two children. One child will
                                // be the target instruction if the branch is taken and the other will
                                // be the instruction at the next logical offset.

                                ProcessBranchTarget((Instruction)(instruction.Operand), node);
                            }

                            // We need to add the next logical instruction as well, for when none of the
                            // branch conditions are met. However, we want to add this as a child node and
                            // then do no further processing.

                            ProcessBranchTarget(instruction.Next, node);
                            instruction = null;
                            break;
                        }

                        case FlowControl.Meta:
                        {
                            // Meta nodes generally provide additional information to the instruction at
                            // the next logical offset. We just want to treat them as `Next` for most cases.

                            var metaCode = instruction.OpCode.Code;

                            if (metaCode != Code.Volatile)
                            {
                                throw new NotImplementedException();
                            }
                        }
                        goto case FlowControl.Next;

                        case FlowControl.Break:
                        case FlowControl.Call:
                        case FlowControl.Next:
                        {
                            // Break, Call, and Next instructions just transfer control to the instruction
                            // at the next logical offset, they continue processing on the current node.

                            instruction = instruction.Next;

                            if (ProcessInstruction(instruction, node) == false)
                            {
                                instruction = null;
                            }

                            break;
                        }

                        case FlowControl.Phi:
                            throw new NotImplementedException();

                        case FlowControl.Return:
                        case FlowControl.Throw:
                        {
                            // Return and Throw instructions terminate the sequence for the current node and
                            // do not cause any new nodes to appear on the stack.

                            instruction = null;
                            break;
                        }
                    }
                }
                while (instruction != null);
            }
            while (pendingNodes.Count != 0);

            return rootNode;

            void ProcessBranchTarget(Instruction instruction, InstructionNode currentNode)
            {
                if (firstInstructionMap.TryGetValue(instruction, out var node))
                {
                    // This is already the first instruction of a node, so we don't need
                    // to do anything and can just return.

                    Debug.Assert(instructionNodeMap.ContainsKey(instruction));
                }
                else if (instructionNodeMap.TryGetValue(instruction, out node))
                {
                    // This instruction has already been processed, but it is not the first
                    // instruction of the node it belongs to. We need to create a new node
                    // where it is the first instruction and insert it as a child of the
                    // existing node.

                    var newNode = new InstructionNode();

                    var instructionIndex = node.Instructions.IndexOf(instruction);
                    var instructionCount = (node.Instructions.Length - instructionIndex);

                    for (var index = 0; index < instructionCount; index++)
                    {
                        var existingInstruction = node.Instructions[instructionIndex];

                        Debug.Assert(newNode.Instructions.Contains(existingInstruction) == false);
                        newNode.Instructions = newNode.Instructions.Add(existingInstruction);

                        instructionNodeMap[existingInstruction] = newNode;
                    }

                    Debug.Assert(newNode.Instructions.Length == instructionCount);

                    node.Instructions = node.Instructions.RemoveRange(instructionIndex, instructionCount);
                    firstInstructionMap.Add(instruction, newNode);

                    newNode.Children = node.Children;
                    node.Children = ImmutableHashSet.Create(newNode);
                    newNode.Parents = newNode.Parents.Add(node);

                    node = newNode;
                }
                else
                {
                    // This instruction hasn't been processed yet, so we need to process it by
                    // creating a new node and adding it as the first instruction and then
                    // pushing it into the list of pending nodes.

                    node = new InstructionNode();
                    node.Instructions = node.Instructions.Add(instruction);

                    instructionNodeMap.Add(instruction, node);
                    firstInstructionMap.Add(instruction, node);

                    pendingNodes.Push(node);
                }

                node.Parents = node.Parents.Add(currentNode);
                currentNode.Children = currentNode.Children.Add(node);
            }

            bool ProcessInstruction(Instruction instruction, InstructionNode currentNode)
            {
                if (firstInstructionMap.TryGetValue(instruction, out var node))
                {
                    // This is already the first instruction of a node, so we just need
                    // to add the existing node as a child of the current node and return
                    // false so that we stop processing this sequence.

                    currentNode.Children = currentNode.Children.Add(node);
                    node.Parents = node.Parents.Add(currentNode);
                    return false;
                }
                else
                {
                    // This instruction hasn't been processed yet, so we need to process it by
                    // adding it to the current node and returning true so that we can continue
                    // processing the current sequence.

                    Debug.Assert(currentNode.Instructions.Contains(instruction) == false);
                    currentNode.Instructions = currentNode.Instructions.Add(instruction);
                    return true;
                }
            }
        }
        #endregion

        #region Methods
        public void AddProperty<T>(object key, T value)
        {
            Properties.Add(key, value);
        }

        public bool CanReach(InstructionNode node)
        {
            // Determining if this node can reach the target node is a simple traversal
            // where we return true if the traversed node is ever the target node.

            return TraverseDepthFirst((traversedNode) => (traversedNode == node)).Any();
        }

        public bool ContainsProperty(object key)
        {
            return Properties.ContainsKey(key);
        }

        public bool IsAdjacent(InstructionNode node)
        {
            // Determining if we are adjacent to a node is simply checking if the
            // children or parents of this node contains the target node.

            return Children.Contains(node) || Parents.Contains(node);
        }

        public T GetProperty<T>(object key)
        {
            return (T)(Properties[key]);
        }

        public void SetProperty<T>(object key, T value)
        {
            Properties[key] = value;
        }

        public void TraverseBreadthFirst(Action<InstructionNode> action)
        {
            // We do a breadth-first traversal of the nodes using a queue-based algorithm
            // to track pending nodes, rather than using recursion, so that we can
            // process much more complex graphs.

            var visitedNodes = new HashSet<InstructionNode>();
            var pendingNodes = new Queue<InstructionNode>();

            visitedNodes.Add(this);
            pendingNodes.Enqueue(this);

            do
            {
                var node = pendingNodes.Dequeue();
                action(node);

                foreach (var child in node.Children)
                {
                    if (visitedNodes.Contains(child) == false)
                    {
                        visitedNodes.Add(child);
                        pendingNodes.Enqueue(child);
                    }
                }
            }
            while (pendingNodes.Count != 0);
        }

        public IEnumerable<T> TraverseBreadthFirst<T>(Func<InstructionNode, T> func)
        {
            // We do a breadth-first traversal of the nodes using a queue-based algorithm
            // to track pending nodes, rather than using recursion, so that we can
            // process much more complex graphs.

            var visitedNodes = new HashSet<InstructionNode>();
            var pendingNodes = new Queue<InstructionNode>();

            visitedNodes.Add(this);
            pendingNodes.Enqueue(this);

            do
            {
                var node = pendingNodes.Dequeue();
                yield return func(node);

                foreach (var child in node.Children)
                {
                    if (visitedNodes.Contains(child) == false)
                    {
                        visitedNodes.Add(child);
                        pendingNodes.Enqueue(child);
                    }
                }
            }
            while (pendingNodes.Count != 0);
        }

        public void TraverseDepthFirst(Action<InstructionNode> action)
        {
            // We do a depth-first traversal of the nodes using a stack-based algorithm
            // to track pending nodes, rather than using recursion, so that we can
            // process much more complex graphs.

            var visitedNodes = new HashSet<InstructionNode>();
            var pendingNodes = new Stack<InstructionNode>();

            visitedNodes.Add(this);
            pendingNodes.Push(this);

            do
            {
                var node = pendingNodes.Pop();
                action(node);

                foreach (var child in node.Children)
                {
                    if (visitedNodes.Contains(child) == false)
                    {
                        visitedNodes.Add(child);
                        pendingNodes.Push(child);
                    }
                }
            }
            while (pendingNodes.Count != 0);
        }

        public IEnumerable<T> TraverseDepthFirst<T>(Func<InstructionNode, T> func)
        {
            // We do a depth-first traversal of the nodes using a stack-based algorithm
            // to track pending nodes, rather than using recursion, so that we can
            // process much more complex graphs.

            var visitedNodes = new HashSet<InstructionNode>();
            var pendingNodes = new Stack<InstructionNode>();

            visitedNodes.Add(this);
            pendingNodes.Push(this);

            do
            {
                var node = pendingNodes.Pop();
                yield return func(node);

                foreach (var child in node.Children)
                {
                    if (visitedNodes.Contains(child) == false)
                    {
                        visitedNodes.Add(child);
                        pendingNodes.Push(child);
                    }
                }
            }
            while (pendingNodes.Count != 0);
        }

        public bool TryGetProperty<T>(object key, out T value)
        {
            if (ContainsProperty(key))
            {
                value = GetProperty<T>(key);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
        #endregion

        #region System.Object Methods
        public override string ToString()
        {
            switch (Instructions.Length)
            {
                case 0:
                {
                    return string.Empty;
                }

                case 1:
                {
                    return $"({Instructions.Single()})";
                }

                default:
                {
                    return $"({Instructions.First()}, {Instructions.Last()})";
                }
            }
        }
        #endregion
    }
}
