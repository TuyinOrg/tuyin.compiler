using System;
using System.Runtime.CompilerServices;

namespace Tuyin.IR.Analysis.Data
{
    public enum OpCode
    {
        Nop,
        Xor,
        Sub,
        Rem,
        Mul,
        Div,
        Add,
        And,
        Or,
        Shr,
        Shl,
        Test,
        Goto,
        Ceq,
        Neg,
        Cgt,
        Clt,
        Ret,
        Call,
        Ldc,
        Ldr,
        Ldstr,
        Ldelem,
        New,
        Load,
        Store,
        Phi
    }

    static class OpCodeHelper 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperandType GetOperandType(this OpCode op) 
        {
            return op switch
            {
                OpCode.Goto => OperandType.Uint16,
                OpCode.Test => OperandType.Uint16,
                OpCode.New => OperandType.Uint16,
                OpCode.Call => OperandType.Uint8,
                OpCode.Ldc => OperandType.Int32,
                OpCode.Ldr => OperandType.Float32,
                OpCode.Ldstr => OperandType.String,
                OpCode.Load => OperandType.String,

                _ => OperandType.None
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPopSize(this OpCode op) 
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPushSize(this OpCode op) 
        {
            throw new NotImplementedException();
        }
    }
}
