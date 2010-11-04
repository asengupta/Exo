using System;
using Mono.Cecil;

namespace CecilBasedWeaver
{
    public class NullVisitor : IAttributeVisitor
    {
        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
        }
    }
}