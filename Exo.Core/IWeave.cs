using Mono.Cecil;

namespace CecilBasedWeaver
{
    public interface IAttributeVisitor
    {
        void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute);
    }

    public interface IWeave : IAttributeVisitor
    {
    }
}