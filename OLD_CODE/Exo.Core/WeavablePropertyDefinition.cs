using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace CecilBasedWeaver
{
    public class WeavablePropertyDefinition : IBehaviorDefinition
    {
        private readonly PropertyDefinition propertyDefinition;

        public WeavablePropertyDefinition(PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.GetMethod == null) throw new UnsupportedPropertyFormException(propertyDefinition);
            this.propertyDefinition = propertyDefinition;
        }

        public MethodBody Body
        {
            get { return propertyDefinition.GetMethod.Body; }
        }

        public ModuleDefinition Module
        {
            get { return propertyDefinition.Module; }
        }

        public TypeReference ReturnType
        {
            get { return propertyDefinition.GetMethod.ReturnType; }
        }

        public Collection<ParameterDefinition> Parameters
        {
            get { return propertyDefinition.SetMethod.Parameters; }
        }

        public string Name
        {
            get { return propertyDefinition.FullName; }
        }

        public TypeDefinition ContainingType
        {
            get { return propertyDefinition.DeclaringType; }
        }

    }
}