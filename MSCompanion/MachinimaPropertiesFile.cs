using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace jamoram62.tools.MSCompanion
{
    class MachinimaPropertiesFile
    {
        private static readonly Regex 
            AddonStatusRegex = new Regex(@"addon\.[a-z0-9].*\.[a-z0-9].*\.enabled=[tf].*", RegexOptions.IgnoreCase),
            AddonLoadOrderRegex = new Regex(@"addons.loadorder=.*", RegexOptions.IgnoreCase);


        /// <summary>
        /// Contenido del fichero
        /// </summary>
        public string FileText { get { return _fileText; } }
        private string _fileText = "";

        /// <summary>
        /// Relación de addons y su estado de carga
        /// </summary>
        public Dictionary<AddOnBasicInfo, bool> AddonEnabledMap {get {return _addonEnabledMap; }}
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
        /// <param name="pFilename">Nombre del fichero</param>
        public MachinimaPropertiesFile(string pFilename)
        {

            _addonEnabledMap = new Dictionary<AddOnBasicInfo, bool>();
            _addonLoadOrder = new List<string>();

            string line;
            StringBuilder fileTextBuilder = new StringBuilder();
            using (StreamReader reader = new StreamReader(pFilename, true))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    fileTextBuilder.AppendLine(line);
                    if (AddonStatusRegex.IsMatch(line))
                    {
                        string[] splitStrings = line.Split('=');
                        bool addonEnabled = splitStrings[1].ToLower().Trim() == "true";

                        string[] splitLine = splitStrings[0].Replace("\\ ", " ").Split('.');

                        AddOnBasicInfo addOnBasicInfo = new AddOnBasicInfo(splitLine[2], splitLine[1]);
                        _addonEnabledMap.Add(addOnBasicInfo, addonEnabled);
                    }
                    else if (AddonLoadOrderRegex.IsMatch(line))
                    {
                        string[] splitStrings = line.Split('=');
                        string[] splitAddons = splitStrings[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach(string addonName in splitAddons)
                            _addonLoadOrder.Add(addonName);
                    }
                }
                reader.Close();
                _fileText = fileTextBuilder.ToString();
            }
        }

    }
}
