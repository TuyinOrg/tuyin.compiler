using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tuyin.IR.Reflection.Types;
using Type = Tuyin.IR.Reflection.Types.Type;

namespace Tuyin.IR.Reflection.Symbols
{
    public sealed class DIMetadataManager
    {
        private Dictionary<Type, DIBasicType> mTypes;
        private Dictionary<DIMetadata, int> mIdents;
        private Dictionary<DIMetadata, string> mReferences;
        private static Dictionary<string, Delegate> mDelegates;

        public bool Enabled 
        { 
            get; 
            internal set;
        }

        static DIMetadataManager() 
        {
            mDelegates = new Dictionary<string, Delegate>();

            var diType = typeof(DIMetadata);
            var types = diType.Assembly.GetTypes().Where(x => diType.IsAssignableFrom(x));
        }

        public DIMetadataManager()
        {
            mTypes = new Dictionary<Type, DIBasicType>();
            mIdents = new Dictionary<DIMetadata, int>();
            mReferences = new Dictionary<DIMetadata, string>();
        }

        internal DIBasicType GetMetadataType(Type type)
        {
            if (!mTypes.ContainsKey(type)) 
            {
                DIBasicType diType = null;

                if (type.IsPrimitive)
                {
                    var ltype = type as PrimitiveType;
                    switch (ltype.Type)
                    {
                        case PrimitiveTypes.i8:
                            diType = new DIBasicType("i8", 8, 8, DIEncoding.DW_ATE_signed, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.i16:
                            diType = new DIBasicType("i16", 16, 16, DIEncoding.DW_ATE_signed, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.i32:
                            diType = new DIBasicType("i32", 32, 32, DIEncoding.DW_ATE_signed, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.i64:
                            diType = new DIBasicType("i64", 64, 64, DIEncoding.DW_ATE_signed, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.u8:
                            diType = new DIBasicType("u8", 8, 8, DIEncoding.DW_ATE_unsigned, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.u16:
                            diType = new DIBasicType("u16", 16, 16, DIEncoding.DW_ATE_unsigned, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.u32:
                            diType = new DIBasicType("u32", 32, 32, DIEncoding.DW_ATE_unsigned, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.u64:
                            diType = new DIBasicType("u64", 64, 64, DIEncoding.DW_ATE_unsigned, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.f32:
                            diType = new DIBasicType("f32", 32, 32, DIEncoding.DW_ATE_float, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.f64:
                            diType = new DIBasicType("f64", 64, 64, DIEncoding.DW_ATE_float, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.@bool:
                            diType = new DIBasicType("bool", 1, 8, DIEncoding.DW_ATE_boolean, DW_TAG.DW_TAG_base_type);
                            break;
                        case PrimitiveTypes.@void:
                            diType = new DIBasicType("void", 8, 8, DIEncoding.DW_ATE_address, DW_TAG.DW_TAG_base_type);
                            break;
                    }
                }

                if (diType != null)
                    mTypes[type] = diType;
                else
                    throw new NotSupportedException();
            }

            return mTypes[type];
        }

        internal int GetIdent(DIMetadata metadata) 
        {
            if (!mIdents.ContainsKey(metadata))
                mIdents[metadata] = mIdents.Count;

            return mIdents[metadata];
        }

        internal string GetReference(DIMetadata metadata)
        {
            if (!mReferences.ContainsKey(metadata))
                mReferences[metadata] = $"!{GetIdent(metadata)}";

            return mReferences[metadata];
        }

        internal string Flush()
        {
            var sb = new StringBuilder();

            // 写入头
            //sb.Append(DbgHeader.Parse(this));
            //sb.AppendLine();

            // 写入其他元数据
            foreach(var metadata in mReferences)
                sb.AppendLine($"{metadata.Value}={metadata.Key}");

            return sb.ToString();
        }
    }
}
