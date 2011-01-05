using System;
using System.Collections.Generic;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Exo.Weaves
{
    public class PublishSelfWeave : IWeave
    {
        private readonly Type broadcastType;

        public PublishSelfWeave(Type broadcastType)
        {
            this.broadcastType = broadcastType;
        }

        public void Visit(IBehaviorDefinition method, CustomAttribute attribute)
        {
            ILProcessor processor = method.Body.GetILProcessor();
            Instruction exitInstruction = processor.Create(OpCodes.Callvirt, method.Module.Import(broadcastType.GetMethod("Run", new[] { typeof(object) })));
            var returnValue = new VariableDefinition("retVal", method.Module.Import(typeof(object)));
            var enclosingObject = new VariableDefinition("enclosing", method.Module.Import(typeof(object)));
            method.Body.Variables.Add(enclosingObject);
            method.Body.Variables.Add(returnValue);
            Instruction store = processor.Create(OpCodes.Stloc, returnValue);
            Instruction reload = processor.Create(OpCodes.Ldloc, returnValue);
            var instructions = new List<Instruction>();
            if (!ReturnsVoid(method))
            {
                instructions.Add(store);
            }
            instructions.Add(processor.Create(OpCodes.Newobj, method.Module.Import(broadcastType.GetConstructor(new Type[] { }))));
            instructions.Add(processor.Create(OpCodes.Ldarg_0));
            instructions.Add(exitInstruction);
            if (!ReturnsVoid(method))
            {
                instructions.Add(reload);
            }

            new InsertionAtEnd(processor, method).Run(instructions);
        }

        private bool ReturnsVoid(IBehaviorDefinition method)
        {
            TypeReference voidType = method.Module.Import(typeof(void));
            return method.ReturnType.FullName.Equals(voidType.FullName);
        }
    }
}