using System;
using System.Collections;
using System.Collections.Generic;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;

namespace Exo.Weaves
{
    public class DebugWeave : IWeave
    {
        private readonly Type breakpointType;

        public DebugWeave(Type breakpointType)
        {
            this.breakpointType = breakpointType;
        }

        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
            ILProcessor processor = behaviorDefinition.Body.GetILProcessor();
            var pingerDefinition = new VariableDefinition("pinger",
                                                          behaviorDefinition.Module.Import(breakpointType));
            behaviorDefinition.Body.Variables.Add(pingerDefinition);
            var sequencedInstructions =
                behaviorDefinition.Body.Instructions.Where(instruction => instruction.SequencePoint != null).ToList();

            Console.Out.WriteLine(sequencedInstructions.Count);
//            var extractedInstructions = Specific(sequencedInstructions, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 20, 21);
            var extractedInstructions = Filtered(sequencedInstructions);
            foreach (var instruction in extractedInstructions)
            {
                Type intType = typeof(int);
                var breakPointInstructions = new List<Instruction>
                                       {
                                           processor.Create(OpCodes.Newobj, behaviorDefinition.Module.Import(
                                                                                breakpointType.GetConstructor(new Type[]{}))),
                                           processor.Create(OpCodes.Ldc_I4, instruction.SequencePoint.StartLine),
                                           processor.Create(OpCodes.Ldc_I4, instruction.SequencePoint.StartColumn),
                                           processor.Create(OpCodes.Ldc_I4, instruction.SequencePoint.EndLine),
                                           processor.Create(OpCodes.Ldc_I4, instruction.SequencePoint.EndColumn),
                                           processor.Create(OpCodes.Ldstr, instruction.SequencePoint.Document.Url),
                                           processor.Create(OpCodes.Call,
                                                            behaviorDefinition.Module.Import(breakpointType.GetMethod("Activate",
                                                                                                                      new[] {intType, intType, intType, intType, typeof(string)})))
                                       };
                breakPointInstructions.ForEach(bpr => processor.InsertBefore(instruction, bpr));
                foreach (var i in behaviorDefinition.Body.Instructions)
                {
                    Console.Out.WriteLine("{0}/[{1}]: {2} {3}", i.Offset, Sequence(i.SequencePoint), i.OpCode, i.Operand);
                }
            }
        }

        private void Log(List<Instruction> sequencedInstructions, int i)
        {
            Console.Out.WriteLine(sequencedInstructions[i].SequencePoint.StartLine);
            Console.Out.WriteLine(sequencedInstructions[i].SequencePoint.StartColumn);
            Console.Out.WriteLine(sequencedInstructions[i].OpCode);
            Console.Out.WriteLine(sequencedInstructions[i].Operand);
        }

        private string Sequence(SequencePoint point)
        {
            return point == null ? "NULL" : string.Format("{0}[{1}]-{2}[{3}] <{4}>", point.StartLine, point.StartColumn, point.EndLine, point.EndColumn, point.Document.Url);
        }

        private List<Instruction> Filtered(List<Instruction> instructions)
        {
            return instructions.Where(instruction => instruction.OpCode != OpCodes.Pop && instruction.OpCode != OpCodes.Dup && CannotIgnore(instruction.SequencePoint.StartLine)).ToList();
        }

        private bool CannotIgnore(int line)
        {
            return line != 0xFeeFee;
        }

        private List<Instruction> Specific(List<Instruction> instructions, params int[] indices)
        {
            return indices.Select(i => instructions[i]).ToList();
        }
    }
}