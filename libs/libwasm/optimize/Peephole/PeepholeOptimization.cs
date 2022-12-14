using libwasm.instructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace libwasm.optimize
{
    /// <summary>
    /// An optimization that pattern-matches and rewrites small sequences of
    /// instructions.
    /// </summary>
    public abstract class PeepholeOptimization
    {
        /// <summary>
        /// Tests if the items at the front of the given list of instructions
        /// match the peephole optimization; if a match occurs, a nonzero value
        /// is returned that indicates the number of instructions at the front
        /// of the list of instructions that should be rewritten.
        /// </summary>
        /// <param name="instructions">
        /// The instructions to match against the peephole optimization.
        /// </param>
        /// <returns>The number of instructions to rewrite.</returns>
        public abstract uint Match(IReadOnlyList<Instruction> instructions);

        /// <summary>
        /// Rewrites the given sequence of instructions.
        /// </summary>
        /// <param name="matched">
        /// A list of instructions that has been matched and will all be replaced.
        /// </param>
        /// <returns>The rewritten instructions.</returns>
        public abstract IReadOnlyList<Instruction> Rewrite(IReadOnlyList<Instruction> matched);
    }

    /// <summary>
    /// An optimizer that applies peephole optimizations.
    /// </summary>
    public sealed class PeepholeOptimizer
    {
        /// <summary>
        /// Creates a peephole optimizer that applies the given optimizations.
        /// </summary>
        /// <param name="optimizations">The optimizations to apply.</param>
        public PeepholeOptimizer(IEnumerable<PeepholeOptimization> optimizations)
        {
            this.opts = optimizations;
        }

        private IEnumerable<PeepholeOptimization> opts;

        /// <summary>
        /// A peephole optimizer based that uses the default set of peephole
        /// optimizations offered by cs-wasm.
        /// </summary>
        public static PeepholeOptimizer DefaultOptimizer => new PeepholeOptimizer(DefaultOptimizations);

        /// <summary>
        /// The default set of peephole optimizations that ships with cs-wasm.
        /// </summary>
        public static readonly IEnumerable<PeepholeOptimization> DefaultOptimizations =
            new PeepholeOptimization[]
        {
            TeeLocalOptimization.Instance,
            UnreachableCodeOptimization.Instance
        };

        /// <summary>
        /// Uses this peephole optimizer to optimize the given sequence of instructions.
        /// </summary>
        /// <param name="instructions">The instructions to optimize.</param>
        /// <returns>An optimized sequence of instructions.</returns>
        public IReadOnlyList<Instruction> Optimize(IReadOnlyList<Instruction> instructions)
        {
            var inputArray = Enumerable.ToArray<Instruction>(instructions);
            var results = new List<Instruction>();
            for (int i = 0; i < inputArray.Length;)
            {
                PeepholeOptimization bestOpt;
                uint matchSize = LongestMatch(
                    new ArraySegment<Instruction>(inputArray, i, inputArray.Length - i),
                    out bestOpt);
                if (matchSize > 0)
                {
                    results.AddRange(
                        bestOpt.Rewrite(
                            new ArraySegment<Instruction>(inputArray, i, (int)matchSize)));
                    i += (int)matchSize;
                }
                else
                {
                    if (inputArray[i] is BlockInstruction)
                    {
                        // Visit block instructions recursively.
                        var block = (BlockInstruction)inputArray[i];
                        results.Add(
                            new BlockInstruction((BlockOperator)block.Op, block.Type, Optimize(block.Contents)));
                    }
                    else if (inputArray[i] is IfElseInstruction)
                    {
                        // Visit if-else instructions recursively, too.
                        var ifElse = (IfElseInstruction)inputArray[i];
                        results.Add(
                            new IfElseInstruction(
                                ifElse.Type,
                                ifElse.IfBranch == null ? null : Optimize(ifElse.IfBranch),
                                ifElse.ElseBranch == null ? null : Optimize(ifElse.ElseBranch)));
                    }
                    else
                    {
                        // Other instructions are added to the list unmodified.
                        results.Add(inputArray[i]);
                    }
                    i++;
                }
            }
            return results;
        }

        private uint LongestMatch(
            IReadOnlyList<Instruction> instructions,
            out PeepholeOptimization matchingOptimization)
        {
            uint bestMatch = 0;
            PeepholeOptimization bestOpt = null;
            foreach (var opt in opts)
            {
                uint match = opt.Match(instructions);
                if (match > bestMatch)
                {
                    bestMatch = match;
                    bestOpt = opt;
                }
            }
            matchingOptimization = bestOpt;
            return bestMatch;
        }
    }
}
