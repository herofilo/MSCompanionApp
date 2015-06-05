using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{
    public class AddOnBasicInfo
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string FriendlyName { get; set; }

        public string QualifiedName
        {
            get { return Publisher + "." + Name; }
        }

        public bool Installed { get { return _installed;  } }
        private bool _installed = false;


        public AddOnBasicInfo(string pName, string pPublisher, string pFriendlyName = "")
        {
            if(string.IsNullOrEmpty(pName) || ((pName = pName.Trim()) == ""))
                throw new Exception("AddOn name cannot be empty or null");
            Name = pName;

            if (string.IsNullOrEmpty(pPublisher) || ((pPublisher = pPublisher.Trim()) == ""))
                throw new Exception("Publisher name cannot be empty or null");
            Publisher = pPublisher;
            FriendlyName = (pFriendlyName == "") ? Name : pFriendlyName;
            IsInstalled();
        }


        public AddOnBasicInfo(string pPropertiesAddonLine)
        {
            string[] splitStrings = pPropertiesAddonLine.Split(new char[] {'.', '='},
                StringSplitOptions.RemoveEmptyEntries);
            if(splitStrings.Length != 5)
                throw new Exception("Not a properties addon line");

            Name = FriendlyName = splitStrings[2];
            Publisher = splitStrings[1];
            IsInstalled();
        }


        public bool IsInstalled(AppConfiguration pConfigurationInfo = null)
        {
            _installed = false;
            try
            {
                if (pConfigurationInfo == null)
                    pConfigurationInfo = Globals.ConfigurationInfo;
                if (pConfigurationInfo == null)
                    throw new Exception("Application configuration not set");
                _installed =
                    File.Exists(string.Format(@"{0}\AddOn\{1}\.AddOn", pConfigurationInfo.MoviestormAppPath, Name)) ||
                    File.Exists(string.Format(@"{0}\AddOn\{1}\.AddOn", pConfigurationInfo.MoviestormUserDataPath, Name));

            }
            catch { }
            return _installed;
        }

    }



    public class MovieAddonList
    {
        private List<AddOnBasicInfo> _addonList = new List<AddOnBasicInfo>();
        public List<AddOnBasicInfo> AddonList { get { return _addonList; } }

        public string MovieName { get { return _movieName; }}
        private string _movieName = "";


        public MovieAddonList(string pMoviePath)
        {
            string movieSummaryFile = Path.Combine(pMoviePath, Globals.MovieSummary);
            if (!File.Exists(movieSummaryFile))
                return;

            _movieName = Path.GetFileName(pMoviePath);


            XmlDocument summaryXmlDocument = new XmlDocument();
            summaryXmlDocument.Load(movieSummaryFile);

            // summaryXmlDocument.
            XmlNode rootNode = summaryXmlDocument.FirstChild;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name.ToLower() == "addonsused")
                {
                    foreach (XmlNode addonEntryNode in node.ChildNodes)
                    {
                        if(addonEntryNode.Name.ToLower() != "addonentry")
                            continue;

                        string name = "", publisher = "", friendly = "";
                        foreach (XmlNode detailNode in addonEntryNode.ChildNodes)
                        {
                            switch (detailNode.Name.ToLower())
                            {
                                case "name" : name = detailNode.InnerText; break;
                                case "publisher": publisher = detailNode.InnerText; break;
                                case "friendly": friendly = detailNode.InnerText; break;
                            }
                        }
                        if ((name != "") && (publisher != ""))
                            _addonList.Add(new AddOnBasicInfo(name, publisher, friendly));
                    }

                    _addonList.Sort(
                        (first, next) => System.String.CompareOrdinal(first.Name, next.Name)
                        );
                    break;
                }
            }
        }
    }
}
