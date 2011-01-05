using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;
using Mono.Cecil.Rocks;

namespace CecilBasedWeaver
{
    public class InsertionAtEnd : PositionalInsertion
    {
        public InsertionAtEnd(ILProcessor processor, IBehaviorDefinition definition) : base(processor, definition)
        {
        }

        public override void Run(List<Instruction> instructions)
        {
            if (instructions.Count == 0) return;
            var ret = definition.Body.Instructions.Last();
            foreach (var instruction in instructions)
            {
                processor.InsertBefore(ret, instruction);
            }

            FixBranchInstructions(instructions, ret);
        }

        private void FixBranchInstructions(IList<Instruction> instructions, Instruction returnInstruction)
        {
            foreach(Instruction i in definition.Body.Instructions)
            {
                if (CanReturnFromMethod(i, returnInstruction))
                {
                    i.Operand = instructions[0];
                }
            }
        }

        private bool CanReturnFromMethod(Instruction instruction, Instruction returnInstruction)
        {
            if (!IsBranching(instruction)) return false;
            if (instruction.Operand == returnInstruction) return true;
            return false;
        }

        private bool IsBranching(Instruction instruction)
        {
            var allPossibleBranchOpCodes = new[]
                            {
                                OpCodes.Beq, OpCodes.Beq_S, OpCodes.Bge, OpCodes.Bge_S, OpCodes.Bge_Un, OpCodes.Bge_Un_S, OpCodes.Bgt,
                                OpCodes.Bgt_S, OpCodes.Bgt_Un, OpCodes.Bgt_Un_S, OpCodes.Ble, OpCodes.Ble_S, OpCodes.Ble_Un,
                                OpCodes.Bge_Un_S, OpCodes.Blt, OpCodes.Blt_S, OpCodes.Blt_Un, OpCodes.Blt_Un_S, OpCodes.Bne_Un,
                                OpCodes.Bne_Un_S, OpCodes.Br, OpCodes.Br_S, OpCodes.Brfalse, OpCodes.Brfalse_S, OpCodes.Brtrue,
                                OpCodes.Brtrue_S, OpCodes.Leave, OpCodes.Leave_S
                            };
            return allPossibleBranchOpCodes.Contains(instruction.OpCode);
        }
    }
}