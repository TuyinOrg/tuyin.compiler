using System.IO;
using System.Text;
using libwasm.binary;

namespace libwasm.instructions
{
    /// <summary>
    /// Describes an operator that takes a single 32-bit signed integer immediate.
    /// </summary>
    public sealed class VarInt32Operator : Operator
    {
        /// <summary>
        /// Creates an operator that takes a single 32-bit signed integer immediate.
        /// </summary>
        /// <param name="opCode">The operator's opcode.</param>
        /// <param name="declaringType">A type that defines the operator, if any.</param>
        /// <param name="mnemonic">The operator's mnemonic.</param>
        public VarInt32Operator(byte opCode, WasmType declaringType, string mnemonic)
            : base(opCode, declaringType, mnemonic)
        { }

        /// <summary>
        /// Reads the immediates (not the opcode) of a WebAssembly instruction
        /// for this operator from the given reader and returns the result as an
        /// instruction.
        /// </summary>
        /// <param name="reader">The WebAssembly file reader to read immediates from.</param>
        /// <returns>A WebAssembly instruction.</returns>
        public override Instruction ReadImmediates(BinaryWasmReader reader)
        {
            return Create(reader.ReadVarInt32());
        }

        /// <summary>
        /// Creates a new instruction from this operator and the given
        /// immediate.
        /// </summary>
        /// <param name="immediate">The immediate.</param>
        /// <returns>A new instruction.</returns>
        public VarInt32Instruction Create(int immediate)
        {
            return new VarInt32Instruction(this, immediate);
        }

        /// <summary>
        /// Casts the given instruction to this operator's instruction type.
        /// </summary>
        /// <param name="value">The instruction to cast.</param>
        /// <returns>The given instruction as this operator's instruction type.</returns>
        public VarInt32Instruction CastInstruction(Instruction value)
        {
            return (VarInt32Instruction)value;
        }
    }
}
