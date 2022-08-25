// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace IlOptimizer.CodeAnalysis
{
    // This is a control flow graph where each node defines a basic block of instructions.

    public sealed class InstructionGraph
    {
        #region Constructors
        public InstructionGraph(MethodBody methodBody)
        {
            if (methodBody is null)
            {
                throw new ArgumentNullException(nameof(methodBody));
            }

            Root = InstructionNode.Create(methodBody.Instructions[0]);

            var nodeCount = 0;
            {
                TraverseDepthFirst((node) => { nodeCount += 1; });
            }
            NodeCount = nodeCount;
        }
        #endregion

        #region Properties
        public MethodBody MethodBody { get; }

        public int NodeCount { get; }

        public InstructionNode Root { get; }
        #endregion

        #region Methods
        public void TraverseBreadthFirst(Action<InstructionNode> action)
        {
            Root.TraverseBreadthFirst(action);
        }

        public IEnumerable<T> TravserBreadthFirst<T>(Func<InstructionNode, T> func)
        {
            return Root.TraverseBreadthFirst(func);
        }

        public void TraverseDepthFirst(Action<InstructionNode> action)
        {
            Root.TraverseDepthFirst(action);
        }

        public IEnumerable<T> TraverseDepthFirst<T>(Func<InstructionNode, T> func)
        {
            return Root.TraverseDepthFirst(func);
        }
        #endregion
    }
}
