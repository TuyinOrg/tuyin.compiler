using System;
using System.Collections.Generic;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal class TypeStructureField : IComparable<TypeStructureField>
    {
        public TypeStructureField()
        {
        }

        public TypeStructureField(SpirvTypeBase type, string name = null)
        {
            Type = type;
            Name = name;
        }

        public BuiltIn BuiltIn { get; set; }

        public SpirvTypeBase Type { get; set; }

        public string Name { get; set; }

        public uint? ByteOffset { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public int CompareTo(TypeStructureField other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Nullable.Compare(ByteOffset, other.ByteOffset);
        }

        public void SetUpDecoration(Decoration decoration, SpirvInstructionTreeBuilder treeBuilder)
        {
            AddDecoration(decoration);
        }

        protected void AddDecoration(Decoration decoration)
        {
            switch (decoration.Value)
            {
                case Decoration.Enumerant.BuiltIn:
                    BuiltIn = ((Decoration.BuiltInImpl) decoration).BuiltIn;
                    break;
                case Decoration.Enumerant.Location:
                    Location = ((Decoration.LocationImpl) decoration).Location;
                    break;
                case Decoration.Enumerant.Component:
                    Component = ((Decoration.ComponentImpl) decoration).Component;
                    break;
                case Decoration.Enumerant.Offset:
                    ByteOffset = ((Decoration.OffsetImpl) decoration).ByteOffset;
                    break;
                case Decoration.Enumerant.RowMajor:
                    RowMajor = true;
                    break;
                case Decoration.Enumerant.ColMajor:
                    ColMajor = true;
                    break;
                case Decoration.Enumerant.MatrixStride:
                    MatrixStride = ((Decoration.MatrixStrideImpl) decoration).MatrixStride;
                    break;
                case Decoration.Enumerant.NoPerspective:
                    NoPerspective = true;
                    break;
                case Decoration.Enumerant.Flat:
                    Flat = true;
                    break;
                case Decoration.Enumerant.Patch:
                    Patch = true;
                    break;
                case Decoration.Enumerant.Centroid:
                    Centroid = true;
                    break;
                case Decoration.Enumerant.Sample:
                    Sample = true;
                    break;
                case Decoration.Enumerant.Volatile:
                    Volatile = true;
                    break;
                case Decoration.Enumerant.Coherent:
                    Coherent = true;
                    break;
                case Decoration.Enumerant.NonWritable:
                    NonWritable = true;
                    break;
                case Decoration.Enumerant.NonReadable:
                    NonReadable = true;
                    break;
                case Decoration.Enumerant.Uniform:
                    Uniform = true;
                    break;
                case Decoration.Enumerant.RelaxedPrecision:
                    RelaxedPrecision = true;
                    break;
                case Decoration.Enumerant.Invariant:
                    Invariant = true;
                    break;
                //case Decoration.Enumerant.Stream:
                //    Stream = ((Decoration.Stream)decoration).StreamNumber;
                //    break;
                //case Decoration.Enumerant.Component:
                //    Component = ((Decoration.Component)decoration).ComponentValue;
                //    break;
                //case Decoration.Enumerant.XfbBuffer:
                //    XfbBuffer = ((Decoration.XfbBuffer)decoration).XFBBufferNumber;
                //    break;
                default:
                    throw new NotImplementedException("Member decoration instruction " + decoration.Value +
                                                      " not yet implemented by " + this.GetType().Name + " class.");
            }
        }

        public bool Invariant { get; set; }

        public uint? Component { get; set; }

        public bool RelaxedPrecision { get; set; }

        public uint? Location { get; set; }

        public bool Uniform { get; set; }

        public bool NonReadable { get; set; }

        public bool NonWritable { get; set; }

        public bool Coherent { get; set; }

        public bool Volatile { get; set; }

        public bool Sample { get; set; }

        public bool Centroid { get; set; }

        public bool Patch { get; set; }

        public bool Flat { get; set; }

        public bool NoPerspective { get; set; }

        public uint? MatrixStride { get; set; }

        public bool ColMajor { get; set; }

        public bool RowMajor { get; set; }

        public IEnumerable<Node> BuildDecorations(TypeStruct typeStruct, uint index)
        {
            if (Name != null) yield return new MemberName() {Type = typeStruct, Member = index, Name = Name};
            if (BuiltIn != null) yield return new MemberDecorate() {StructureType = typeStruct, Member = index, Decoration = Decoration.BuiltIn(BuiltIn)};
            if (ByteOffset != null) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Offset(ByteOffset.Value) };
            if (RowMajor) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.RowMajor() };
            if (ColMajor) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.ColMajor() };
            if (MatrixStride != null) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.MatrixStride(MatrixStride.Value) };
            if (Location != null) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Location(Location.Value) };
            if (Component != null) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Location(Component.Value) };
            if (NoPerspective) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.NoPerspective() };
            if (Flat) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Flat() };
            if (Patch) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Patch() };
            if (Centroid) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Centroid() };
            if (Sample) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Sample() };
            if (Volatile) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Volatile() };
            if (Coherent) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Coherent() };
            if (NonWritable) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.NonWritable() };
            if (NonReadable) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.NonReadable() };
            if (Uniform) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Uniform() };
            if (RelaxedPrecision) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.RelaxedPrecision() };
            if (Invariant) yield return new MemberDecorate() { StructureType = typeStruct, Member = index, Decoration = Decoration.Invariant() };
        }

        public TypeStructureField WithDecoration(Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }
    }
}