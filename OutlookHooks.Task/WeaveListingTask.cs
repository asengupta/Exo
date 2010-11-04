using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    [TaskName("list.weave.outlook.addin")]
    public class WeaveListingTask : OutlookAssemblyVisitorTask
    {
        private readonly BroadcastAspectWeaver weaver;

        public WeaveListingTask()
            : base(new BroadcastAspectWeaver(new OutlookAttributeDescriptorFactory()))
        {
        }
    }
}