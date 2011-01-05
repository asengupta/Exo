using System;
using System.IO;
using Microsoft.Win32;

namespace OutlookHooks.Task
{
    public class AddInDeploymentDirectorySearch
    {
        private readonly string addInManifestResgistryPath;

        public AddInDeploymentDirectorySearch(string addInRegistryEntry)
        {
            addInManifestResgistryPath =
                string.Format("Software\\Microsoft\\Office\\Outlook\\Addins\\{0}", addInRegistryEntry);
        }

        public AddInDeploymentDirectorySearch()
            : this("IMD - Outlook 2003 AddIn")
        {
        }

        public void Run(Action<DirectoryInfo> loadAction)
        {
            new RegistryKeySearch(Registry.CurrentUser).Run(addInManifestResgistryPath,
                                                            manifestPath => ResolvePath(loadAction, manifestPath));
            new RegistryKeySearch(Registry.LocalMachine).Run(addInManifestResgistryPath,
                                                             manifestPath => ResolvePath(loadAction, manifestPath));
        }

        private void ResolvePath(Action<DirectoryInfo> loadAction, string manifestFilePath)
        {
            var manifestFileInfo = new FileInfo(manifestFilePath);
            loadAction(manifestFileInfo.Directory);
        }
    }
}