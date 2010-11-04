using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    [TaskName("weave.outlook.addin")]
    public class WeaverTask : OutlookAssemblyVisitorTask
    {
        private readonly BroadcastAspectWeaver weaver;

        public WeaverTask()
            : base(new BroadcastAspectWeaver(new OutlookAttributeVisitorFactory()))
        {
        }
    }
}