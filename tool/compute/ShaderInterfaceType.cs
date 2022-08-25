using compute.vulkan;
using System;
using System.Collections.Generic;

namespace compute
{
    public class ShaderInterfaceType                                    
    {
        public static readonly ShaderInterfaceType Void = new ShaderInterfaceType("void", 8);

        public static readonly ShaderInterfaceType SByte = new ShaderInterfaceType("sbyte", 1);
        public static readonly ShaderInterfaceType Byte = new ShaderInterfaceType("byte", 1);
        public static readonly ShaderInterfaceType Short = new ShaderInterfaceType("short", 2);
        public static readonly ShaderInterfaceType Ushort = new ShaderInterfaceType("ushort", 2);
        public static readonly ShaderInterfaceType Int = new ShaderInterfaceType("int", 4);
        public static readonly ShaderInterfaceType Uint = new ShaderInterfaceType("uint", 4);
        public static readonly ShaderInterfaceType Long = new ShaderInterfaceType("long", 8);
        public static readonly ShaderInterfaceType Ulong = new ShaderInterfaceType("ulong", 8);

        public static readonly ShaderInterfaceType Float = new ShaderInterfaceType("float", 4);
        public static readonly ShaderInterfaceType Double = new ShaderInterfaceType("double", 8);

        static readonly FormatTree sIntFormatTree = new FormatTree(Format.Undefined);
        static readonly FormatTree sUintFormatTree = new FormatTree(Format.Undefined);
        static readonly FormatTree sSFloatFormatTree = new FormatTree(Format.Undefined);
        static readonly Dictionary<Format, int> sFormatSize = new Dictionary<Format, int>();
        static readonly Dictionary<Format, FormatTreeType> sFormatType = new Dictionary<Format, FormatTreeType>();

        static ShaderInterfaceType() 
        {
            sIntFormatTree.Add(1, Format.R8SInt).Add(1, Format.R8G8SInt).Add(1, Format.R8G8B8SInt).Add(1, Format.R8G8B8A8SInt);
            sIntFormatTree.Add(2, Format.R16SInt).Add(2, Format.R16G16SInt).Add(2, Format.R16G16B16SInt).Add(2, Format.R16G16B16A16SInt);
            sIntFormatTree.Add(4, Format.R32SInt).Add(4, Format.R32G32SInt).Add(4, Format.R32G32B32SInt).Add(4, Format.R32G32B32A32SInt);
            sIntFormatTree.Add(8, Format.R64SInt).Add(8, Format.R64G64SInt).Add(8, Format.R64G64B64SInt).Add(8, Format.R64G64B64A64SInt);

            sUintFormatTree.Add(1, Format.R8UInt).Add(1, Format.R8G8UInt).Add(1, Format.R8G8B8UInt).Add(1, Format.R8G8B8A8UInt);
            sUintFormatTree.Add(2, Format.R16UInt).Add(2, Format.R16G16UInt).Add(2, Format.R16G16B16UInt).Add(2, Format.R16G16B16A16UInt);
            sUintFormatTree.Add(4, Format.R32UInt).Add(4, Format.R32G32UInt).Add(4, Format.R32G32B32UInt).Add(4, Format.R32G32B32A32UInt);
            sUintFormatTree.Add(8, Format.R64UInt).Add(8, Format.R64G64UInt).Add(8, Format.R64G64B64UInt).Add(8, Format.R64G64B64A64UInt);

            sSFloatFormatTree.Add(2, Format.R16SFloat).Add(2, Format.R16G16SFloat).Add(2, Format.R16G16B16SFloat).Add(2, Format.R16G16B16A16SFloat);
            sSFloatFormatTree.Add(4, Format.R32SFloat).Add(4, Format.R32G32SFloat).Add(4, Format.R32G32B32SFloat).Add(4, Format.R32G32B32A32SFloat);
            sSFloatFormatTree.Add(8, Format.R64SFloat).Add(8, Format.R64G64SFloat).Add(8, Format.R64G64B64SFloat).Add(8, Format.R64G64B64A64SFloat);

            sFormatSize.Add(Format.R8SInt, 1);
            sFormatSize.Add(Format.R8G8SInt, 2);
            sFormatSize.Add(Format.R8G8B8SInt, 3);
            sFormatSize.Add(Format.R8G8B8A8SInt, 4);

            sFormatSize.Add(Format.R16SInt, 2);
            sFormatSize.Add(Format.R16G16SInt, 4);
            sFormatSize.Add(Format.R16G16B16SInt, 6);
            sFormatSize.Add(Format.R16G16B16A16SInt, 8);

            sFormatSize.Add(Format.R32SInt, 4);
            sFormatSize.Add(Format.R32G32SInt, 8);
            sFormatSize.Add(Format.R32G32B32SInt, 12);
            sFormatSize.Add(Format.R32G32B32A32SInt, 16);

            sFormatSize.Add(Format.R64SInt, 8);
            sFormatSize.Add(Format.R64G64SInt, 16);
            sFormatSize.Add(Format.R64G64B64SInt, 24);
            sFormatSize.Add(Format.R64G64B64A64SInt, 32);

            sFormatSize.Add(Format.R8UInt, 1);
            sFormatSize.Add(Format.R8G8UInt, 2);
            sFormatSize.Add(Format.R8G8B8UInt, 3);
            sFormatSize.Add(Format.R8G8B8A8UInt, 4);

            sFormatSize.Add(Format.R16UInt, 2);
            sFormatSize.Add(Format.R16G16UInt, 4);
            sFormatSize.Add(Format.R16G16B16UInt, 6);
            sFormatSize.Add(Format.R16G16B16A16UInt, 8);

            sFormatSize.Add(Format.R32UInt, 4);
            sFormatSize.Add(Format.R32G32UInt, 8);
            sFormatSize.Add(Format.R32G32B32UInt, 12);
            sFormatSize.Add(Format.R32G32B32A32UInt, 16);

            sFormatSize.Add(Format.R64UInt, 8);
            sFormatSize.Add(Format.R64G64UInt, 16);
            sFormatSize.Add(Format.R64G64B64UInt, 24);
            sFormatSize.Add(Format.R64G64B64A64UInt, 32);

            sFormatSize.Add(Format.R16SFloat, 2);
            sFormatSize.Add(Format.R16G16SFloat, 4);
            sFormatSize.Add(Format.R16G16B16SFloat, 6);
            sFormatSize.Add(Format.R16G16B16A16SFloat, 8);

            sFormatSize.Add(Format.R32SFloat, 4);
            sFormatSize.Add(Format.R32G32SFloat, 8);
            sFormatSize.Add(Format.R32G32B32SFloat, 12);
            sFormatSize.Add(Format.R32G32B32A32SFloat, 16);

            sFormatSize.Add(Format.R64SFloat, 8);
            sFormatSize.Add(Format.R64G64SFloat, 16);
            sFormatSize.Add(Format.R64G64B64SFloat, 24);
            sFormatSize.Add(Format.R64G64B64A64SFloat, 32);

            sFormatType.Add(Format.R8SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R8G8SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R8G8B8SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R8G8B8A8SInt, FormatTreeType.SInt);

            sFormatType.Add(Format.R16SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R16G16SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R16G16B16SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R16G16B16A16SInt, FormatTreeType.SInt);

            sFormatType.Add(Format.R32SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R32G32SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R32G32B32SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R32G32B32A32SInt, FormatTreeType.SInt);

            sFormatType.Add(Format.R64SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R64G64SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R64G64B64SInt, FormatTreeType.SInt);
            sFormatType.Add(Format.R64G64B64A64SInt, FormatTreeType.SInt);

            sFormatType.Add(Format.R8UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R8G8UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R8G8B8UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R8G8B8A8UInt, FormatTreeType.UInt);

            sFormatType.Add(Format.R16UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R16G16UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R16G16B16UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R16G16B16A16UInt, FormatTreeType.UInt);

            sFormatType.Add(Format.R32UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R32G32UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R32G32B32UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R32G32B32A32UInt, FormatTreeType.UInt);

            sFormatType.Add(Format.R64UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R64G64UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R64G64B64UInt, FormatTreeType.UInt);
            sFormatType.Add(Format.R64G64B64A64UInt, FormatTreeType.UInt);

            sFormatType.Add(Format.R16SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R16G16SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R16G16B16SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R16G16B16A16SFloat, FormatTreeType.SFloat);

            sFormatType.Add(Format.R32SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R32G32SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R32G32B32SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R32G32B32A32SFloat, FormatTreeType.SFloat);

            sFormatType.Add(Format.R64SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R64G64SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R64G64B64SFloat, FormatTreeType.SFloat);
            sFormatType.Add(Format.R64G64B64A64SFloat, FormatTreeType.SFloat);
        }

        public string Name { get; }

        public uint Size { get; }

        public Field[] Fields { get; }

        public ShaderInterfaceType(string name, uint size, params Field[] fields)
        {
            Name = name;
            Size = size;
            Fields = fields;
        }

        public class Pointer : ShaderInterfaceType 
        {
            public Pointer(ShaderInterfaceType type)
                : base(type.Name + "*", 8)
            {
            }
        }

        public struct Field
        {
            public Field(ShaderInterfaceType type, string name, uint offset)
            {
                Type = type;
                Name = name;
                Offset = offset;
            }

            public ShaderInterfaceType Type { get; }

            public string Name { get; }

            public uint Offset { get; }
        }

        internal Format GetFormat() 
        {
            if (Fields.Length == 0)
            {
                if (this == Float)
                    return Format.R32SFloat;

                if (this == Double)
                    return Format.R64SFloat;

                switch (Size) 
                {
                    case 1: return Format.R8SInt;
                    case 2: return Format.R16SInt;
                    case 4: return Format.R32SInt;
                    case 8: return Format.R64SInt;
                }

                throw new NotSupportedException($"Unknown type {Name}'s format.");
            }
            else 
            {
                FormatTree tree = null;
                for (var i = 0; i < Fields.Length; i++)
                {
                    var field = Fields[i];
                    var format = field.Type.GetFormat();

                    if (tree == null) 
                    {
                        if(!sFormatType.ContainsKey(format))
                            throw new NotSupportedException($"Unknown format '{Enum.GetName(typeof(Format), format)}' of type '{field.Type}'.");

                        var treeType = sFormatType[format];
                        switch (treeType)
                        {
                            case FormatTreeType.SInt:
                                tree = sIntFormatTree;
                                break;
                            case FormatTreeType.UInt:
                                tree = sUintFormatTree;
                                break;
                            case FormatTreeType.SFloat:
                                tree = sSFloatFormatTree;
                                break;
                        }
                    }

                    if(!sFormatSize.ContainsKey(format))
                        throw new NotSupportedException($"Unknown format '{Enum.GetName(typeof(Format), format)}' of type '{field.Type}'.");

                    var temp = tree[sFormatSize[format]];
                    if(temp == null)
                        throw new NotSupportedException($"The specified subsequent format '{Enum.GetName(typeof(Format), format)}' does not exist after '{Enum.GetName(typeof(Format), tree.Format)}'.");

                    tree = temp;
                }

                return tree.Format;
            }
        }

        enum FormatTreeType 
        {
            SInt,
            UInt,
            SFloat
        }

        class FormatTree
        {
            private Dictionary<int, FormatTree> mChildrens = new Dictionary<int, FormatTree>();

            public FormatTree this[int size] 
            {
                get 
                {
                    if (mChildrens.ContainsKey(size))
                        return mChildrens[size];

                    return null;
                }
            }

            public Format Format { get; }

            public FormatTree(Format format)
            {
                Format = format;
            }

            public FormatTree Add(int size, Format format) 
            {
                var tree = new FormatTree(format);
                mChildrens.Add(size, tree);
                return tree;
            }
        }
    }
}
