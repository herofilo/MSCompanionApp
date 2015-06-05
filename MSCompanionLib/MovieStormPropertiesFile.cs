using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace jamoram62.tools.MSCompanion
{
    public class MovieStormPropertiesFile
    {

        public static string BackupFilesFolder = Directory.GetCurrentDirectory();

        private static readonly Regex
            AddonStatusRegex = new Regex(@"addon\.[a-z0-9].*\.[a-z0-9].*\.enabled=[tf].*", RegexOptions.IgnoreCase),
            AddonLoadOrderRegex = new Regex(@"addons.loadorder=.*", RegexOptions.IgnoreCase),
            BackupFilenameRegex = new Regex("machinimascope.properties-20[0-9][0-9][01][0-9][0-3][0-9]_[0-2][0-9][0-5][0-9][0-5][0-9]", RegexOptions.IgnoreCase);

        private string _fileName = "";

        /// <summary>
        /// Contenido del fichero
        /// </summary>
        public string FileText { get { return _fileText; } }
        private string _fileText = "";

        /// <summary>
        /// Relación de addons y su estado de carga
        /// </summary>
        public Dictionary<AddOnBasicInfo, bool> AddonEnabledMap { get { return _addonEnabledMap; } }
        private Dictionary<AddOnBasicInfo, bool> _addonEnabledMap = null;

        /// <summary>
        /// Relación de addons en su orden de carga
        /// </summary>
        public List<string> AddonLoadOrder { get { return _addonLoadOrder; } }
        private List<string> _addonLoadOrder = null;

        // -------------------------------------------------------------------------------------------------


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pFilename">Properties file name</param>
        /// <param name="pPackageSet">Packages actually installed</param>
        public MovieStormPropertiesFile(string pFilename)
        {
            if (string.IsNullOrEmpty(pFilename) || ((pFilename = pFilename.Trim()) == ""))
                throw new Exception("File name cannot be null or blankx");
            if (!File.Exists(pFilename))
                throw new Exception("File not found");

            _fileName = pFilename;
        }

        // --------------------------------------------------------------------------------------------

        /// <summary>
        /// Get addon status according to the contents in the file
        /// </summary>
        /// <param name="pPackageSet">Packages actually installed</param>
        public void GetAddonStatus(AddonPackageSet pPackageSet)
        {

            _addonEnabledMap = new Dictionary<AddOnBasicInfo, bool>();
            _addonLoadOrder = new List<string>();
            List<string> addonsInPropertiesFile = new List<string>();

            string line;
            StringBuilder fileTextBuilder = new StringBuilder();
            using (StreamReader reader = new StreamReader(_fileName, true))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    fileTextBuilder.AppendLine(line);
                    string addonName, addonPublisher;
                    bool addonEnabled;
                    if (GetAddonFromLine(line, out addonName, out addonPublisher, out addonEnabled))
                    {
                        AddOnBasicInfo addOnBasicInfo = new AddOnBasicInfo(addonName, addonPublisher);
                        addOnBasicInfo.InPropertiesFile = true;
                        addOnBasicInfo.PackageInfo = pPackageSet.FindPackageByName(addOnBasicInfo.Name,
                            addOnBasicInfo.Publisher);
                        _addonEnabledMap.Add(addOnBasicInfo, addonEnabled);
                        addonsInPropertiesFile.Add(addOnBasicInfo.QualifiedName);                        
                    }
                    else if (AddonLoadOrderRegex.IsMatch(line))
                    {
                        string[] splitStrings = line.Split('=');
                        string[] splitAddons = splitStrings[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string addonLoadedName in splitAddons)
                            _addonLoadOrder.Add(addonLoadedName);
                    }
                }
                reader.Close();
                _fileText = fileTextBuilder.ToString();
            }


            foreach (AddonPackage package in pPackageSet.Packages)
            {
                if (!addonsInPropertiesFile.Contains(package.QualifiedName))
                {
                    AddOnBasicInfo addOnBasicInfo = new AddOnBasicInfo(package.Name, package.Publisher);
                    addOnBasicInfo.InPropertiesFile = false;
                    addOnBasicInfo.PackageInfo = package;
                    _addonEnabledMap.Add(addOnBasicInfo, package.HasAssetFile && package.HasDataFolder);
                }

            }
        }

        /// <summary>
        /// Retrieves info regarding an addon from a line in the file
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pAddonName"></param>
        /// <param name="pAddonPublisher"></param>
        /// <param name="pAddonEnabled"></param>
        /// <returns></returns>
        private bool GetAddonFromLine(string pLine, out string pAddonName, out string pAddonPublisher, out bool pAddonEnabled)
        {
            pAddonName = pAddonPublisher = "";
            pAddonEnabled = false;
            if (!AddonStatusRegex.IsMatch(pLine))
                return false;

            string[] splitStrings = pLine.Split('=');
            if (splitStrings.Length < 2)
                return false;
            pAddonEnabled = splitStrings[1].ToLower().Trim() == "true";

            string[] splitLine = splitStrings[0].Replace("\\ ", " ").Split('.');
            if (splitLine.Length < 3)
                return false;
            pAddonName = splitLine[2];
            pAddonPublisher = splitLine[1];

            return true;
        }


        private string EscapeBlanks(string pText)
        {
            return pText.Replace(" ", "\\ ");
        }


        // ---------------------------------------------------------------------------------------------------- 

        public string UpdateAddonReferences(MovieAddonList pMovieAddonList, AddonGridData pAddonGridData, bool pAppendNotReferenced = true)
        {
            string line;
            List<string> addonList = new List<string>();
            if (pMovieAddonList != null)
            {
                foreach (AddOnBasicInfo item in pMovieAddonList.AddonList)
                    addonList.Add(item.QualifiedName);
            }

            List<string> includedAddonList = new List<string>();

            string temporaryFileName = _fileName + ".$$$";
            // Write the temporary new file
            using (StreamReader reader = new StreamReader(_fileName, true))
            {
                // Encoding oldEncoding = reader.CurrentEncoding;
                using (StreamWriter writer = new StreamWriter(temporaryFileName, false, Encoding.GetEncoding(Globals.EncodingPage)))
                {
                    while ((line = reader.ReadLine()) != null)
                    {

                        string addonName, addonPublisher;
                        bool addonEnabled;
                        if (GetAddonFromLine(line, out addonName, out addonPublisher, out addonEnabled))
                        {
                            AddonGridDataItem addonGridDataItem = pAddonGridData.FindItem(addonName, addonPublisher);
                            if ((addonGridDataItem == null) || (!addonGridDataItem.Installed && !addonGridDataItem.MoviestormPackage))
                                continue;   // remove from the file

                            string qualifiedName = addonPublisher + "." + addonName;
                            if (pMovieAddonList != null)
                            {
                                bool value = (addonList.Contains(qualifiedName));
                                line = string.Format("addon.{0}.{1}.enabled={2}",
                                    EscapeBlanks(addonPublisher), EscapeBlanks(addonName),
                                    value ? "true" : "false"
                                    );
                            }
                            includedAddonList.Add(qualifiedName);
                        }
                        else if (AddonLoadOrderRegex.IsMatch(line))
                        {
                            // Load order line
                            StringBuilder loadOrderLine = new StringBuilder();
                            loadOrderLine.Append("addons.loadOrder=Core,Base01");
                            foreach (AddonGridDataItem item in pAddonGridData.AddonGridDataList)
                            {
                                string addonLoadName = item.Name;
                                if ((addonLoadName != "Core") && (addonLoadName != "Base01") && item.Installed)
                                {
                                    if(pAppendNotReferenced || item.InPropertiesFile)
                                        loadOrderLine.Append("," + addonLoadName);
                                }
                            }
                            line = loadOrderLine.ToString();
                        }
                        writer.WriteLine(line);
                    }
                    writer.Close();
                }
                reader.Close();
            }


            if (pAppendNotReferenced)
            {
                // Appends enabled status of installed addons not previously referenced
                using (
                    StreamWriter writer = new StreamWriter(temporaryFileName, true,
                        Encoding.GetEncoding(Globals.EncodingPage)))
                {
                    foreach (AddonGridDataItem item in pAddonGridData.AddonGridDataList)
                    {
                        if (!item.Installed)
                            continue;
                        if (includedAddonList.Contains(item.QualifiedName))
                            continue;

                        string notReferencedEnabled = "true";
                        if (pMovieAddonList != null)
                        {
                            AddOnBasicInfo addOnBasicInfo = pMovieAddonList.FindByName(item.Name, item.Publisher);
                            notReferencedEnabled = (addOnBasicInfo == null)
                                ? "false"
                                : "true";
                        }

                        writer.WriteLine(
                            string.Format("addon.{0}.{1}.enabled={2}",
                                EscapeBlanks(item.Publisher), EscapeBlanks(item.Name), 
                                notReferencedEnabled
                                )
                            );
                    }
                    writer.Close();
                }
            }


            string backupFile = Path.Combine(
                BackupFilesFolder,
                string.Format("machinimascope.properties-{0}", DateTime.Now.ToString("yyyyMMdd_HHmmss"))
                );
            // Globals.LogIt("  Saving properties file: " + backupFile);
            File.Copy(_fileName, backupFile);
            try
            {
                // Globals.LogIt("Updating properties file");
                File.Delete(_fileName);
                File.Move(temporaryFileName, _fileName);
            }
            catch (Exception exception)
            {
                string errorText = "Error: " + exception.Message;
                // Globals.LogIt(errorText);
                // MessageBox.Show(errorText);

            }

            return backupFile;
        }


        // ---------------------------------------------------------------------------------------------------- 

        /// <summary>
        /// Restore configuration file from a backup created by the application
        /// </summary>
        /// <param name="pBackupFilename">Backup file name</param>
        /// <returns>Operation result</returns>
        public bool Restore(string pBackupFilename)
        {
            if (string.IsNullOrEmpty(pBackupFilename) || !File.Exists(pBackupFilename))
                return false;

            if (!BackupFilenameRegex.IsMatch(Path.GetFileName(pBackupFilename)))
                return false;

            if (!File.Exists(pBackupFilename))
                return false;

            string line = "";
            using (StreamReader reader = new StreamReader(_fileName, true))
            {
                line = reader.ReadLine();
                reader.Close();
            }
            if (string.IsNullOrEmpty(line) || !line.Contains("Machinimascope"))
                return false;

            File.Copy(pBackupFilename, _fileName, true);

            return true;
        }

        // .....................................................................................................

        public AddOnBasicInfo FindPackageByName(string pPackageName)
        {
            foreach (KeyValuePair<AddOnBasicInfo, bool> item in _addonEnabledMap)
                if (item.Key.Name == pPackageName)
                    return item.Key;
            return null;
        }



        public bool? PackageIsEnabled(string pPackageName)
        {
            foreach (KeyValuePair<AddOnBasicInfo, bool> item in _addonEnabledMap)
                if (item.Key.Name == pPackageName)
                    return item.Value;
            return null;
        }
    }
}
