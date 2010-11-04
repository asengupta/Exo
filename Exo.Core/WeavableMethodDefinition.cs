using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace CecilBasedWeaver
{
    public class WeavableMethodDefinition : IBehaviorDefinition
    {
        private readonly MethodDefinition methodDefinition;

        public WeavableMethodDefinition(MethodDefinition methodDefinition)
        {
            this.methodDefinition = methodDefinition;
        }

        public MethodBody Body
        {
            get 
            { 
                return methodDefinition.Body; 
            }
        }

        public ModuleDefinition Module
        {
            get { return methodDefinition.Module; }
        }

        public TypeReference ReturnType
        {
            get { return methodDefinition.ReturnType; }
        }

        public Collection<ParameterDefinition> Parameters
        {
            get
            {
                return methodDefinition.Parameters;
            }
        }

        public string Name
        {
            get { return methodDefinition.FullName; }
        }

        public TypeDefinition ContainingType
        {
            get { return methodDefinition.DeclaringType; }
        }
    }
}