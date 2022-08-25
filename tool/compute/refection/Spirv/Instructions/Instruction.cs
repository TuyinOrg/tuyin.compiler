using System;
using System.Collections.Generic;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Instructions
{
    internal abstract class Instruction
    {
        public abstract Op OpCode { get; }

        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public abstract bool HasResultId { get; }

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public abstract bool HasResultType { get; }

        public virtual bool TryGetResultId(out uint id)
        {
            id = 0;
            return false;
        }

        /// <summary>
        /// Read complete instruction from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public abstract void Parse(WordReader reader, uint end);

        /// <summary>
        /// Read instruction operands from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public abstract void ParseOperands(WordReader reader, uint end);

        public override string ToString()
        {
            return OpCode.ToString();
        }

        /// <summary>
        /// Calculate number of words to fit complete instruction bytecode.
        /// </summary>
        /// <returns>Number of words in instruction bytecode.</returns>
        public virtual uint GetWordCount()
        {
            throw new NotImplementedException(OpCode + " not implemented yet.");
        }

        /// <summary>
        /// Write instruction into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public virtual void Write(WordWriter writer)
        {
            throw new NotImplementedException(OpCode + " not implemented yet.");
        }

        /// <summary>
        /// Write instruction operands into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public virtual void WriteOperands(WordWriter writer)
        {
            throw new NotImplementedException(OpCode + " not implemented yet.");
        }

        public void Build(WordWriter writer)
        {
            uint wordCount = GetWordCount() + 1;
            uint opCode = (uint)OpCode | ((wordCount) << 16);
            writer.WriteWord(opCode);
            var expectedEnd = writer.Position + wordCount - 1;
            Write(writer);
            if (writer.Position != expectedEnd)
            {
                throw new InvalidOperationException("Written word count doesn't match expected word count");
            }
        }
    }
}