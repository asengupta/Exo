using Exo.Aspects.Core;
using Exo.MsmqEndpoint;

namespace Exo.Aspects.Msmq
{
    public class ReturnValueMsmqPublication : IReturnValuePublication
    {
        public void Run(object returnValue, string description)
        {
            /*new SafeAspectExecution().Run(() => new DefaultMsmqEndpoint("OutlookFT.Response").Send(
                                                    string.Format("{0}/{1}...", enclosingObject.GetType(), returnValue)));*/
            new SafeAspectExecution().Run(() => new DefaultMsmqEndpoint("OutlookFT.Response").Send(
                                                    string.Format("{0}/{1}...", description, returnValue)));
        }
    }
}