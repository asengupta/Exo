using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace CecilBasedWeaver
{
    public interface IPositionalInsertion
    {
        void Run(List<Instruction> instructions);
    }
}