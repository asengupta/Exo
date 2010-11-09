using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    public abstract class OutlookAssemblyVisitorTask : AssemblyVisitorTask
    {
        protected OutlookAssemblyVisitorTask(BroadcastAspectWeaver weaver) : base(weaver)
        {
        }

        [TaskAttribute("addin.name", Required = false)]
        public string AddInName { get; set; }
        
        protected override void ExecuteTask()
        {
            if (string.IsNullOrEmpty(AddInName)) new AddInDeploymentDirectorySearch().Run(Weave);
            else new AddInDeploymentDirectorySearch(AddInName).Run(Weave);
        }
    }
}