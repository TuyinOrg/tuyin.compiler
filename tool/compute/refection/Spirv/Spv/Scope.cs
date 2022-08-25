using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class Scope : ValueEnum
    {
        public enum Enumerant
        {
            CrossDevice = 0,
            Device = 1,
            Workgroup = 2,
            Subgroup = 3,
            Invocation = 4,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            QueueFamily = 5,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            QueueFamilyKHR = 5,
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ShaderCallKHR = 6,
        }

        #region CrossDevice
        public static CrossDeviceImpl CrossDevice()
        {
            return CrossDeviceImpl.Instance;
            
        }

        public class CrossDeviceImpl: Scope
        {
            public static readonly CrossDeviceImpl Instance = new CrossDeviceImpl();
        
            private  CrossDeviceImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.CrossDevice;
            public new static CrossDeviceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CrossDeviceImpl object.</summary>
            /// <returns>A string that represents the CrossDeviceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.CrossDevice()";
            }
        }
        #endregion //CrossDevice

        #region Device
        public static DeviceImpl Device()
        {
            return DeviceImpl.Instance;
            
        }

        public class DeviceImpl: Scope
        {
            public static readonly DeviceImpl Instance = new DeviceImpl();
        
            private  DeviceImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.Device;
            public new static DeviceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DeviceImpl object.</summary>
            /// <returns>A string that represents the DeviceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.Device()";
            }
        }
        #endregion //Device

        #region Workgroup
        public static WorkgroupImpl Workgroup()
        {
            return WorkgroupImpl.Instance;
            
        }

        public class WorkgroupImpl: Scope
        {
            public static readonly WorkgroupImpl Instance = new WorkgroupImpl();
        
            private  WorkgroupImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.Workgroup;
            public new static WorkgroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorkgroupImpl object.</summary>
            /// <returns>A string that represents the WorkgroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.Workgroup()";
            }
        }
        #endregion //Workgroup

        #region Subgroup
        public static SubgroupImpl Subgroup()
        {
            return SubgroupImpl.Instance;
            
        }

        public class SubgroupImpl: Scope
        {
            public static readonly SubgroupImpl Instance = new SubgroupImpl();
        
            private  SubgroupImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.Subgroup;
            public new static SubgroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupImpl object.</summary>
            /// <returns>A string that represents the SubgroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.Subgroup()";
            }
        }
        #endregion //Subgroup

        #region Invocation
        public static InvocationImpl Invocation()
        {
            return InvocationImpl.Instance;
            
        }

        public class InvocationImpl: Scope
        {
            public static readonly InvocationImpl Instance = new InvocationImpl();
        
            private  InvocationImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.Invocation;
            public new static InvocationImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InvocationImpl object.</summary>
            /// <returns>A string that represents the InvocationImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.Invocation()";
            }
        }
        #endregion //Invocation

        #region QueueFamily
        public static QueueFamilyImpl QueueFamily()
        {
            return QueueFamilyImpl.Instance;
            
        }

        public class QueueFamilyImpl: Scope
        {
            public static readonly QueueFamilyImpl Instance = new QueueFamilyImpl();
        
            private  QueueFamilyImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.QueueFamily;
            public new static QueueFamilyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the QueueFamilyImpl object.</summary>
            /// <returns>A string that represents the QueueFamilyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.QueueFamily()";
            }
        }
        #endregion //QueueFamily

        #region QueueFamilyKHR
        public static QueueFamilyKHRImpl QueueFamilyKHR()
        {
            return QueueFamilyKHRImpl.Instance;
            
        }

        public class QueueFamilyKHRImpl: Scope
        {
            public static readonly QueueFamilyKHRImpl Instance = new QueueFamilyKHRImpl();
        
            private  QueueFamilyKHRImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.QueueFamilyKHR;
            public new static QueueFamilyKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the QueueFamilyKHRImpl object.</summary>
            /// <returns>A string that represents the QueueFamilyKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.QueueFamilyKHR()";
            }
        }
        #endregion //QueueFamilyKHR

        #region ShaderCallKHR
        public static ShaderCallKHRImpl ShaderCallKHR()
        {
            return ShaderCallKHRImpl.Instance;
            
        }

        public class ShaderCallKHRImpl: Scope
        {
            public static readonly ShaderCallKHRImpl Instance = new ShaderCallKHRImpl();
        
            private  ShaderCallKHRImpl()
            {
            }
            public override Enumerant Value => Scope.Enumerant.ShaderCallKHR;
            public new static ShaderCallKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderCallKHRImpl object.</summary>
            /// <returns>A string that represents the ShaderCallKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Scope.ShaderCallKHR()";
            }
        }
        #endregion //ShaderCallKHR

        public abstract Enumerant Value { get; }

        public static Scope Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.CrossDevice :
                    return CrossDeviceImpl.Parse(reader, wordCount - 1);
                case Enumerant.Device :
                    return DeviceImpl.Parse(reader, wordCount - 1);
                case Enumerant.Workgroup :
                    return WorkgroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.Subgroup :
                    return SubgroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.Invocation :
                    return InvocationImpl.Parse(reader, wordCount - 1);
                case Enumerant.QueueFamily :
                    return QueueFamilyImpl.Parse(reader, wordCount - 1);
                //QueueFamilyKHR has the same id as another value in enum.
                //case Enumerant.QueueFamilyKHR :
                //    return QueueFamilyKHR.Parse(reader, wordCount - 1);
                case Enumerant.ShaderCallKHR :
                    return ShaderCallKHRImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown Scope "+id);
            }
        }
        
        public static Scope ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<Scope> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<Scope>();
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