using System;

namespace jamoram62.tools.MSCompanion
{
    /// <summary>
    /// Basic information of the addon
    /// </summary>
    public class AddOnBasicInfo
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string FriendlyName { get; set; }

        /// <summary>
        /// Full qualified name (publisher.name)
        /// </summary>
        public string QualifiedName
        {
            get { return Publisher + "." + Name; }
        }

        /// <summary>
        /// Addon actually installed
        /// </summary>
        public bool Installed { get { return ((_packageInfo != null) && !_packageInfo.Preview); } }
        

        /// <summary>
        /// Addon referenced in Moviestorm configuration file
        /// </summary>
        public bool InPropertiesFile { get { return _inPropertiesFile; } set { _inPropertiesFile = value; } }
        private bool _inPropertiesFile = false;

        /// <summary>
        /// Full package info instance
        /// </summary>
        public AddonPackage PackageInfo { get { return _packageInfo; } set { _packageInfo = value; } }
        private AddonPackage _packageInfo = null;

        // ---------------------------------------------------------------------------------------

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pName">Addon name</param>
        /// <param name="pPublisher">Addon publisher</param>
        /// <param name="pFriendlyName">Friendly name</param>
        public AddOnBasicInfo(string pName, string pPublisher, string pFriendlyName = "")
        {
            if(string.IsNullOrEmpty(pName))
                throw new Exception("AddOn name cannot be empty or null");
            Name = pName;

            if (string.IsNullOrEmpty(pPublisher))
                throw new Exception("Publisher name cannot be empty or null");
            Publisher = pPublisher;
            FriendlyName = (pFriendlyName == "") ? Name : pFriendlyName;
            // IsInstalled();
        }


        /// <summary>
        /// Constructs an instance from a line in Moviestorm configuration file
        /// </summary>
        /// <param name="pPropertiesAddonLine">Line of the file</param>
        public AddOnBasicInfo(string pPropertiesAddonLine)
        {
            string[] splitStrings = pPropertiesAddonLine.Split(new char[] {'.', '='},
                StringSplitOptions.RemoveEmptyEntries);
            if(splitStrings.Length != 5)
                throw new Exception("Not a properties addon line");

            Name = FriendlyName = splitStrings[2];
            Publisher = splitStrings[1];
            // IsInstalled();
        }

    }
}
