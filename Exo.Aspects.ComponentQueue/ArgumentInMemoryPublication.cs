using System.Collections.Generic;
using Exo.Aspects.Core;

namespace Exo.Aspects.ComponentQueue
{
    public class ArgumentInMemoryPublication : IArgumentPublication
    {
        public void Run(List<object> arguments, string description)
        {
            new SafeAspectExecution().Run(() => EventBus.ComponentEventBus.Instance.Publish(arguments, description));
        }
    }
}