using Exo.Aspects.Core;
using Exo.MsmqEndpoint;

namespace Exo.Aspects.Msmq
{
    public class MsmqBreakpoint : IBreakPoint
    {
        public void Activate(int startLine, int startColumn, int endLine, int endColumn, string documentURL)
        {
            string message = string.Format("{0}/{1}/{2}/{3}/{4}", startLine, startColumn, endLine, endColumn,
                                           documentURL);
            new SafeAspectExecution().Run(
                () => new DefaultMsmqEndpoint("ExoDebugServer").Send(message));
            new SafeAspectExecution().Run(
                () => new QueueListener(new DefaultMsmqEndpoint("ExoDebugClient")).ListenFor(message));
        }
    }
}