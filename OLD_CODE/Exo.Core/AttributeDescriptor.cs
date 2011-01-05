using System;
using Mono.Cecil;

namespace CecilBasedWeaver
{
    public class AttributeDescriptor : IAttributeVisitor
    {
        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
            Console.Out.WriteLine("{0} - [{1}]", behaviorDefinition.Name,
                                  attribute.Constructor.DeclaringType.FullName);
        }
    }
}