using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CecilBasedWeaver;
using Exo.Core;
using Mono.Cecil;

namespace OutlookHooks.Task
{
    public abstract class AssemblyVisitorTask : NAnt.Core.Task
    {
        private readonly BroadcastAspectWeaver weaver;

        protected AssemblyVisitorTask(BroadcastAspectWeaver weaver)
        {
            this.weaver = weaver;
        }

        protected void Weave(DirectoryInfo directoryInfo)
        {
            FileSystemInfo[] files = directoryInfo.GetFileSystemInfos("*.dll");
            IEnumerable<FileSystemInfo> includedFiles = files.Where(info => !IsExcluded(info));
            ((BaseAssemblyResolver) GlobalAssemblyResolver.Instance).AddSearchDirectory(directoryInfo.FullName);
            foreach (FileSystemInfo file in includedFiles)
            {
                if (Verbose)
                     Console.Out.WriteLine("Processing {0}...", file.FullName);
                weaver.Weave(new ModuleIO(file.FullName), Verbose);
            }
        }

        private bool IsExcluded(FileSystemInfo info)
        {
            var exclusions = new List<string>
                                 {
                                     "Exo",
                                     "ObjectNinja"
                                 };
            foreach (string exclusion in exclusions)
            {
                if (info.FullName.Contains(exclusion))
                {
                    if (Verbose)
                        Console.Out.WriteLine("File {0} is excluded, skipping...", exclusion);
                    return true;
                }
            }
            return false;
        }
    }
}