using System;
using System.Collections.Generic;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class PublishArgumentsWeave : IWeave
    {
        private readonly Type broadcastType;

        public PublishArgumentsWeave(Type broadcastType)
        {
            this.broadcastType = broadcastType;
        }

        public void Visit(IBehaviorDefinition method, CustomAttribute attribute)
        {
            ILProcessor processor = method.Body.GetILProcessor();
            var listType = typeof(List<object>);
            var listVariable = new VariableDefinition("argumentz", method.Module.Import(listType));
            method.Body.Variables.Add(listVariable);
            var instructions = new List<Instruction>();
            instructions.Add(processor.Create(OpCodes.Newobj, method.Module.Import(listType.GetConstructor(new Type[] { }))));
            instructions.Add(processor.Create(OpCodes.Stloc, listVariable));

            int parameterIndex = 1;
            foreach (var parameter in method.Parameters)
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, listVariable));
                instructions.Add(processor.Create(OpCodes.Ldarg, parameterIndex));
                if (parameter.ParameterType.IsPrimitive || parameter.ParameterType.IsValueType) instructions.Add(processor.Create(OpCodes.Box, parameter.ParameterType));
                instructions.Add(processor.Create(OpCodes.Callvirt, method.Module.Import(listType.GetMethod("Add", new[] { typeof(object) }))));
                ++parameterIndex;
            }
            instructions.Add(processor.Create(OpCodes.Newobj, method.Module.Import(broadcastType.GetConstructor(new Type[] {}))));
            instructions.Add(processor.Create(OpCodes.Ldloc, listVariable));
            instructions.Add(processor.Create(OpCodes.Callvirt, method.Module.Import(broadcastType.GetMethod("Run", new[] { listType }))));

            new InsertionAtStart(processor, method).Run(instructions);
        }
    }
}