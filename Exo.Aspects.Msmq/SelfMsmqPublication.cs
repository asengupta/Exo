using Exo.Aspects.Core;
using Exo.MsmqEndpoint;

namespace Exo.Aspects.Msmq
{
    public class SelfMsmqPublication : ISelfPublication
    {
        public void Run(object self, string description)
        {
            new SafeAspectExecution().Run(() => new DefaultMsmqEndpoint("OutlookFT.Response").Send(
                                                    string.Format("{0} published self...", self)));
        }
    }
}