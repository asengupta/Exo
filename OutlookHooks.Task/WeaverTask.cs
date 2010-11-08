using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    [TaskName("weave.outlook.addin")]
    public class WeaverTask : OutlookAssemblyVisitorTask
    {
        public WeaverTask()
            : base(new BroadcastAspectWeaver(new OutlookAttributeVisitorFactory()))
        {
        }
    }
}