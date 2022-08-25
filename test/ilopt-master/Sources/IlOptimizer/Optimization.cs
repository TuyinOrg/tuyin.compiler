// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Immutable;
using System.Collections.Generic;
using Mono.Cecil;

namespace IlOptimizer
{
    /// <summary>Defines an optimization that can be performed by the tool.</summary>
    public sealed class Optimization
    {
        /// <summary>Initializes a new instance of the <see cref="Optimization" /> struct.</summary>
        /// <param name="name">The name of the optimization.</param>
        /// <param name="description">The description of the optimization.</param>
        /// <param name="availableParameters">The parameters available to the optimization.</param>
        /// <param name="optimizeMethod">The method to use when performing the optimization.</param>
        public Optimization(string name, string description, Dictionary<string, string> availableParameters,  Func<MethodDefinition, string, bool?> optimizeMethod)
        {
            Name = name;
            Description = description;
            AvailableParameters = availableParameters.ToImmutableDictionary();
            OptimizeMethod = optimizeMethod;
            Parameter = string.Empty;
        }

        /// <summary>Gets the parameters available to the optimization.</summary>
        public ImmutableDictionary<string, string> AvailableParameters { get; }

        /// <summary>Gets the name of the optimization.</summary>
        public string Name { get; }

        /// <summary>Gets the description of the optimization.</summary>
        public string Description { get; }

        /// <summary>Gets or sets the number of methods that were failed by the optimization.</summary>
        public int FailedMethodCount { get; set; }

        /// <summary>Gets the method to use when performing the optimization.</summary>
        public Func<MethodDefinition, string, bool?> OptimizeMethod { get; }

        /// <summary>Gets or sets the parameter to use when performing the optimization.</summary>
        public string Parameter { get; set; }

        /// <summary>Gets or sets the number of methods that were skipped by the optimization.</summary>
        public int SkippedMethodCount { get; set; }

        /// <summary>Gets or sets the number of methods that were updated by the optimization.</summary>
        public int UpdatedMethodCount { get; set; }
    }
}
