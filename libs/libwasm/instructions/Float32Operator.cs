using libwasm.binary;

namespace libwasm.instructions
{
    /// <summary>
    /// Describes an operator that takes a single 32-bit floating-point number immediate.
    /// </summary>
    public sealed class Float32Operator : Operator
    {
        /// <summary>
        /// Creates an operator that takes a 32-bit floating-point number immediate.
        /// </summary>
        /// <param name="opCode">The operator's opcode.</param>
        /// <param name="declaringType">A type that defines the operator, if any.</param>
        /// <param name="mnemonic">The operator's mnemonic.</param>
        public Float32Operator(byte opCode, WasmType declaringType, string mnemonic)
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
            return Create(reader.ReadFloat32());
        }

        /// <summary>
        /// Creates a new instruction from this operator and the given
        /// immediate.
        /// </summary>
        /// <param name="immediate">The immediate.</param>
        /// <returns>A new instruction.</returns>
        public Float32Instruction Create(float immediate)
        {
            return new Float32Instruction(this, immediate);
        }

        /// <summary>
        /// Casts the given instruction to this operator's instruction type.
        /// </summary>
        /// <param name="value">The instruction to cast.</param>
        /// <returns>The given instruction as this operator's instruction type.</returns>
        public Float32Instruction CastInstruction(Instruction value)
        {
            return (Float32Instruction)value;
        }
    }
}
