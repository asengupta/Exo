using System;
using System.Collections.Generic;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class PublishReturnValueWeave : IWeave
    {
        private readonly Type broadcastType;

        public PublishReturnValueWeave(Type broadcastType)
        {
            this.broadcastType = broadcastType;
        }

        public void Visit(IBehaviorDefinition method, CustomAttribute attribute)
        {
            ILProcessor processor = method.Body.GetILProcessor();
            Instruction exitInstruction = processor.Create(OpCodes.Callvirt,
                                                           method.Module.Import(
                                                               broadcastType.GetMethod("Run",
                                                                                       new[]
                                                                                           {
                                                                                               typeof (
                                                                                                   object)
                                                                                               ,
                                                                                               typeof (
                                                                                                   object)
                                                                                           })));
            var returnValue = new VariableDefinition("retVal", method.Module.Import(typeof(object)));
            var enclosingObject = new VariableDefinition("enclosing", method.Module.Import(typeof(object)));
            method.Body.Variables.Add(enclosingObject);
            method.Body.Variables.Add(returnValue);
            Instruction store = processor.Create(OpCodes.Stloc, returnValue);

            var instructions = new List<Instruction>
                                   {
                                       store,
                                       processor.Create(OpCodes.Newobj, method.Module.Import(broadcastType.GetConstructor(new Type[] {}))),
                                       processor.Create(OpCodes.Ldarg_0),
                                       processor.Create(OpCodes.Ldloc, returnValue),
                                       exitInstruction,
                                       processor.Create(OpCodes.Ldloc, returnValue)
                                   };
            new InsertionAtEnd(processor, method).Run(instructions);
        }
    }
}