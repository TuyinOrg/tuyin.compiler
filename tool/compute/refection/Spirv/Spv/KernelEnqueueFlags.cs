using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class KernelEnqueueFlags : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            NoWait = 0,
            [Capability(Capability.Enumerant.Kernel)]
            WaitKernel = 1,
            [Capability(Capability.Enumerant.Kernel)]
            WaitWorkGroup = 2,
        }

        #region NoWait
        public static NoWaitImpl NoWait()
        {
            return NoWaitImpl.Instance;
            
        }

        internal class NoWaitImpl: KernelEnqueueFlags
        {
            public static readonly NoWaitImpl Instance = new NoWaitImpl();
        
            private  NoWaitImpl()
            {
            }
            public override Enumerant Value => KernelEnqueueFlags.Enumerant.NoWait;
            public new static NoWaitImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoWaitImpl object.</summary>
            /// <returns>A string that represents the NoWaitImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" KernelEnqueueFlags.NoWait()";
            }
        }
        #endregion //NoWait

        #region WaitKernel
        public static WaitKernelImpl WaitKernel()
        {
            return WaitKernelImpl.Instance;
            
        }

        internal class WaitKernelImpl: KernelEnqueueFlags
        {
            public static readonly WaitKernelImpl Instance = new WaitKernelImpl();
        
            private  WaitKernelImpl()
            {
            }
            public override Enumerant Value => KernelEnqueueFlags.Enumerant.WaitKernel;
            public new static WaitKernelImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WaitKernelImpl object.</summary>
            /// <returns>A string that represents the WaitKernelImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" KernelEnqueueFlags.WaitKernel()";
            }
        }
        #endregion //WaitKernel

        #region WaitWorkGroup
        public static WaitWorkGroupImpl WaitWorkGroup()
        {
            return WaitWorkGroupImpl.Instance;
            
        }

        internal class WaitWorkGroupImpl: KernelEnqueueFlags
        {
            public static readonly WaitWorkGroupImpl Instance = new WaitWorkGroupImpl();
        
            private  WaitWorkGroupImpl()
            {
            }
            public override Enumerant Value => KernelEnqueueFlags.Enumerant.WaitWorkGroup;
            public new static WaitWorkGroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WaitWorkGroupImpl object.</summary>
            /// <returns>A string that represents the WaitWorkGroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" KernelEnqueueFlags.WaitWorkGroup()";
            }
        }
        #endregion //WaitWorkGroup

        public abstract Enumerant Value { get; }

        public static KernelEnqueueFlags Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.NoWait :
                    return NoWaitImpl.Parse(reader, wordCount - 1);
                case Enumerant.WaitKernel :
                    return WaitKernelImpl.Parse(reader, wordCount - 1);
                case Enumerant.WaitWorkGroup :
                    return WaitWorkGroupImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown KernelEnqueueFlags "+id);
            }
        }
        
        public static KernelEnqueueFlags ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<KernelEnqueueFlags> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<KernelEnqueueFlags>();
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
            return 1;
        }

        public virtual void Write(WordWriter writer)
        {
            writer.WriteWord((uint)Value);
        }
    }
}