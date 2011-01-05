using System;
using Microsoft.Win32;

namespace OutlookHooks.Task
{
    public class RegistryKeySearch
    {
        private readonly RegistryKey rootKey;

        public RegistryKeySearch(RegistryKey rootKey)
        {
            this.rootKey = rootKey;
        }

        public void Run(string registryPath, Action<string> loadAction)
        {
            RegistryKey key = rootKey.OpenSubKey(registryPath, false);
            if (key == null) return;
            object manifestPath = key.GetValue("Manifest");
            loadAction(manifestPath.ToString());
        }
    }
}