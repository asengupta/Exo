using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace CecilBasedWeaver
{
    public interface IBehaviorDefinition
    {
        MethodBody Body { get; }
        ModuleDefinition Module { get; }
        TypeReference ReturnType { get; }
        Collection<ParameterDefinition> Parameters { get; }
        string Name { get; }
        TypeDefinition ContainingType { get; }
    }
}