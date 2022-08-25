// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

namespace IlOptimizer
{
    /// <summary>Defines the types and counts of data that was processed by the tool.</summary>
    public struct ProcessedData
    {
        /// <summary>The number of modules that were processed.</summary>
        public int ModuleCount { get; set; }

        /// <summary>The number of types that were processed.</summary>
        public int TypeCount { get; set; }

        /// <summary>The number of events that were processed.</summary>
        public int EventCount { get; set; }

        /// <summary>The number of properties that were processed.</summary>
        public int PropertyCount { get; set; }

        /// <summary>The number of methods that were processed.</summary>
        public int MethodCount { get; set; }
    }
}
