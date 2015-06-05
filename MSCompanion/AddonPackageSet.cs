using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace jamoram62.tools.MSCompanion
{
    public class AddonPackageSet
    {

        public AddonPackageSet(string pTemporaryFolder, string pApplicationPath = "", string pUserPath = "")
        {
            string _tmpFile =
                string.IsNullOrEmpty(pTemporaryFolder)
                    ? Path.Combine(Path.GetTempPath(), "MSCompanionTemp")
                    : pTemporaryFolder;

            if(!string.IsNullOrEmpty(pApplicationPath))
                ScanPath(Path.Combine(pApplicationPath, "AddOn"), _tmpFile);

            if (!string.IsNullOrEmpty(pUserPath))
                ScanPath(Path.Combine(pUserPath, "AddOn"), _tmpFile);


        }


        private void ScanPath(string pPath, string pTemporaryFolder)
        {
            if (!Directory.Exists(pPath))
                return;

            foreach (string addonFolder in Directory.GetDirectories(pPath, "*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    AddonPackage addon = new AddonPackage(addonFolder, pTemporaryFolder);
                }
                catch { }


            }

        }


    }
}
