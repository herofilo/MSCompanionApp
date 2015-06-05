using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jamoram62.tools.MSCompanion
{
    public class AddonPackageSet
    {

        /// <summary>
        /// Lista de addons encontrados
        /// </summary>
        public List<AddonPackage> Packages { get { return _packages; } }
        private List<AddonPackage> _packages = new List<AddonPackage>();


        public string ErrorLog { get { return _errorLog.ToString(); } }
        private StringBuilder _errorLog = new StringBuilder();



        // --------------------------------------------------------------------------------------------------

        public AddonPackageSet(string pTemporaryFolder, string pApplicationPath = "", string pUserPath = "")
        {
            string _tmpFile =
                string.IsNullOrEmpty(pTemporaryFolder)
                    ? Path.Combine(Path.GetTempPath(), "MSCompanionTemp")
                    : pTemporaryFolder;

            if (!string.IsNullOrEmpty(pApplicationPath))
            {
                ScanPath(Path.Combine(pApplicationPath, "AddOn"), _tmpFile, true, false);
                ScanPath(Path.Combine(pApplicationPath, "Previews"), _tmpFile, true, true);
            }

            if (!string.IsNullOrEmpty(pUserPath))
                ScanPath(Path.Combine(pUserPath, "AddOn"), _tmpFile, false, false);


        }


        private void ScanPath(string pPath, string pTemporaryFolder, bool pMoviestormPackage, bool pPreview)
        {
            if (!Directory.Exists(pPath))
                return;

            foreach (string addonFolder in Directory.GetDirectories(pPath, "*", SearchOption.TopDirectoryOnly))
            {
                if (pPreview && (FindPackageByName(Path.GetFileName(addonFolder)) != null))
                    continue;

                try
                {
                    AddonPackage addon = new AddonPackage(addonFolder, pTemporaryFolder, pMoviestormPackage, pPreview);
                    if (addon.HasIssues)
                        _errorLog.AppendLine(string.Format("ERROR: package '{0}' : {1}", Path.GetFileName(addonFolder),
                            addon.Issues));
                    _packages.Add(addon);
                }
                catch (Exception exception)
                {
                    _errorLog.AppendLine(string.Format("ERROR: package '{0}' : {1}", Path.GetFileName(addonFolder), exception.Message));
                }
            }
        }


        public AddonPackage FindPackageByName(string pPackageName, string pPublisher = "")
        {
            foreach (AddonPackage package in _packages)
            {
                if((package.Name == pPackageName) && (string.IsNullOrEmpty(pPublisher) || (package.Publisher == pPublisher)))
                    return package;
            }
            return null;
        }

        public AddonPackage FindPackageByFriendlyName(string pPackageName, string pPublisher = "")
        {
            foreach (AddonPackage package in _packages)
            {
                if ((package.FriendlyName == pPackageName) && (string.IsNullOrEmpty(pPublisher) || (package.Publisher == pPublisher)))
                    return package;
            }
            return null;
        }


        /// <summary>
        /// Returns a mapping Prop Template name -> Package name
        /// </summary>
        /// <returns>Prop->Package mapping</returns>
        public Dictionary<string, string> MapPropToPackage(MovieStormPropertiesFile pPropertiesFile)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (AddonPackage package in _packages)
            {
                foreach (string propTemplateName in package.PropList())
                {
                    try
                    {
                        string priorPackageName;
                        if (!map.TryGetValue(propTemplateName, out priorPackageName))
                        {
                            map.Add(propTemplateName, package.Name);
                            continue;
                        }
                        // Prop duplicado!
                        bool? isEnabled = pPropertiesFile.PackageIsEnabled(priorPackageName);
                        if (isEnabled.HasValue && isEnabled.Value)
                            continue;

                        isEnabled = pPropertiesFile.PackageIsEnabled(package.Name);
                        if (isEnabled.HasValue && isEnabled.Value)
                            map[propTemplateName] = package.Name;
                    }
                    catch (Exception exception)
                    {
                        
                    }
                }

            }
            return map;
        }

        /// <summary>
        /// Returns a mapping Prop Template name -> Package name
        /// </summary>
        /// <returns>Prop->Package mapping</returns>
        public Dictionary<string, string> MapBodyPartToPackage(MovieStormPropertiesFile pPropertiesFile)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (AddonPackage package in _packages)
            {
                foreach (string bodyPartName in package.BodyPartList())
                {
                    string priorPackageName;
                    if (!map.TryGetValue(bodyPartName, out priorPackageName))
                    {
                        map.Add(bodyPartName, package.Name);
                        continue;
                    }
                    // Prop duplicado!
                    bool? isEnabled = pPropertiesFile.PackageIsEnabled(priorPackageName);
                    if (isEnabled.HasValue && isEnabled.Value)
                        continue;

                    isEnabled = pPropertiesFile.PackageIsEnabled(package.Name);
                    if (isEnabled.HasValue && isEnabled.Value)
                        map[bodyPartName] = package.Name;
                }
            }
            return map;
        }



        public Dictionary<string, string> MapFiltersToPackage(MovieStormPropertiesFile pPropertiesFile)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (AddonPackage package in _packages)
            {
                foreach (string filterName in package.FilterList())
                {
                    string priorPackageName;
                    if (!map.TryGetValue(filterName, out priorPackageName))
                    {
                        map.Add(filterName, package.Name);
                        continue;
                    }
                    // Prop duplicado!
                    bool? isEnabled = pPropertiesFile.PackageIsEnabled(priorPackageName);
                    if (isEnabled.HasValue && isEnabled.Value)
                        continue;

                    isEnabled = pPropertiesFile.PackageIsEnabled(package.Name);
                    if (isEnabled.HasValue && isEnabled.Value)
                        map[filterName] = package.Name;
                }
            }
            return map;
        }


        public Dictionary<string, string> MapSoundsToPackage(MovieStormPropertiesFile pPropertiesFile)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (AddonPackage package in _packages)
            {
                foreach (string soundName in package.SoundList())
                {
                    string priorPackageName;
                    if (!map.TryGetValue(soundName, out priorPackageName))
                    {
                        map.Add(soundName, package.Name);
                        continue;
                    }
                    // Prop duplicado!
                    bool? isEnabled = pPropertiesFile.PackageIsEnabled(priorPackageName);
                    if (isEnabled.HasValue && isEnabled.Value)
                        continue;

                    isEnabled = pPropertiesFile.PackageIsEnabled(package.Name);
                    if (isEnabled.HasValue && isEnabled.Value)
                        map[soundName] = package.Name;
                }
            }
            return map;
        }


    }
}
