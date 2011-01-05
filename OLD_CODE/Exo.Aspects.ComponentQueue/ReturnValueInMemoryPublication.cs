using Exo.Aspects.Core;

namespace Exo.Aspects.ComponentQueue
{
    public class ReturnValueInMemoryPublication : IReturnValuePublication
    {
        public void Run(object enclosingObject, object returnValue)
        {
            new SafeAspectExecution().Run(() => EventBus.ComponentEventBus.Instance.Publish(returnValue));
        }
    }
}