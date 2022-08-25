// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace IlOptimizer.CodeAnalysis
{
    public static class InstructionExtensions
    {
        private static readonly InstructionFamily[] InstructionFamilies = {
            InstructionFamily.Nop,            // Nop
            InstructionFamily.Break,          // Break
            InstructionFamily.Ldarg,          // Ldarg_0
            InstructionFamily.Ldarg,          // Ldarg_1
            InstructionFamily.Ldarg,          // Ldarg_2
            InstructionFamily.Ldarg,          // Ldarg_3
            InstructionFamily.Ldloc,          // Ldloc_0
            InstructionFamily.Ldloc,          // Ldloc_1
            InstructionFamily.Ldloc,          // Ldloc_2
            InstructionFamily.Ldloc,          // Ldloc_3
            InstructionFamily.Stloc,          // Stloc_0
            InstructionFamily.Stloc,          // Stloc_1
            InstructionFamily.Stloc,          // Stloc_2
            InstructionFamily.Stloc,          // Stloc_3
            InstructionFamily.Ldarg,          // Ldarg_S
            InstructionFamily.Ldarga,         // Ldarga_S
            InstructionFamily.Starg,          // Starg_S
            InstructionFamily.Ldloc,          // Ldloc_S
            InstructionFamily.Ldloca,         // Ldloca_S
            InstructionFamily.Stloc,          // Stloc_S
            InstructionFamily.Ldnull,         // Ldnull
            InstructionFamily.Ldc,            // Ldc_I4_M1
            InstructionFamily.Ldc,            // Ldc_I4_0
            InstructionFamily.Ldc,            // Ldc_I4_1
            InstructionFamily.Ldc,            // Ldc_I4_2
            InstructionFamily.Ldc,            // Ldc_I4_3
            InstructionFamily.Ldc,            // Ldc_I4_4
            InstructionFamily.Ldc,            // Ldc_I4_5
            InstructionFamily.Ldc,            // Ldc_I4_6
            InstructionFamily.Ldc,            // Ldc_I4_7
            InstructionFamily.Ldc,            // Ldc_I4_8
            InstructionFamily.Ldc,            // Ldc_I4_S
            InstructionFamily.Ldc,            // Ldc_I4
            InstructionFamily.Ldc,            // Ldc_I8
            InstructionFamily.Ldc,            // Ldc_R4
            InstructionFamily.Ldc,            // Ldc_R8
            InstructionFamily.Dup,            // Dup
            InstructionFamily.Pop,            // Pop
            InstructionFamily.Jmp,            // Jmp
            InstructionFamily.Call,           // Call
            InstructionFamily.Calli,          // Calli
            InstructionFamily.Ret,            // Ret
            InstructionFamily.Br,             // Br_S
            InstructionFamily.Brfalse,        // Brfalse_S
            InstructionFamily.Brtrue,         // Brtrue_S
            InstructionFamily.Beq,            // Beq_S
            InstructionFamily.Bge,            // Bge_S
            InstructionFamily.Bgt,            // Bgt_S
            InstructionFamily.Ble,            // Ble_S
            InstructionFamily.Blt,            // Blt_S
            InstructionFamily.Bne,            // Bne_Un_S
            InstructionFamily.Bge,            // Bge_Un_S
            InstructionFamily.Bgt,            // Bgt_Un_S
            InstructionFamily.Ble,            // Ble_Un_S
            InstructionFamily.Blt,            // Blt_Un_S
            InstructionFamily.Br,             // Br
            InstructionFamily.Brfalse,        // Brfalse
            InstructionFamily.Brtrue,         // Brtrue
            InstructionFamily.Beq,            // Beq
            InstructionFamily.Bge,            // Bge
            InstructionFamily.Bgt,            // Bgt
            InstructionFamily.Ble,            // Ble
            InstructionFamily.Blt,            // Blt
            InstructionFamily.Bne,            // Bne_Un
            InstructionFamily.Bge,            // Bge_Un
            InstructionFamily.Bgt,            // Bgt_Un
            InstructionFamily.Ble,            // Ble_Un
            InstructionFamily.Blt,            // Blt_Un
            InstructionFamily.Switch,         // Switch
            InstructionFamily.Ldind,          // Ldind_I1
            InstructionFamily.Ldind,          // Ldind_U1
            InstructionFamily.Ldind,          // Ldind_I2
            InstructionFamily.Ldind,          // Ldind_U2
            InstructionFamily.Ldind,          // Ldind_I4
            InstructionFamily.Ldind,          // Ldind_U4
            InstructionFamily.Ldind,          // Ldind_I8
            InstructionFamily.Ldind,          // Ldind_I
            InstructionFamily.Ldind,          // Ldind_R4
            InstructionFamily.Ldind,          // Ldind_R8
            InstructionFamily.Ldind,          // Ldind_Ref
            InstructionFamily.Stind,          // Stind_Ref
            InstructionFamily.Stind,          // Stind_I1
            InstructionFamily.Stind,          // Stind_I2
            InstructionFamily.Stind,          // Stind_I4
            InstructionFamily.Stind,          // Stind_I8
            InstructionFamily.Stind,          // Stind_R4
            InstructionFamily.Stind,          // Stind_R8
            InstructionFamily.Add,            // Add
            InstructionFamily.Sub,            // Sub
            InstructionFamily.Mul,            // Mul
            InstructionFamily.Div,            // Div
            InstructionFamily.Div,            // Div_Un
            InstructionFamily.Rem,            // Rem
            InstructionFamily.Rem,            // Rem_Un
            InstructionFamily.And,            // And
            InstructionFamily.Or,             // Or
            InstructionFamily.Xor,            // Xor
            InstructionFamily.Shl,            // Shl
            InstructionFamily.Shr,            // Shr
            InstructionFamily.Shr,            // Shr_Un
            InstructionFamily.Neg,            // Neg
            InstructionFamily.Not,            // Not
            InstructionFamily.Conv,           // Conv_I1
            InstructionFamily.Conv,           // Conv_I2
            InstructionFamily.Conv,           // Conv_I4
            InstructionFamily.Conv,           // Conv_I8
            InstructionFamily.Conv,           // Conv_R4
            InstructionFamily.Conv,           // Conv_R8
            InstructionFamily.Conv,           // Conv_U4
            InstructionFamily.Conv,           // Conv_U8
            InstructionFamily.Callvirt,       // Callvirt
            InstructionFamily.Cpobj,          // Cpobj
            InstructionFamily.Ldobj,          // Ldobj
            InstructionFamily.Ldstr,          // Ldstr
            InstructionFamily.Newobj,         // Newobj
            InstructionFamily.Castclass,      // Castclass
            InstructionFamily.Isinst,         // Isinst
            InstructionFamily.Conv,           // Conv_R_Un
            InstructionFamily.Unbox,          // Unbox
            InstructionFamily.Throw,          // Throw
            InstructionFamily.Ldfld,          // Ldfld
            InstructionFamily.Ldflda,         // Ldflda
            InstructionFamily.Stfld,          // Stfld
            InstructionFamily.Ldsfld,         // Ldsfld
            InstructionFamily.Ldsflda,        // Ldsflda
            InstructionFamily.Stsfld,         // Stsfld
            InstructionFamily.Stobj,          // Stobj
            InstructionFamily.Conv,           // Conv_Ovf_I1_Un
            InstructionFamily.Conv,           // Conv_Ovf_I2_Un
            InstructionFamily.Conv,           // Conv_Ovf_I4_Un
            InstructionFamily.Conv,           // Conv_Ovf_I8_Un
            InstructionFamily.Conv,           // Conv_Ovf_U1_Un
            InstructionFamily.Conv,           // Conv_Ovf_U2_Un
            InstructionFamily.Conv,           // Conv_Ovf_U4_Un
            InstructionFamily.Conv,           // Conv_Ovf_U8_Un
            InstructionFamily.Conv,           // Conv_Ovf_I_Un
            InstructionFamily.Conv,           // Conv_Ovf_U_Un
            InstructionFamily.Box,            // Box
            InstructionFamily.Newarr,         // Newarr
            InstructionFamily.Ldlen,          // Ldlen
            InstructionFamily.Ldelema,        // Ldelema
            InstructionFamily.Ldelem,         // Ldelem_I1
            InstructionFamily.Ldelem,         // Ldelem_U1
            InstructionFamily.Ldelem,         // Ldelem_I2
            InstructionFamily.Ldelem,         // Ldelem_U2
            InstructionFamily.Ldelem,         // Ldelem_I4
            InstructionFamily.Ldelem,         // Ldelem_U4
            InstructionFamily.Ldelem,         // Ldelem_I8
            InstructionFamily.Ldelem,         // Ldelem_I
            InstructionFamily.Ldelem,         // Ldelem_R4
            InstructionFamily.Ldelem,         // Ldelem_R8
            InstructionFamily.Ldelem,         // Ldelem_Ref
            InstructionFamily.Stelem,         // Stelem_I
            InstructionFamily.Stelem,         // Stelem_I1
            InstructionFamily.Stelem,         // Stelem_I2
            InstructionFamily.Stelem,         // Stelem_I4
            InstructionFamily.Stelem,         // Stelem_I8
            InstructionFamily.Stelem,         // Stelem_R4
            InstructionFamily.Stelem,         // Stelem_R8
            InstructionFamily.Stelem,         // Stelem_Ref
            InstructionFamily.Ldelem,         // Ldelem_Any
            InstructionFamily.Stelem,         // Stelem_Any
            InstructionFamily.Unbox,          // Unbox_Any
            InstructionFamily.Conv,           // Conv_Ovf_I1
            InstructionFamily.Conv,           // Conv_Ovf_U1
            InstructionFamily.Conv,           // Conv_Ovf_I2
            InstructionFamily.Conv,           // Conv_Ovf_U2
            InstructionFamily.Conv,           // Conv_Ovf_I4
            InstructionFamily.Conv,           // Conv_Ovf_U4
            InstructionFamily.Conv,           // Conv_Ovf_I8
            InstructionFamily.Conv,           // Conv_Ovf_U8
            InstructionFamily.Refanyval,      // Refanyval
            InstructionFamily.Ckfinite,       // Ckfinite
            InstructionFamily.Mkrefany,       // Mkrefany
            InstructionFamily.Ldtoken,        // Ldtoken
            InstructionFamily.Conv,           // Conv_U2
            InstructionFamily.Conv,           // Conv_U1
            InstructionFamily.Conv,           // Conv_I
            InstructionFamily.Conv,           // Conv_Ovf_I
            InstructionFamily.Conv,           // Conv_Ovf_U
            InstructionFamily.Add,            // Add_Ovf
            InstructionFamily.Add,            // Add_Ovf_Un
            InstructionFamily.Mul,            // Mul_Ovf
            InstructionFamily.Mul,            // Mul_Ovf_Un
            InstructionFamily.Sub,            // Sub_Ovf
            InstructionFamily.Sub,            // Sub_Ovf_Un
            InstructionFamily.Endfinally,     // Endfinally
            InstructionFamily.Leave,          // Leave
            InstructionFamily.Leave,          // Leave_S
            InstructionFamily.Stind,          // Stind_I
            InstructionFamily.Conv,           // Conv_U
            InstructionFamily.Arglist,        // Arglist
            InstructionFamily.Ceq,            // Ceq
            InstructionFamily.Cgt,            // Cgt
            InstructionFamily.Cgt,            // Cgt_Un
            InstructionFamily.Clt,            // Clt
            InstructionFamily.Clt,            // Clt_Un
            InstructionFamily.Ldftn,          // Ldftn
            InstructionFamily.Ldvirtftn,      // Ldvirtftn
            InstructionFamily.Ldarg,          // Ldarg
            InstructionFamily.Ldarga,         // Ldarga
            InstructionFamily.Starg,          // Starg
            InstructionFamily.Ldloc,          // Ldloc
            InstructionFamily.Ldloca,         // Ldloca
            InstructionFamily.Stloc,          // Stloc
            InstructionFamily.Localloc,       // Localloc
            InstructionFamily.Endfilter,      // Endfilter
            InstructionFamily.Unaligned,      // Unaligned
            InstructionFamily.Volatile,       // Volatile
            InstructionFamily.Tail,           // Tail
            InstructionFamily.Initobj,        // Initobj
            InstructionFamily.Constrained,    // Constrained
            InstructionFamily.Cpblk,          // Cpblk
            InstructionFamily.Initblk,        // Initblk
            InstructionFamily.No,             // No
            InstructionFamily.Rethrow,        // Rethrow
            InstructionFamily.Sizeof,         // Sizeof
            InstructionFamily.Refanytype,     // Refanytype
            InstructionFamily.Readonly,       // Readonly
        };

        public static Code GetCode(this Instruction instruction)
        {
            return instruction.OpCode.Code;
        }

        public static Instruction GetConsumerForLdloca(this Instruction instruction, MethodBody methodBody, out int stackIndex)
        {
            if (instruction.Previous.GetInstructionFamily() != InstructionFamily.Ldloca)
            {
                throw new InvalidOperationException();
            }

            var stack = new Stack<int>();
            stack.Push(4);

            do
            {
                var instructionFamily = instruction.GetInstructionFamily();

                switch (instructionFamily)
                {
                    case InstructionFamily.Nop:
                    case InstructionFamily.Break:
                    case InstructionFamily.Volatile:
                    case InstructionFamily.Constrained:
                    {
                        break;
                    }

                    case InstructionFamily.Ldarg:
                    {
                        var parameter = instruction.GetParameterForLdarg(methodBody);
                        PushStack(parameter.ParameterType);
                        break;
                    }

                    case InstructionFamily.Ldloc:
                    {
                        var variable = instruction.GetVariableForLdloc(methodBody);
                        PushStack(variable.VariableType);
                        break;
                    }

                    case InstructionFamily.Stloc:
                    {
                        var variable = instruction.GetVariableForStloc(methodBody);
                        PopStack(variable.VariableType);
                        break;
                    }

                    case InstructionFamily.Ldarga:
                    case InstructionFamily.Ldloca:
                    case InstructionFamily.Ldnull:
                    case InstructionFamily.Ldstr:
                    case InstructionFamily.Ldsflda:
                    case InstructionFamily.Ldtoken:
                    case InstructionFamily.Arglist:
                    case InstructionFamily.Sizeof:
                    {
                        stack.Push(4);
                        break;
                    }

                    case InstructionFamily.Starg:
                    {
                        var parameter = instruction.GetParameterForStarg();
                        PopStack(parameter.ParameterType);
                        break;
                    }

                    case InstructionFamily.Ldc:
                    {
                        switch (instruction.GetCode())
                        {
                            case Code.Ldc_I4_M1:
                            case Code.Ldc_I4_0:
                            case Code.Ldc_I4_1:
                            case Code.Ldc_I4_2:
                            case Code.Ldc_I4_3:
                            case Code.Ldc_I4_4:
                            case Code.Ldc_I4_5:
                            case Code.Ldc_I4_6:
                            case Code.Ldc_I4_7:
                            case Code.Ldc_I4_8:
                            case Code.Ldc_I4_S:
                            case Code.Ldc_I4:
                            case Code.Ldc_R4:
                                stack.Push(4);
                                break;

                            case Code.Ldc_I8:
                            case Code.Ldc_R8:
                                stack.Push(8);
                                break;

                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    }

                    case InstructionFamily.Dup:
                    {
                        var size = stack.Peek();
                        stack.Push(size);
                        break;
                    }

                    case InstructionFamily.Pop:
                    {
                        stack.Pop();
                        break;
                    }

                    case InstructionFamily.Jmp:
                    case InstructionFamily.Ret:
                    case InstructionFamily.Br:
                    case InstructionFamily.Brfalse:
                    case InstructionFamily.Brtrue:
                    case InstructionFamily.Beq:
                    case InstructionFamily.Bgt:
                    case InstructionFamily.Ble:
                    case InstructionFamily.Blt:
                    case InstructionFamily.Bne:
                    case InstructionFamily.Bge:
                    case InstructionFamily.Switch:
                    case InstructionFamily.Throw:
                    {
                        stackIndex = 0;
                        return null;
                    }

                    case InstructionFamily.Call:
                    case InstructionFamily.Callvirt:
                    case InstructionFamily.Newobj:
                    {
                        var method = (MethodReference)(instruction.Operand);
                        var parametersCount = method.Parameters.Count;

                        if (method.HasThis && (method.ExplicitThis == false))
                        {
                            parametersCount++;
                        }

                        if (method.HasParameters)
                        {
                            if (parametersCount >= stack.Count)
                            {
                                stackIndex = (parametersCount - stack.Count);
                                return instruction;
                            }

                            foreach (var parameter in method.Parameters)
                            {
                                PopStack(parameter.ParameterType);
                            }
                        }

                        if (method.HasThis)
                        {
                            if (stack.Count == 1)
                            {
                                stackIndex = 0;
                                return instruction;
                            }

                            Pop(4);
                        }

                        var returnType = method.ReturnType;

                        if (returnType.MetadataType != MetadataType.Void)
                        {
                            PushStack(returnType);
                        }

                        break;
                    }

                    case InstructionFamily.Ldind:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);

                        switch (instruction.GetCode())
                        {
                            case Code.Ldind_I1:
                            case Code.Ldind_U1:
                            case Code.Ldind_I2:
                            case Code.Ldind_U2:
                            case Code.Ldind_I4:
                            case Code.Ldind_U4:
                            case Code.Ldind_I:
                            case Code.Ldind_R4:
                            case Code.Ldind_Ref:
                                stack.Push(4);
                                break;

                            case Code.Ldind_I8:
                            case Code.Ldind_R8:
                                stack.Push(8);
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                        break;
                    }

                    case InstructionFamily.Stind:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (2 - stack.Count);
                            return instruction;
                        }

                        switch (instruction.GetCode())
                        {
                            case Code.Stind_Ref:
                            case Code.Stind_I1:
                            case Code.Stind_I2:
                            case Code.Stind_I4:
                            case Code.Stind_R4:
                                Pop(4);
                                break;

                            case Code.Stind_I8:
                            case Code.Stind_R8:
                                Pop(8);
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                        Pop(4);
                        break;
                    }

                    case InstructionFamily.Calli:

                    case InstructionFamily.Add:
                    case InstructionFamily.Sub:
                    case InstructionFamily.Mul:
                    case InstructionFamily.Div:
                    case InstructionFamily.Rem:
                    case InstructionFamily.And:
                    case InstructionFamily.Or:
                    case InstructionFamily.Xor:
                    case InstructionFamily.Shl:
                    case InstructionFamily.Shr:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (stack.Count - 2);
                            return instruction;
                        }

                        var size = Math.Max(stack.Pop(), stack.Pop());
                        stack.Push(size);

                        break;
                    }

                    case InstructionFamily.Neg:
                    case InstructionFamily.Not:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        break;
                    }

                    case InstructionFamily.Conv:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        stack.Pop();

                        switch (instruction.GetCode())
                        {
                            case Code.Conv_I1:
                            case Code.Conv_I2:
                            case Code.Conv_I4:
                            case Code.Conv_R4:
                            case Code.Conv_U4:
                            case Code.Conv_Ovf_I1_Un:
                            case Code.Conv_Ovf_I2_Un:
                            case Code.Conv_Ovf_I4_Un:
                            case Code.Conv_Ovf_U1_Un:
                            case Code.Conv_Ovf_U2_Un:
                            case Code.Conv_Ovf_U4_Un:
                            case Code.Conv_Ovf_I_Un:
                            case Code.Conv_Ovf_U_Un:
                            case Code.Conv_Ovf_I1:
                            case Code.Conv_Ovf_U1:
                            case Code.Conv_Ovf_I2:
                            case Code.Conv_Ovf_U2:
                            case Code.Conv_Ovf_I4:
                            case Code.Conv_Ovf_U4:
                            case Code.Conv_U2:
                            case Code.Conv_U1:
                            case Code.Conv_I:
                            case Code.Conv_Ovf_I:
                            case Code.Conv_Ovf_U:
                            case Code.Conv_U:
                                stack.Push(4);
                                break;

                            case Code.Conv_I8:
                            case Code.Conv_R8:
                            case Code.Conv_U8:
                            case Code.Conv_R_Un:
                            case Code.Conv_Ovf_I8_Un:
                            case Code.Conv_Ovf_U8_Un:
                            case Code.Conv_Ovf_I8:
                            case Code.Conv_Ovf_U8:
                                stack.Push(8);
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                        break;
                    }

                    case InstructionFamily.Castclass:
                    case InstructionFamily.Isinst:
                    {
                        Pop(4);
                        stack.Push(4);
                        break;
                    }

                    case InstructionFamily.Unbox:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);
                        stack.Push(4);

                        break;
                    }

                    case InstructionFamily.Ldfld:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);

                        var field = instruction.GetFieldForLdfld();
                        PushStack(field.FieldType);

                        break;
                    }

                    case InstructionFamily.Ldflda:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);
                        stack.Push(4);

                        break;
                    }

                    case InstructionFamily.Stfld:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (2 - stack.Count);
                            return instruction;
                        }

                        var field = instruction.GetFieldForLdfld();
                        PopStack(field.FieldType);

                        Pop(4);
                        break;
                    }

                    case InstructionFamily.Ldsfld:
                    {
                        var field = instruction.GetFieldForLdsfld();
                        PushStack(field.FieldType);
                        break;
                    }

                    case InstructionFamily.Stsfld:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        var field = instruction.GetFieldForStsfld();
                        PopStack(field.FieldType);
                        break;
                    }

                    case InstructionFamily.Box:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        stack.Pop();
                        stack.Push(4);

                        break;
                    }

                    case InstructionFamily.Newarr:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);
                        stack.Push(4);

                        break;
                    }

                    case InstructionFamily.Ldlen:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);
                        stack.Push(4);

                        break;
                    }

                    case InstructionFamily.Ldelema:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (2 - stack.Count);
                            return instruction;
                        }

                        Pop(4);
                        Pop(4);

                        stack.Push(4);
                        break;
                    }

                    case InstructionFamily.Ldelem:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (2 - stack.Count);
                            return instruction;
                        }

                        Pop(4);
                        Pop(4);

                        var type = instruction.GetTypeForLdelem();

                        if (type != null)
                        {
                            PushStack(type);
                        }
                        else
                        {
                            switch (instruction.GetCode())
                            {
                                case Code.Ldelem_I1:
                                case Code.Ldelem_U1:
                                case Code.Ldelem_I2:
                                case Code.Ldelem_U2:
                                case Code.Ldelem_I4:
                                case Code.Ldelem_U4:
                                case Code.Ldelem_I:
                                case Code.Ldelem_R4:
                                case Code.Ldelem_Ref:
                                    stack.Push(4);
                                    break;

                                case Code.Ldelem_I8:
                                case Code.Ldelem_R8:
                                    stack.Push(8);
                                    break;

                                default:
                                    throw new InvalidOperationException();
                            }
                        }

                        break;
                    }

                    case InstructionFamily.Stelem:
                    {
                        if (stack.Count <= 3)
                        {
                            stackIndex = (3 - stack.Count);
                            return instruction;
                        }

                        var type = instruction.GetTypeForStelem();

                        if (type != null)
                        {
                            PopStack(type);
                        }
                        else
                        {
                            switch (instruction.GetCode())
                            {
                                case Code.Stelem_I:
                                case Code.Stelem_I1:
                                case Code.Stelem_I2:
                                case Code.Stelem_I4:
                                case Code.Stelem_R4:
                                case Code.Stelem_Ref:
                                    Pop(4);
                                    break;

                                case Code.Stelem_I8:
                                case Code.Stelem_R8:
                                    Pop(8);
                                    break;

                                default:
                                    throw new InvalidOperationException();
                            }
                        }

                        Pop(4);
                        Pop(4);

                        break;
                    }

                    case InstructionFamily.Ceq:
                    case InstructionFamily.Cgt:
                    case InstructionFamily.Clt:
                    {
                        if (stack.Count <= 2)
                        {
                            stackIndex = (2 - stack.Count);
                            return instruction;
                        }

                        stack.Pop();
                        stack.Pop();

                        stack.Push(4);
                        break;
                    }

                    case InstructionFamily.Initobj:
                    {
                        if (stack.Count == 1)
                        {
                            stackIndex = 0;
                            return instruction;
                        }

                        Pop(4);
                        break;
                    }

                    case InstructionFamily.Cpobj:
                    case InstructionFamily.Ldobj:
                    case InstructionFamily.Stobj:
                    case InstructionFamily.Refanyval:
                    case InstructionFamily.Ckfinite:
                    case InstructionFamily.Mkrefany:
                    case InstructionFamily.Endfinally:
                    case InstructionFamily.Leave:
                    case InstructionFamily.Ldftn:
                    case InstructionFamily.Ldvirtftn:
                    case InstructionFamily.Localloc:
                    case InstructionFamily.Endfilter:
                    case InstructionFamily.Unaligned:
                    case InstructionFamily.Tail:
                    case InstructionFamily.Cpblk:
                    case InstructionFamily.Initblk:
                    case InstructionFamily.No:
                    case InstructionFamily.Rethrow:
                    case InstructionFamily.Refanytype:
                    case InstructionFamily.Readonly:
                        throw new NotImplementedException();
                }

                if (stack.Count == 0)
                {
                    throw new InvalidOperationException();
                }

                instruction = instruction.Next;
            }
            while (instruction != null);

            stackIndex = 0;
            return instruction;

            void Pop(int size)
            {
                if ((stack.Count == 0) || (stack.Peek() != size))
                {
                    throw new InvalidOperationException();
                }

                stack.Pop();
            }

            void PopStack(TypeReference type)
            {
                var size = GetStackSize(type);
                Pop(size);
            }

            void PushStack(TypeReference type)
            {
                var size = GetStackSize(type);
                stack.Push(size);
            }

            int GetStackSize(TypeReference type)
            {
                switch (type.MetadataType)
                {
                    case MetadataType.Void:
                    default:
                        throw new InvalidOperationException();

                    case MetadataType.Boolean:
                    case MetadataType.Char:
                    case MetadataType.SByte:
                    case MetadataType.Byte:
                    case MetadataType.Int16:
                    case MetadataType.UInt16:
                    case MetadataType.Int32:
                    case MetadataType.UInt32:
                    case MetadataType.Single:
                        return 4;

                    case MetadataType.Int64:
                    case MetadataType.UInt64:
                    case MetadataType.Double:
                        return 8;

                    case MetadataType.String:
                    case MetadataType.Pointer:
                    case MetadataType.ByReference:
                    case MetadataType.Class:
                    case MetadataType.Array:
                    case MetadataType.IntPtr:
                    case MetadataType.UIntPtr:
                    case MetadataType.FunctionPointer:
                    case MetadataType.Object:
                    case MetadataType.Pinned:
                        return 4;

                    case MetadataType.ValueType:
                    {
                        if (type.IsDefinition)
                        {
                            var typeDefinition = (TypeDefinition)(type);

                            if (typeDefinition.IsEnum)
                            {
                                return GetStackSize(typeDefinition.Fields.First().FieldType);
                            }
                        }

                        return 4;
                    }

                    case MetadataType.Var:
                    case MetadataType.GenericInstance:
                    case MetadataType.MVar:
                        return 4;


                    case MetadataType.RequiredModifier:
                        var requiredModifier = (RequiredModifierType)(type);
                        return GetStackSize(requiredModifier.ElementType);

                    case MetadataType.OptionalModifier:
                        var optionalModifier = (OptionalModifierType)(type);
                        return GetStackSize(optionalModifier.ElementType);

                    
                    case MetadataType.TypedByReference:
                    case MetadataType.Sentinel:
                        throw new NotImplementedException();
                }
            }
        }

        public static InstructionFamily GetInstructionFamily(this Instruction instruction)
        {
            return InstructionFamilies[(int)(instruction.OpCode.Code)];
        }

        public static FieldReference GetFieldForLdfld(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldfld)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static FieldReference GetFieldForLdflda(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldflda)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static FieldReference GetFieldForLdsfld(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldsfld)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static FieldReference GetFieldForLdsflda(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldsflda)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static FieldReference GetFieldForStfld(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Stfld)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static FieldReference GetFieldForStsfld(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Stsfld)
            {
                throw new InvalidOperationException();
            }

            return (FieldReference)(instruction.Operand);
        }

        public static ParameterReference GetParameterForLdarg(this Instruction instruction, MethodBody methodBody)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldarg)
            {
                throw new InvalidOperationException();
            }

            var method = methodBody.Method;

            switch (instruction.GetCode())
            {
                case Code.Ldarg_0:
                    return method.HasThis ? methodBody.ThisParameter : method.Parameters[0];

                case Code.Ldarg_1:
                    return method.Parameters[method.HasThis ? 0 : 1];

                case Code.Ldarg_2:
                    return method.Parameters[method.HasThis ? 1 : 2];

                case Code.Ldarg_3:
                    return method.Parameters[method.HasThis ? 2 : 3];

                default:
                    return (ParameterReference)(instruction.Operand);
            }
        }

        public static ParameterReference GetParameterForLdarga(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldarga)
            {
                throw new InvalidOperationException();
            }

            return (ParameterReference)(instruction.Operand);
        }

        public static ParameterReference GetParameterForStarg(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Starg)
            {
                throw new InvalidOperationException();
            }

            return (ParameterReference)(instruction.Operand);
        }

        public static TypeReference GetTypeForLdelem(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldelem)
            {
                throw new InvalidOperationException();
            }

            return (TypeReference)(instruction.Operand);
        }

        public static TypeReference GetTypeForLdelema(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldelema)
            {
                throw new InvalidOperationException();
            }

            return (TypeReference)(instruction.Operand);
        }

        public static TypeReference GetTypeForStelem(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Stelem)
            {
                throw new InvalidOperationException();
            }

            return (TypeReference)(instruction.Operand);
        }

        public static VariableReference GetVariableForLdloc(this Instruction instruction, MethodBody methodBody)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldloc)
            {
                throw new InvalidOperationException();
            }

            switch (instruction.GetCode())
            {
                case Code.Ldloc_0:
                    return methodBody.Variables[0];

                case Code.Ldloc_1:
                    return methodBody.Variables[1];

                case Code.Ldloc_2:
                    return methodBody.Variables[2];

                case Code.Ldloc_3:
                    return methodBody.Variables[3];

                default:
                    return (VariableReference)(instruction.Operand);
            }
        }

        public static VariableReference GetVariableForLdloca(this Instruction instruction)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Ldloca)
            {
                throw new InvalidOperationException();
            }

            return (VariableReference)(instruction.Operand);
        }

        public static VariableReference GetVariableForStloc(this Instruction instruction, MethodBody methodBody)
        {
            if (instruction.GetInstructionFamily() != InstructionFamily.Stloc)
            {
                throw new InvalidOperationException();
            }

            switch (instruction.GetCode())
            {
                case Code.Stloc_0:
                    return methodBody.Variables[0];

                case Code.Stloc_1:
                    return methodBody.Variables[1];

                case Code.Stloc_2:
                    return methodBody.Variables[2];

                case Code.Stloc_3:
                    return methodBody.Variables[3];

                default:
                    return (VariableReference)(instruction.Operand);
            }
        }
    }
}
