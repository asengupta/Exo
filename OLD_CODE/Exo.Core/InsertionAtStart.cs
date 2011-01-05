using System.Collections.Generic;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace CecilBasedWeaver
{
    public class InsertionAtStart : PositionalInsertion
    {
        public InsertionAtStart(ILProcessor processor, IBehaviorDefinition definition) : base(processor, definition)
        {
        }

        public override void Run(List<Instruction> instructions)
        {
            if (instructions.Count == 0) return;
            instructions.Reverse();
            foreach (var instruction in instructions)
            {
                processor.InsertBefore(definition.Body.Instructions[0], instruction);
            }
        }
    }
}