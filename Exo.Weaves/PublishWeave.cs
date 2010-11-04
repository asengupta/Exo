using System;
using System.Linq;
using CecilBasedWeaver;
using Exo.Attributes;
using Mono.Cecil;

namespace Exo.Weaves
{
    public class PublishWeave : IWeave
    {
        private readonly Type returnValueHandler;
        private readonly Type selfHandler;
        private readonly Type argumentsHandler;

        public PublishWeave(Type returnValueHandler, Type selfHandler, Type argumentsHandler)
        {
            this.returnValueHandler = returnValueHandler;
            this.selfHandler = selfHandler;
            this.argumentsHandler = argumentsHandler;
        }

        public void Visit(IBehaviorDefinition behaviorDefinition, CustomAttribute attribute)
        {
            var first = attribute.Properties.Where(argument => (argument.Name == "Target")).First();
            var target =
                ((PublishTarget)
                 first.Argument.Value);
            switch (target)
            {
                case PublishTarget.Return:
                    new PublishReturnValueWeave(returnValueHandler).Visit(behaviorDefinition, attribute);
                    break;

                case PublishTarget.Self:
                    new PublishSelfWeave(selfHandler).Visit(behaviorDefinition, attribute);
                    break;

                case PublishTarget.Arguments:
                    new PublishArgumentsWeave(argumentsHandler).Visit(behaviorDefinition, attribute);
                    break;
            }
        }
    }
}