using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace CecilBasedWeaver
{
    public abstract class PositionalInsertion : IPositionalInsertion
    {
        protected readonly ILProcessor processor;
        protected readonly IBehaviorDefinition definition;
        public abstract void Run(List<Instruction> instructions);

        protected PositionalInsertion(ILProcessor processor, IBehaviorDefinition definition)
        {
            this.processor = processor;
            this.definition = definition;
        }
    }
}