using System;
using Exo.Aspects.Core;
using Exo.MsmqEndpoint;

namespace Exo.Aspects.Msmq
{
    public class MsmqPinger : AbstractPinger
    {
        public MsmqPinger(string methodDescription, Guid guid) : base(methodDescription, guid)
        {
        }

        public override void Start()
        {
            Send("Start");
        }

        public override void End()
        {
            Send("Stop");
        }

        private void Send(string status)
        {
            new SafeAspectExecution().Run(
                () =>
                new DefaultMsmqEndpoint("OutlookFT.Response").Send(string.Format("{0}/{1}/{2}/{3}", methodDescription,
                                                                                 guid, status, DateTime.Now)));
        }
    }
}