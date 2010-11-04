using System.Collections.Generic;
using CecilBasedWeaver;
using Exo.Aspects.ComponentQueue;
using Exo.Aspects.Msmq;
using Exo.Aspects.Text;
using Exo.Attributes;
using Exo.Weaves;

namespace OutlookHooks.Task
{
    public class OutlookAttributeVisitorFactory : IAttributeVisitorFactory
    {
        private readonly IDictionary<string, IWeave> weaves;

        public OutlookAttributeVisitorFactory(IDictionary<string, IWeave> weaves)
        {
            this.weaves = weaves;
        }

        public OutlookAttributeVisitorFactory() : this(new Dictionary<string, IWeave>
                                                    {
                                                        {typeof (Broadcast).FullName, new BroadcastWeave(typeof(MsmqExitBroadcast))},
                                                        {typeof (Publish).FullName, new PublishWeave(typeof(ReturnValueInMemoryPublication), typeof(SelfInMemoryPublication), typeof(ArgumentInMemoryPublication))},
                                                        {typeof (StackTraceBroadcast).FullName, new StackTraceBroadcastWeave(typeof(TextStackTracePublication))},
                                                        {typeof (Performance).FullName, new ExecutionTimeMonitorWeave(typeof(ExecutionTimeMonitor))}
                                                    })
        {
        }

        public IAttributeVisitor Visitor(string key)
        {
            if (weaves.ContainsKey(key)) return weaves[key];
            return new NullVisitor();
        }
    }
}