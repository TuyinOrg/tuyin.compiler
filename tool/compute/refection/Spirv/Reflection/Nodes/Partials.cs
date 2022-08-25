using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Reflection.Operands;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Constant
    {
        public Constant(NumberLiteral value, string debugName = null):this(value.Type,value, debugName)
        {
        }
    }

    internal partial class Variable
    {
        public static Variable gl_VertexIndex()
        {
            return new Variable(SpirvTypeBase.Int.MakePointer(StorageClass.Input()), StorageClass.Input(), null, "gl_VertexIndex")
                .WithDecoration(Decoration.BuiltIn(BuiltIn.VertexIndex()));
        }
    }

    internal partial class ConstantComposite
    {
        public ConstantComposite(SpirvTypeBase returnType, params Node[] constituents):this(returnType, constituents, null)
        {
        }
    }

    internal partial class FunctionCall
    {
        public FunctionCall(Function function, IEnumerable<Node> arguments, string debugName = null)
            : this(function.ResultType, function, arguments, debugName)
        {
        }

    }

    internal partial class Function
    {
        public Function(Spv.FunctionControl functionControl, TypeFunction functionType, string debugName = null)
        :this(functionType.ReturnType, functionControl, functionType, debugName)
        {
        }


        public IList<FunctionParameter> Parameters { get; } = new List<FunctionParameter>();
    }

    internal partial class MemoryModel
    {
        public static readonly MemoryModel GLSL450 = new MemoryModel(){AddressingModel = Spv.AddressingModel.Logical(), Value = Spv.MemoryModel.GLSL450() };
    }

    internal partial class Variable
    {
        public uint? DescriptorSet { get; set; }

        public uint? BindingPoint { get; set; }

        public uint? Location { get; set; }

        protected override void AddDecoration(Decoration decoration)
        {
            switch (decoration.Value)
            {
                case Decoration.Enumerant.Location:
                    Location = ((Decoration.LocationImpl)decoration).Location;
                    return;
                case Decoration.Enumerant.DescriptorSet:
                    DescriptorSet = ((Decoration.DescriptorSetImpl)decoration).DescriptorSet;
                    return;
                case Decoration.Enumerant.Binding:
                    BindingPoint = ((Decoration.BindingImpl)decoration).BindingPoint;
                    return;
            }
            base.AddDecoration(decoration);
        }

        public override bool RemoveDecoration(Decoration.Enumerant decoration)
        {
            switch (decoration)
            {
                case Decoration.Enumerant.Location:
                    Location = null;
                    return true;
                case Decoration.Enumerant.DescriptorSet:
                    DescriptorSet = null;
                    return true;
                case Decoration.Enumerant.Binding:
                    BindingPoint = null;
                    return true;
            }
            return base.RemoveDecoration(decoration);
        }

        public override IEnumerable<Node> BuildDecorations()
        {
            return base.BuildDecorations()
                .Concat(BuidlLocation())
                .Concat(BuildDescriptorSet())
                .Concat(Binding());
        }

        private IEnumerable<Node> Binding()
        {
            if (BindingPoint != null)
            {
                yield return new Decorate(){Target = this, Decoration = Decoration.Binding(BindingPoint.Value)};
            }
        }

        private IEnumerable<Node> BuildDescriptorSet()
        {
            if (DescriptorSet != null)
            {
                yield return new Decorate() { Target = this, Decoration = Decoration.DescriptorSet(DescriptorSet.Value) };
            }
        }

        private IEnumerable<Node> BuidlLocation()
        {
            if (Location != null)
            {
                yield return new Decorate() { Target = this, Decoration = Decoration.Location(Location.Value) };
            }
        }
    }
}
