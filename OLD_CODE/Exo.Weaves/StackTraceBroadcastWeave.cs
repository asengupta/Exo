using System;
using System.Collections.Generic;
using System.Diagnostics;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class StackTraceBroadcastWeave : IWeave
    {
        private readonly Type stackTraceBroadcastType;

        public StackTraceBroadcastWeave(Type stackTraceType)
        {
            stackTraceBroadcastType = stackTraceType;
        }

        public void Visit(IBehaviorDefinition behavior, CustomAttribute attribute)
        {
            ILProcessor processor = behavior.Body.GetILProcessor();
            var instructions = new List<Instruction>();
            Type stackTraceType = typeof (StackTrace);
            instructions.Add(processor.Create(OpCodes.Newobj,
                                              behavior.Module.Import(
                                                  stackTraceBroadcastType.GetConstructor(new Type[] {}))));
            instructions.Add(processor.Create(OpCodes.Newobj,
                                              behavior.Module.Import(stackTraceType.GetConstructor(new Type[] {}))));
            instructions.Add(processor.Create(OpCodes.Callvirt,
                                              behavior.Module.Import(stackTraceBroadcastType.GetMethod("Run",
                                                                                                       new[]
                                                                                                           {
                                                                                                               stackTraceType
                                                                                                           }))));

            new InsertionAtEnd(processor, behavior).Run(instructions);
        }
    }
}