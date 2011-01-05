using System.IO;
using Exo.Core;
using NAnt.Core.Attributes;

namespace OutlookHooks.Task
{
    public abstract class OutlookAssemblyVisitorTask : AssemblyVisitorTask
    {
        protected OutlookAssemblyVisitorTask(AspectWeaver weaver) : base(weaver)
        {
        }

        [TaskAttribute("dir.to.weave", Required = true)]
        public string DirectoryToWeave { get; set; }
        
        protected override void ExecuteTask()
        {
            Weave(new DirectoryInfo(DirectoryToWeave));
        }
    }
}