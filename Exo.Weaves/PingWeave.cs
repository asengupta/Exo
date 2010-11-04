using System;
using System.Collections.Generic;
using System.Linq;
using CecilBasedWeaver;
using Exo.Aspects;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class PingWeave : IWeave
    {
        private readonly Type pingType;

        public PingWeave(Type pingType)
        {
            this.pingType = pingType;
        }

        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
            ILProcessor processor = behaviorDefinition.Body.GetILProcessor();
            var pingerDefinition = new VariableDefinition("pinger",
                                                         behaviorDefinition.Module.Import(pingType));
            behaviorDefinition.Body.Variables.Add(pingerDefinition);

            var start = new List<Instruction>
                            {
                                processor.Create(OpCodes.Ldstr, behaviorDefinition.Name),
                                processor.Create(OpCodes.Newobj,
                                                 behaviorDefinition.Module.Import(
                                                     pingType.GetConstructor(new[]{typeof(string)}))),
                                processor.Create(OpCodes.Stloc, pingerDefinition),
                                processor.Create(OpCodes.Ldloc, pingerDefinition),
                                processor.Create(OpCodes.Callvirt,
                                               behaviorDefinition.Module.Import(pingType.GetMethod("Start",
                                                                                                      new Type[] {}))),

                            };
            var end = new List<Instruction>
                          {
                              processor.Create(OpCodes.Ldloc, pingerDefinition),
                              processor.Create(OpCodes.Callvirt,
                                               behaviorDefinition.Module.Import(pingType.GetMethod("End",
                                                                                                      new Type[] {})))
                          };

            new InsertionAtStart(processor, behaviorDefinition).Run(start);
            new InsertionAtEnd(processor, behaviorDefinition).Run(end);
            foreach (var instruction in behaviorDefinition.Body.Instructions)
            {
                Console.Out.WriteLine("{0}/[{1}]: {2} {3}", instruction.Offset, Sequence(instruction.SequencePoint), instruction.OpCode, instruction.Operand);
            }
        }

        private string Sequence(SequencePoint point)
        {
            return point == null ? "NULL" : string.Format("{0}[{1}]-{2}[{3}] <{4}>", point.StartLine, point.StartColumn, point.EndLine, point.EndColumn, point.Document.Url); 
        }
    }
}