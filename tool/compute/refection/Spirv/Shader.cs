using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV
{
    internal class Shader
    {
        public IEnumerable<OpVariable> Uniforms => GetAllVariablesByStorageClass(StorageClass.Enumerant.Uniform);

        public IEnumerable<OpVariable> UniformConstant => GetAllVariablesByStorageClass(StorageClass.Enumerant.UniformConstant);

        public IEnumerable<OpVariable> Inputs => GetAllVariablesByStorageClass(StorageClass.Enumerant.Input);

        public IEnumerable<OpVariable> Outputs => GetAllVariablesByStorageClass(StorageClass.Enumerant.Output);

        public uint Bound { get; set; }

        public IList<Instruction> Instructions { get; } = new List<Instruction>();

        public uint GeneratorName { get; set; } = 851975;

        public uint Version { get; set; } = SpirVVersion;

        public void Build(WordWriter writer)
        {
            writer.WriteWord(Magic);
            writer.WriteWord(Version);
            writer.WriteWord(GeneratorName);
            uint bound = 0;
            foreach (var instruction in Instructions)
            {
                if (instruction.TryGetResultId(out uint id))
                {
                    if (id >= bound)
                        bound = id + 1;
                }
            }
            writer.WriteWord(bound);
            writer.WriteWord(0);
            foreach (var instruction in Instructions)
            {
                instruction.Build(writer);
            }
        }

        public const uint Magic = 0x07230203;
        public const uint SpirVVersion = 0x00010000;
        public static Shader Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var shader = new Shader();
            if (reader.ReadWord() != Magic) throw new FormatException("SpirV magic number doesn't match");
            shader.Version = reader.ReadWord();
            if (shader.Version != SpirVVersion) throw new FormatException("Unsupported SpirV version");
            shader.GeneratorName = reader.ReadWord();
            shader.Bound = reader.ReadWord();
            if (reader.ReadWord() != 0) throw new FormatException("SpirV reserved word isn't 0");
            while (reader.Position < end)
            {
                var instruction = InstructionFactory.Parse(reader);
                shader.Instructions.Add(instruction);
            }
            return shader;
        }

        public static Shader Parse(BinaryReader reader, uint wordCount)
        {
            return Parse(new WordReader(reader, new InstructionRegistry()), wordCount);
        }

        public static Shader Parse(BinaryReader reader)
        {
            var length = reader.BaseStream.Length - reader.BaseStream.Position;
            if (length % 4 != 0) throw new FormatException("SpirV bytecode length should be divisable by 4");
            return Parse(new WordReader(reader, new InstructionRegistry()), (uint) length / 4);
        }

        public byte[] Build()
        {
            var memoryStream = new MemoryStream();
            Build(memoryStream);
            return memoryStream.ToArray();
        }

        public void Build(Stream stream)
        {
            using (var binaryWriter = new BinaryWriter(stream))
            {
                Build(binaryWriter);
            }
        }
        public void Build(BinaryWriter writer)
        {
            using (var wordWriter = new WordWriter(writer))
            {
                Build(wordWriter);
            }
        }
 
        public static Shader Parse(byte[] spirVBytes)
        {
            if (spirVBytes.Length % 4 != 0) throw new FormatException("SpirV bytecode length should be divisable by 4");
            using (var binaryReader = new BinaryReader(new MemoryStream(spirVBytes)))
            {
                return Parse(binaryReader, (uint) spirVBytes.Length / 4);
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Instructions);
        }

        private IEnumerable<OpVariable> GetAllVariablesByStorageClass(StorageClass.Enumerant storageClass)
        {
            foreach (var instruction in Instructions)
            {
                var variable = instruction as OpVariable;
                if (variable != null && variable.StorageClass.Value == storageClass)
                    yield return variable;
            }
        }
    }
}