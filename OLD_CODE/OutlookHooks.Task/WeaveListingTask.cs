using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    [TaskName("list.weave.outlook.addin")]
    public class WeaveListingTask : OutlookAssemblyVisitorTask
    {
        private readonly AspectWeaver weaver;

        public WeaveListingTask()
            : base(new AspectWeaver(new OutlookAttributeDescriptorFactory()))
        {
        }
    }
}