using System;
using System.Collections.Generic;
using Toe.SPIRV.Instructions;

namespace Toe.SPIRV
{
    internal class InstructionRegistry
    {
        public InstructionRegistry()
        {
            
        }
        private readonly Dictionary<uint, InstructionWithId> _instructions = new Dictionary<uint, InstructionWithId>();

        public InstructionWithId this[uint id]
        {
            get
            {
                if (id == 0)
                    return null;
                if (_instructions.TryGetValue(id, out var instruction))
                    return instruction;
                throw new KeyNotFoundException("Instruction with IdResult = " + id + " is not found");
            }
        }

        public void Add(InstructionWithId instruction)
        {
            if (instruction.IdResult == 0)
                throw new ArgumentException("Instruction should have not zeo IdResult");
            _instructions.Add(instruction.IdResult, instruction);
        }

        public bool Remove(InstructionWithId instruction)
        {
            if (instruction.IdResult == 0)
                throw new ArgumentException("Instruction should have not zeo IdResult");
            return _instructions.Remove(instruction.IdResult);
        }
    }
}