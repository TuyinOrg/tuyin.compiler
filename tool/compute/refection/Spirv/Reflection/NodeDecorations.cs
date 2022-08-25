using System;
using System.Collections.Generic;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection
{
    internal static class ExtensionMethods
    {
        public static Decoration FindDecoration(this IEnumerable<Instruction> items, Decoration.Enumerant decoration)
        {
            foreach (var instruction in items)
            {
                if (instruction.OpCode == Op.OpDecorate)
                {
                    var decorate = (OpDecorate) instruction;
                    if (decorate.Decoration.Value == decoration)
                    {
                        return decorate.Decoration;
                    }
                }
            }

            return null;
        }

        public static T FindDecoration<T>(this IEnumerable<Instruction> items) where T:Decoration
        {
            foreach (var instruction in items)
            {
                if (instruction.OpCode == Op.OpDecorate)
                {
                    var decorate = (OpDecorate)instruction;
                    if (decorate.Decoration is T d)
                    {
                        return d;
                    }
                }
            }

            return null;
        }
    }
    internal struct NodeDecorations
    {
        private List<Instruction> _decorations;

        public OpName Name;

        public IReadOnlyList<Instruction> Decorations
        {
            get { return _decorations ?? (IReadOnlyList<Instruction>)Array.Empty<Instruction>(); }
        }

        public void AddDecoration(Instruction instruction)
        {
            if (_decorations == null)
                _decorations = new List<Instruction>();
            _decorations.Add(instruction);
        }
    }
}