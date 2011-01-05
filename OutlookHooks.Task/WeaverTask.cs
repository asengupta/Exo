using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    [TaskName("weave.outlook.addin")]
    public class WeaverTask : OutlookAssemblyVisitorTask
    {
        private readonly AspectWeaver weaver;

        public WeaverTask()
            : base(new AspectWeaver(new OutlookAttributeVisitorFactory()))
        {
        }

        public void ExecuteFromUnitTest()
        {
            ExecuteTask();
        }
    }
}