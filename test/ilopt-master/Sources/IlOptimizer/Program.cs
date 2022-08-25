// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using IlOptimizer.Optimizations;
using Mono.Cecil;

namespace IlOptimizer
{
    /// <summary>Provides a set of methods for executing a program.</summary>
    public static class Program
    {
        private static readonly Optimization[] Optimizations = {
            new Optimization(
                name: nameof(StripLocalsInit),
                description: "Strips the 'init' flag from the '.locals' directive for the matching methods.",
                availableParameters: new Dictionary<string, string> {
                    ["all"] = "Skips definite assignment analysis and strips the flag regardless",
                    ["out"] = "Skips definite assignment analysis for C# style 'out' variables",
                    ["stackalloc"] = "Skips definite assignment analysis for C# style 'stackalloc' variables",
                    ["csharp"] = "Combines the 'out' and 'stackalloc' options"
                },
                optimizeMethod: StripLocalsInit.Optimize
            )
        };

        /// <summary>The entry point of the program.</summary>
        /// <param name="args">The arguments that should be used when running the program.</param>
        /// <returns>The exit code of the program.</returns>
        public static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    return PrintHelp();
                }

                var regex = string.Empty;
                var optimizations = new List<Optimization>();
                var assemblyPaths = new List<string>();

                foreach (var arg in args)
                {
                    if (Matches(arg, "?", "h", "help") == string.Empty)
                    {
                        return PrintHelp();
                    }
                    else
                    {
                        var parameter = Matches(arg, "f", "filter");

                        if (parameter != null)
                        {
                            if (parameter == string.Empty)
                            {
                                WriteError("    Error: The 'filter' command was specified but did not contain the expected parameter.");
                                WriteError("           Please use the 'help' command for more details on the expected format.");
                                Exit();
                            }

                            regex = parameter;
                            continue;
                        }

                        foreach (var optimization in Optimizations)
                        {
                            parameter = Matches(arg, optimization.Name);

                            if (parameter != null)
                            {
                                if (parameter != string.Empty)
                                {
                                    if (optimization.AvailableParameters.Count == 0)
                                    {
                                        WriteWarning("    Warning: A parameter was specified but none are expected.");
                                        WriteWarning($"             Optimization: '{arg}'");
                                        WriteWarning($"             Parameter: '{parameter}'");
                                        WriteWarning("    Continuing will ignore this parameter; Otherwise, the process will exit.");

                                        if (!CheckForContinue())
                                        {
                                            Exit();
                                        }
                                    }
                                    else if (optimization.AvailableParameters.Keys.Any((key) => key.Equals(parameter, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        optimization.Parameter = parameter;
                                    }
                                    else
                                    {
                                        WriteError("    Error: An unrecognized parameter was specified.");
                                        WriteError($"           Optimization: '{arg}'");
                                        WriteError($"           Parameter: '{parameter}'");
                                        WriteError("    Please use the 'help' command for more details on the accepted parameters.");
                                        Exit();
                                    }
                                }

                                optimizations.Add(optimization);
                                break;
                            }
                        }

                        if (parameter is null)
                        {
                            var path = Path.GetFullPath(arg);

                            if (!File.Exists(arg))
                            {
                                WriteWarning("    Warning: A specified assembly could not be located.");
                                WriteWarning($"             Argument: '{arg}'");
                                WriteWarning($"             Resolved Path: '{path}'");
                                WriteWarning("    Continuing will ignore this argument; Otherwise, the process will exit.");

                                if (!CheckForContinue())
                                {
                                    Exit();
                                }
                            }
                            else
                            {
                                assemblyPaths.Add(path);
                            }
                        }
                    }
                }

                if (optimizations.Count == 0)
                {
                    WriteError("    Error: No optimizations were specified.");
                    Exit();
                }
                else if (assemblyPaths.Count == 0)
                {
                    WriteError("    Error: No assemblies were specified.");
                    Exit();
                }

                Run(regex, optimizations, assemblyPaths);
            }
            catch (Exception e)
            {
                WriteError(e.Message);
                Exit();
            }

            return 0;
        }

        private static bool CheckForContinue()
        {
            Console.WriteLine();
            Console.Write("    Would you like to continue [Y/n]: ");

            while (true)
            {
                var input = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(input) || (input == "yes") || (input == "y"))
                {
                    Console.WriteLine();
                    return true;
                }
                else if ((input == "no") || (input == "n"))
                {
                    Console.WriteLine();
                    return false;
                }
                else
                {
                    Console.Write($"        Unrecognized input: '{input}'. Please use 'yes', 'y', 'no', or 'n': ");
                }
            }
        }

        private static void Exit(int exitCode = int.MinValue)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(exitCode);
        }

        private static string Matches(string arg, params string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                if ((arg[0] != '-') && (arg[0] != '/'))
                {
                    return null;
                }

                if ((((arg.Length - 1) == keyword.Length) || (arg.Length > (keyword.Length + 2))) && (string.Compare(arg, 1, keyword, 0, keyword.Length, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    if ((arg.Length - 1) == keyword.Length)
                    {
                        return string.Empty;
                    }

                    if ((arg[keyword.Length + 1] == ':') || (arg[keyword.Length + 1] == '='))
                    {
                        return arg.Substring(keyword.Length + 2);
                    }
                }
            }

            return null;
        }

        private static int PrintHelp()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            Console.WriteLine($"IlOptimizer v{thisAssembly.GetName().Version} - A tool for performing post-compilation optimization on managed assemblies.");
            Console.WriteLine($"    {Path.GetFileName(thisAssembly.Location)} [help-command] [filter-command] <optimizations> <assemblies>");
            Console.WriteLine();
            Console.WriteLine("    All optimizations and commands must be prefixed with '-' or '/'.");
            Console.WriteLine("    Any optimizations that take parameters can use '=' or ':'");
            Console.WriteLine("    Any unrecognized arguments are treated as <assemblies>.");
            Console.WriteLine();
            Console.WriteLine("    Each processed assembly will list the its full name as well as the number of modules, types, events, properties, and methods processed");
            Console.WriteLine("    An event is added to the event count while the methods it contains ('add', 'remove', 'invoke', etc) are added to the method count.");
            Console.WriteLine("    A property is added to the property count while the methods it contains ('get', 'set', etc) are added to the method count.");
            Console.WriteLine();
            Console.WriteLine("    Each optimization performed will then list the number of methods updates, skipped, or failed.");
            Console.WriteLine("    An updated method had the optimization successfully performed.");
            Console.WriteLine("    A skipped method had no code eligible for the optimization.");
            Console.WriteLine("    A failed method had eligible code, but the optimization was not able to be performed succesfully.");
            Console.WriteLine("    ");
            Console.WriteLine();
            Console.WriteLine("    Help Command: 'help', 'h', or '?'");
            Console.WriteLine("        Prints this help message.");
            Console.WriteLine();
            Console.WriteLine("    Filter Command: 'filter=<regex>' or 'f=<regex>'");
            Console.WriteLine("        Filters the methods that should be optimized by matching the regular expression against a method's full name.");
            Console.WriteLine("        All methods will be matched if this command is not specified.");
            Console.WriteLine();
            Console.WriteLine($"    Optimizations");

            foreach (var optimization in Optimizations)
            {
                Console.WriteLine($"        {optimization.Name}");
                Console.WriteLine($"            {optimization.Description}");

                if (optimization.AvailableParameters.Count != 0)
                {
                    Console.WriteLine($"            Parameters: {string.Join(", ", optimization.AvailableParameters)}");

                    foreach (var availableParameter in optimization.AvailableParameters)
                    {
                        Console.WriteLine($"                {availableParameter.Key}: {availableParameter.Value}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"    Asssemblies");
            Console.WriteLine($"        One or more assemblies on which the selected optimizations should be performed.");
            Console.WriteLine($"        The post-processed assemblies will be output to a folder called 'optimized' in the working directory.");
            Console.WriteLine();

            return int.MinValue;
        }

        private static void Process(AssemblyDefinition assembly, string regex, List<Optimization> optimizations)
        {
            Console.WriteLine($"    Processing {assembly.FullName}");

            var processed = new ProcessedData();

            foreach (var module in assembly.Modules)
            {
                Process(module, regex, optimizations, ref processed);
                processed.ModuleCount++;
            }

            Console.WriteLine($"      Processed: {processed.ModuleCount} Modules, {processed.TypeCount} Types, {processed.EventCount} Events, {processed.PropertyCount} Properties, {processed.MethodCount} Methods");
        }

        private static void Process(EventDefinition @event, string regex, List<Optimization> optimizations, ref ProcessedData processed)
        {
            Process(@event.AddMethod, regex, optimizations, ref processed);
            Process(@event.InvokeMethod, regex, optimizations, ref processed);
            Process(@event.RemoveMethod, regex, optimizations, ref processed);

            processed.MethodCount += 3;

            foreach (var otherMethod in @event.OtherMethods)
            {
                Process(otherMethod, regex, optimizations, ref processed);
                processed.MethodCount++;
            }
        }

        private static void Process(MethodDefinition method, string regex, List<Optimization> optimizations, ref ProcessedData processed)
        {
            if ((method is null) || ((regex != string.Empty) && (Regex.IsMatch(method.FullName, regex) == false)))
            {
                processed.MethodCount--;
                return;
            }

            for (var index = 0; index < optimizations.Count; index++)
            {
                var optimization = optimizations[index];

                try
                {
                    var result = optimization.OptimizeMethod(method, optimization.Parameter);

                    if (result is null)
                    {
                        optimization.SkippedMethodCount++;
                    }
                    else if (result == true)
                    {
                        optimization.UpdatedMethodCount++;
                    }
                    else
                    {
                        optimization.FailedMethodCount++;
                    }
                }
                catch (Exception e)
                {
                    WriteWarning($"Failed to optimize: {method.FullName}");
                    WriteWarning($"    Exception: {e.Message}");
                    optimization.FailedMethodCount++;
                }
            }
        }

        private static void Process(ModuleDefinition module, string regex, List<Optimization> optimizations, ref ProcessedData processed)
        {
            foreach (var type in module.Types)
            {
                Process(type, regex, optimizations, ref processed);
                processed.TypeCount++;
            }
        }

        private static void Process(PropertyDefinition property, string regex, List<Optimization> optimizations, ref ProcessedData processed)
        {
            Process(property.GetMethod, regex, optimizations, ref processed);
            Process(property.SetMethod, regex, optimizations, ref processed);

            processed.MethodCount += 2;

            foreach (var otherMethod in property.OtherMethods)
            {
                Process(otherMethod, regex, optimizations, ref processed);
                processed.MethodCount++;
            }
        }

        private static void Process(TypeDefinition type, string regex, List<Optimization> optimizations, ref ProcessedData processed)
        {
            foreach (var nestedType in type.NestedTypes)
            {
                Process(nestedType, regex, optimizations, ref processed);
                processed.TypeCount++;
            }

            foreach (var @event in type.Events)
            {
                Process(@event, regex, optimizations, ref processed);
                processed.EventCount++;
            }

            foreach (var property in type.Properties)
            {
                Process(property, regex, optimizations, ref processed);
                processed.PropertyCount++;
            }

            foreach (var method in type.Methods)
            {
                Process(method, regex, optimizations, ref processed);
                processed.MethodCount++;
            }
        }

        private static void Run(string regex, List<Optimization> optimizations, List<string> assemblyPaths)
        {
            var outputPath = Path.GetFullPath("optimized");
            Directory.CreateDirectory(outputPath);

            foreach (var assemblyPath in assemblyPaths)
            {
                var outputAssemblyPath = Path.Combine(outputPath, Path.GetFileName(assemblyPath));

                if (Path.GetDirectoryName(assemblyPath) == outputPath)
                {
                    WriteWarning("    Warning: An assembly was given which exists in the output folder for the tool.");
                    WriteWarning($"             Assembly: '{assemblyPath}'");
                    WriteWarning("    Continuing will skip processing for this assembly; Otherwise, the process will exit.");

                    if (CheckForContinue())
                    {
                        continue;
                    }

                    Exit();
                }

                if (File.Exists(outputAssemblyPath))
                {
                    WriteWarning("    Warning: A post-processed assembly with the same name already exists in the output folder.");
                    WriteWarning($"             Assembly: '{assemblyPath}'");
                    WriteWarning($"             Output Assembly: '{outputAssemblyPath}'");
                    WriteWarning("    Continuing will cause the existing post-processed assembly to be overwritten; Otherwise, processing for this assembly will be skipped.");

                    if (!CheckForContinue())
                    {
                        continue;
                    }
                }

                // TODO: Add assembly resolver for netcore.

                var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
                Process(assembly, regex, optimizations);

                try
                {
                    assembly.Write(outputAssemblyPath);
                }
                catch
                {
                    if (File.Exists(outputAssemblyPath))
                    {
                        File.Delete(outputAssemblyPath);
                    }

                    throw;
                }

                Console.WriteLine();

                foreach (var optimization in optimizations)
                {
                    Console.WriteLine($"    {optimization.Name}: Updated {optimization.UpdatedMethodCount} Methods, Skipped {optimization.SkippedMethodCount} Methods, Failed {optimization.FailedMethodCount} Methods");
                }
            }
        }

        private static void WriteError(string message)
        {
            WriteLine(ConsoleColor.Red, message);
        }

        private static void WriteLine(ConsoleColor color, string message)
        {
            var foregroundColor = Console.ForegroundColor;
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
            Console.ForegroundColor = foregroundColor;
        }

        private static void WriteWarning(string message)
        {
            WriteLine(ConsoleColor.Yellow, message);
        }
    }
}
