using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class RayFlags : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            NoneKHR = 0x0000,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            OpaqueKHR = 0x0001,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            NoOpaqueKHR = 0x0002,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            TerminateOnFirstHitKHR = 0x0004,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            SkipClosestHitShaderKHR = 0x0008,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CullBackFacingTrianglesKHR = 0x0010,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CullFrontFacingTrianglesKHR = 0x0020,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CullOpaqueKHR = 0x0040,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CullNoOpaqueKHR = 0x0080,
            [Capability(Capability.Enumerant.RayTraversalPrimitiveCullingProvisionalKHR)]
            SkipTrianglesKHR = 0x0100,
            [Capability(Capability.Enumerant.RayTraversalPrimitiveCullingProvisionalKHR)]
            SkipAABBsKHR = 0x0200,
        }

        public RayFlags(Enumerant value)
        {
            Value = value;
        }

        public static RayFlags CreateNoneKHR()
        {
            return new RayFlags(Enumerant.NoneKHR)
            {
            };
        }

        public RayFlags AlsoNoneKHR()
        {
            Value |= Enumerant.NoneKHR;
            return this;
        }

        public static RayFlags CreateOpaqueKHR()
        {
            return new RayFlags(Enumerant.OpaqueKHR)
            {
            };
        }

        public RayFlags AlsoOpaqueKHR()
        {
            Value |= Enumerant.OpaqueKHR;
            return this;
        }

        public static RayFlags CreateNoOpaqueKHR()
        {
            return new RayFlags(Enumerant.NoOpaqueKHR)
            {
            };
        }

        public RayFlags AlsoNoOpaqueKHR()
        {
            Value |= Enumerant.NoOpaqueKHR;
            return this;
        }

        public static RayFlags CreateTerminateOnFirstHitKHR()
        {
            return new RayFlags(Enumerant.TerminateOnFirstHitKHR)
            {
            };
        }

        public RayFlags AlsoTerminateOnFirstHitKHR()
        {
            Value |= Enumerant.TerminateOnFirstHitKHR;
            return this;
        }

        public static RayFlags CreateSkipClosestHitShaderKHR()
        {
            return new RayFlags(Enumerant.SkipClosestHitShaderKHR)
            {
            };
        }

        public RayFlags AlsoSkipClosestHitShaderKHR()
        {
            Value |= Enumerant.SkipClosestHitShaderKHR;
            return this;
        }

        public static RayFlags CreateCullBackFacingTrianglesKHR()
        {
            return new RayFlags(Enumerant.CullBackFacingTrianglesKHR)
            {
            };
        }

        public RayFlags AlsoCullBackFacingTrianglesKHR()
        {
            Value |= Enumerant.CullBackFacingTrianglesKHR;
            return this;
        }

        public static RayFlags CreateCullFrontFacingTrianglesKHR()
        {
            return new RayFlags(Enumerant.CullFrontFacingTrianglesKHR)
            {
            };
        }

        public RayFlags AlsoCullFrontFacingTrianglesKHR()
        {
            Value |= Enumerant.CullFrontFacingTrianglesKHR;
            return this;
        }

        public static RayFlags CreateCullOpaqueKHR()
        {
            return new RayFlags(Enumerant.CullOpaqueKHR)
            {
            };
        }

        public RayFlags AlsoCullOpaqueKHR()
        {
            Value |= Enumerant.CullOpaqueKHR;
            return this;
        }

        public static RayFlags CreateCullNoOpaqueKHR()
        {
            return new RayFlags(Enumerant.CullNoOpaqueKHR)
            {
            };
        }

        public RayFlags AlsoCullNoOpaqueKHR()
        {
            Value |= Enumerant.CullNoOpaqueKHR;
            return this;
        }

        public static RayFlags CreateSkipTrianglesKHR()
        {
            return new RayFlags(Enumerant.SkipTrianglesKHR)
            {
            };
        }

        public RayFlags AlsoSkipTrianglesKHR()
        {
            Value |= Enumerant.SkipTrianglesKHR;
            return this;
        }

        public static RayFlags CreateSkipAABBsKHR()
        {
            return new RayFlags(Enumerant.SkipAABBsKHR)
            {
            };
        }

        public RayFlags AlsoSkipAABBsKHR()
        {
            Value |= Enumerant.SkipAABBsKHR;
            return this;
        }


        public static implicit operator RayFlags(RayFlags.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new RayFlags(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static RayFlags Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new RayFlags(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static RayFlags ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<RayFlags> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<RayFlags>();
            while (reader.Position < end)
            {
                res.Add(Parse(reader, end-reader.Position));
            }
            return res;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual uint GetWordCount()
        {
            uint wordCount = 1;
            return wordCount;
        }

        public void Write(WordWriter writer)
        {
             writer.WriteWord((uint)Value);
        }
    }
}