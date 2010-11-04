using System;
using System.Collections.Generic;
using System.Linq;
using CecilBasedWeaver;
using Exo.Aspects.Core;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class ExecutionTimeMonitorWeave : IWeave
    {
        private readonly Type monitorType;

        public ExecutionTimeMonitorWeave(Type monitorType)
        {
            this.monitorType = monitorType;
        }

        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
            var maximum =
                (int) attribute.Properties.Where(argument => (argument.Name == "Maximum")).First().Argument.Value;
            ILProcessor processor = behaviorDefinition.Body.GetILProcessor();
            Type expectationType = typeof (PerformanceExpectation);
            var timerDefinition = new VariableDefinition("timer",
                                                         behaviorDefinition.Module.Import(monitorType));
            behaviorDefinition.Body.Variables.Add(timerDefinition);

            var start = new List<Instruction>
                            {
                                processor.Create(OpCodes.Call, behaviorDefinition.Module.Import(
                                                                   typeof (DateTime).GetMethod("get_Now",
                                                                                               new Type[] {}))),
                                processor.Create(OpCodes.Ldc_I4, maximum),
                                processor.Create(OpCodes.Newobj,
                                                 behaviorDefinition.Module.Import(
                                                     expectationType.GetConstructor(new[] {typeof (int)}))),
                                processor.Create(OpCodes.Newobj,
                                                 behaviorDefinition.Module.Import(
                                                     monitorType.GetConstructor(new[]
                                                                                    {typeof (DateTime), expectationType}))),
                                processor.Create(OpCodes.Stloc, timerDefinition)
                            };
            var end = new List<Instruction>
                          {
                              processor.Create(OpCodes.Ldloc, timerDefinition),
                              processor.Create(OpCodes.Callvirt,
                                               behaviorDefinition.Module.Import(monitorType.GetMethod("End",
                                                                                                      new Type[] {})))
                          };

            new InsertionAtStart(processor, behaviorDefinition).Run(start);
            new InsertionAtEnd(processor, behaviorDefinition).Run(end);
        }
    }
}