using Exo.Aspects.Core;
using Exo.MsmqEndpoint;

namespace Exo.Aspects.Msmq
{
    public class MsmqExitBroadcast : IExitBroadcast
    {
        public void Run(string description, object enclosingObject)
        {
            new SafeAspectExecution().Run(
                () => new DefaultMsmqEndpoint("OutlookFT.Response").Send(enclosingObject + "/" + description));
        }
    }
}