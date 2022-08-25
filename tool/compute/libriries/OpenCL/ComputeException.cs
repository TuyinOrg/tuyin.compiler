#region License

/*

Copyright (c) 2009 - 2013 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

namespace OpenCL
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents an error state that occurred while executing an OpenCL API call.
    /// </summary>
    /// <seealso cref="ComputeErrorCode"/>
    class ComputeException : ApplicationException
    {
        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ComputeErrorCode _code;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="ComputeErrorCode"/> of the <see cref="ComputeException"/>.
        /// </summary>
        public ComputeErrorCode ComputeErrorCode => _code;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeException"/> with a specified <see cref="ComputeErrorCode"/>.
        /// </summary>
        /// <param name="code"> A <see cref="ComputeErrorCode"/>. </param>
        public ComputeException(ComputeErrorCode code)
            : base("OpenCL error code detected: " + code.ToString() + ".")
        {
            _code = code;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Checks for an OpenCL error code and throws a <see cref="ComputeException"/> if such is encountered.
        /// </summary>
        /// <param name="errorCode"> The value to be checked for an OpenCL error. </param>
        public static void ThrowOnError(int errorCode)
        {
            ThrowOnError((ComputeErrorCode)errorCode);
        }

        /// <summary>
        /// Checks for an OpenCL error code and throws a <see cref="ComputeException"/> if such is encountered.
        /// </summary>
        /// <param name="errorCode"> The OpenCL error code. </param>
        public static void ThrowOnError(ComputeErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ComputeErrorCode.Success:
                    return;

                case ComputeErrorCode.DeviceNotFound:
                    throw new DeviceNotFoundComputeException();

                case ComputeErrorCode.DeviceNotAvailable:
                    throw new DeviceNotAvailableComputeException();

                case ComputeErrorCode.CompilerNotAvailable:
                    throw new CompilerNotAvailableComputeException();

                case ComputeErrorCode.MemoryObjectAllocationFailure:
                    throw new MemoryObjectAllocationFailureComputeException();

                case ComputeErrorCode.OutOfResources:
                    throw new OutOfResourcesComputeException();

                case ComputeErrorCode.OutOfHostMemory:
                    throw new OutOfHostMemoryComputeException();

                case ComputeErrorCode.ProfilingInfoNotAvailable:
                    throw new ProfilingInfoNotAvailableComputeException();

                case ComputeErrorCode.MemoryCopyOverlap:
                    throw new MemoryCopyOverlapComputeException();

                case ComputeErrorCode.ImageFormatMismatch:
                    throw new ImageFormatMismatchComputeException();

                case ComputeErrorCode.ImageFormatNotSupported:
                    throw new ImageFormatNotSupportedComputeException();

                case ComputeErrorCode.BuildProgramFailure:
                    throw new BuildProgramFailureComputeException();

                case ComputeErrorCode.MapFailure:
                    throw new MapFailureComputeException();

                case ComputeErrorCode.InvalidValue:
                    throw new InvalidValueComputeException();

                case ComputeErrorCode.InvalidDeviceType:
                    throw new InvalidDeviceTypeComputeException();

                case ComputeErrorCode.InvalidPlatform:
                    throw new InvalidPlatformComputeException();

                case ComputeErrorCode.InvalidDevice:
                    throw new InvalidDeviceComputeException();

                case ComputeErrorCode.InvalidContext:
                    throw new InvalidContextComputeException();

                case ComputeErrorCode.InvalidCommandQueueFlags:
                    throw new InvalidCommandQueueFlagsComputeException();

                case ComputeErrorCode.InvalidCommandQueue:
                    throw new InvalidCommandQueueComputeException();

                case ComputeErrorCode.InvalidHostPointer:
                    throw new InvalidHostPointerComputeException();

                case ComputeErrorCode.InvalidMemoryObject:
                    throw new InvalidMemoryObjectComputeException();

                case ComputeErrorCode.InvalidImageFormatDescriptor:
                    throw new InvalidImageFormatDescriptorComputeException();

                case ComputeErrorCode.InvalidImageSize:
                    throw new InvalidImageSizeComputeException();

                case ComputeErrorCode.InvalidSampler:
                    throw new InvalidSamplerComputeException();

                case ComputeErrorCode.InvalidBinary:
                    throw new InvalidBinaryComputeException();

                case ComputeErrorCode.InvalidBuildOptions:
                    throw new InvalidBuildOptionsComputeException();

                case ComputeErrorCode.InvalidProgram:
                    throw new InvalidProgramComputeException();

                case ComputeErrorCode.InvalidProgramExecutable:
                    throw new InvalidProgramExecutableComputeException();

                case ComputeErrorCode.InvalidKernelName:
                    throw new InvalidKernelNameComputeException();

                case ComputeErrorCode.InvalidKernelDefinition:
                    throw new InvalidKernelDefinitionComputeException();

                case ComputeErrorCode.InvalidKernel:
                    throw new InvalidKernelComputeException();

                case ComputeErrorCode.InvalidArgumentIndex:
                    throw new InvalidArgumentIndexComputeException();

                case ComputeErrorCode.InvalidArgumentValue:
                    throw new InvalidArgumentValueComputeException();

                case ComputeErrorCode.InvalidArgumentSize:
                    throw new InvalidArgumentSizeComputeException();

                case ComputeErrorCode.InvalidKernelArguments:
                    throw new InvalidKernelArgumentsComputeException();

                case ComputeErrorCode.InvalidWorkDimension:
                    throw new InvalidWorkDimensionsComputeException();

                case ComputeErrorCode.InvalidWorkGroupSize:
                    throw new InvalidWorkGroupSizeComputeException();

                case ComputeErrorCode.InvalidWorkItemSize:
                    throw new InvalidWorkItemSizeComputeException();

                case ComputeErrorCode.InvalidGlobalOffset:
                    throw new InvalidGlobalOffsetComputeException();

                case ComputeErrorCode.InvalidEventWaitList:
                    throw new InvalidEventWaitListComputeException();

                case ComputeErrorCode.InvalidEvent:
                    throw new InvalidEventComputeException();

                case ComputeErrorCode.InvalidOperation:
                    throw new InvalidOperationComputeException();

                case ComputeErrorCode.InvalidGLObject:
                    throw new InvalidGLObjectComputeException();

                case ComputeErrorCode.InvalidBufferSize:
                    throw new InvalidBufferSizeComputeException();

                case ComputeErrorCode.InvalidMipLevel:
                    throw new InvalidMipLevelComputeException();

                default:
                    throw new ComputeException(errorCode);
            }
        }

        #endregion
    }

    #region Exception classes

    // Disable CS1591 warnings (missing XML comment for publicly visible type or member).
    #pragma warning disable 1591

    class DeviceNotFoundComputeException : ComputeException
    { public DeviceNotFoundComputeException() : base(ComputeErrorCode.DeviceNotFound) { } }

    class DeviceNotAvailableComputeException : ComputeException
    { public DeviceNotAvailableComputeException() : base(ComputeErrorCode.DeviceNotAvailable) { } }

    class CompilerNotAvailableComputeException : ComputeException
    { public CompilerNotAvailableComputeException() : base(ComputeErrorCode.CompilerNotAvailable) { } }

    class MemoryObjectAllocationFailureComputeException : ComputeException
    { public MemoryObjectAllocationFailureComputeException() : base(ComputeErrorCode.MemoryObjectAllocationFailure) { } }

    class OutOfResourcesComputeException : ComputeException
    { public OutOfResourcesComputeException() : base(ComputeErrorCode.OutOfResources) { } }

    class OutOfHostMemoryComputeException : ComputeException
    { public OutOfHostMemoryComputeException() : base(ComputeErrorCode.OutOfHostMemory) { } }

    class ProfilingInfoNotAvailableComputeException : ComputeException
    { public ProfilingInfoNotAvailableComputeException() : base(ComputeErrorCode.ProfilingInfoNotAvailable) { } }

    class MemoryCopyOverlapComputeException : ComputeException
    { public MemoryCopyOverlapComputeException() : base(ComputeErrorCode.MemoryCopyOverlap) { } }

    class ImageFormatMismatchComputeException : ComputeException
    { public ImageFormatMismatchComputeException() : base(ComputeErrorCode.ImageFormatMismatch) { } }

    class ImageFormatNotSupportedComputeException : ComputeException
    { public ImageFormatNotSupportedComputeException() : base(ComputeErrorCode.ImageFormatNotSupported) { } }

    class BuildProgramFailureComputeException : ComputeException
    { public BuildProgramFailureComputeException() : base(ComputeErrorCode.BuildProgramFailure) { } }

    class MapFailureComputeException : ComputeException
    { public MapFailureComputeException() : base(ComputeErrorCode.MapFailure) { } }

    class InvalidValueComputeException : ComputeException
    { public InvalidValueComputeException() : base(ComputeErrorCode.InvalidValue) { } }

    class InvalidDeviceTypeComputeException : ComputeException
    { public InvalidDeviceTypeComputeException() : base(ComputeErrorCode.InvalidDeviceType) { } }

    class InvalidPlatformComputeException : ComputeException
    { public InvalidPlatformComputeException() : base(ComputeErrorCode.InvalidPlatform) { } }

    class InvalidDeviceComputeException : ComputeException
    { public InvalidDeviceComputeException() : base(ComputeErrorCode.InvalidDevice) { } }

    class InvalidContextComputeException : ComputeException
    { public InvalidContextComputeException() : base(ComputeErrorCode.InvalidContext) { } }

    class InvalidCommandQueueFlagsComputeException : ComputeException
    { public InvalidCommandQueueFlagsComputeException() : base(ComputeErrorCode.InvalidCommandQueueFlags) { } }

    class InvalidCommandQueueComputeException : ComputeException
    { public InvalidCommandQueueComputeException() : base(ComputeErrorCode.InvalidCommandQueue) { } }

    class InvalidHostPointerComputeException : ComputeException
    { public InvalidHostPointerComputeException() : base(ComputeErrorCode.InvalidHostPointer) { } }

    class InvalidMemoryObjectComputeException : ComputeException
    { public InvalidMemoryObjectComputeException() : base(ComputeErrorCode.InvalidMemoryObject) { } }

    class InvalidImageFormatDescriptorComputeException : ComputeException
    { public InvalidImageFormatDescriptorComputeException() : base(ComputeErrorCode.InvalidImageFormatDescriptor) { } }

    class InvalidImageSizeComputeException : ComputeException
    { public InvalidImageSizeComputeException() : base(ComputeErrorCode.InvalidImageSize) { } }

    class InvalidSamplerComputeException : ComputeException
    { public InvalidSamplerComputeException() : base(ComputeErrorCode.InvalidSampler) { } }

    class InvalidBinaryComputeException : ComputeException
    { public InvalidBinaryComputeException() : base(ComputeErrorCode.InvalidBinary) { } }

    class InvalidBuildOptionsComputeException : ComputeException
    { public InvalidBuildOptionsComputeException() : base(ComputeErrorCode.InvalidBuildOptions) { } }

    class InvalidProgramComputeException : ComputeException
    { public InvalidProgramComputeException() : base(ComputeErrorCode.InvalidProgram) { } }

    class InvalidProgramExecutableComputeException : ComputeException
    { public InvalidProgramExecutableComputeException() : base(ComputeErrorCode.InvalidProgramExecutable) { } }

    class InvalidKernelNameComputeException : ComputeException
    { public InvalidKernelNameComputeException() : base(ComputeErrorCode.InvalidKernelName) { } }

    class InvalidKernelDefinitionComputeException : ComputeException
    { public InvalidKernelDefinitionComputeException() : base(ComputeErrorCode.InvalidKernelDefinition) { } }

    class InvalidKernelComputeException : ComputeException
    { public InvalidKernelComputeException() : base(ComputeErrorCode.InvalidKernel) { } }

    class InvalidArgumentIndexComputeException : ComputeException
    { public InvalidArgumentIndexComputeException() : base(ComputeErrorCode.InvalidArgumentIndex) { } }

    class InvalidArgumentValueComputeException : ComputeException
    { public InvalidArgumentValueComputeException() : base(ComputeErrorCode.InvalidArgumentValue) { } }

    class InvalidArgumentSizeComputeException : ComputeException
    { public InvalidArgumentSizeComputeException() : base(ComputeErrorCode.InvalidArgumentSize) { } }

    class InvalidKernelArgumentsComputeException : ComputeException
    { public InvalidKernelArgumentsComputeException() : base(ComputeErrorCode.InvalidKernelArguments) { } }

    class InvalidWorkDimensionsComputeException : ComputeException
    { public InvalidWorkDimensionsComputeException() : base(ComputeErrorCode.InvalidWorkDimension) { } }

    class InvalidWorkGroupSizeComputeException : ComputeException
    { public InvalidWorkGroupSizeComputeException() : base(ComputeErrorCode.InvalidWorkGroupSize) { } }

    class InvalidWorkItemSizeComputeException : ComputeException
    { public InvalidWorkItemSizeComputeException() : base(ComputeErrorCode.InvalidWorkItemSize) { } }

    class InvalidGlobalOffsetComputeException : ComputeException
    { public InvalidGlobalOffsetComputeException() : base(ComputeErrorCode.InvalidGlobalOffset) { } }

    class InvalidEventWaitListComputeException : ComputeException
    { public InvalidEventWaitListComputeException() : base(ComputeErrorCode.InvalidEventWaitList) { } }

    class InvalidEventComputeException : ComputeException
    { public InvalidEventComputeException() : base(ComputeErrorCode.InvalidEvent) { } }

    class InvalidOperationComputeException : ComputeException
    { public InvalidOperationComputeException() : base(ComputeErrorCode.InvalidOperation) { } }

    class InvalidGLObjectComputeException : ComputeException
    { public InvalidGLObjectComputeException() : base(ComputeErrorCode.InvalidGLObject) { } }

    class InvalidBufferSizeComputeException : ComputeException
    { public InvalidBufferSizeComputeException() : base(ComputeErrorCode.InvalidBufferSize) { } }

    class InvalidMipLevelComputeException : ComputeException
    { public InvalidMipLevelComputeException() : base(ComputeErrorCode.InvalidMipLevel) { } }

    #endregion
}