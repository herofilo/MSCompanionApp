using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{
    /// <summary>
    /// List of addons used in a movie
    /// </summary>
    public class MovieAddonList
    {
        /// <summary>
        /// List of addons used by the movie
        /// </summary>
        public List<AddOnBasicInfo> AddonList { get { return _addonList; } }
        private List<AddOnBasicInfo> _addonList = new List<AddOnBasicInfo>();
        
        /// <summary>
        /// Movie name
        /// </summary>
        public string MovieName { get { return _movieName; }}
        private string _movieName = "";

        // ................................................................................................

        /// <summary>
        /// Constructor
        /// <remarks>
        /// Essentialy, it extracts the info about used addons from the movie's summary file
        /// </remarks>
        /// </summary>
        /// <param name="pMoviePath">Path to the movie folder</param>
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
                        (first, next) => String.CompareOrdinal(first.Name, next.Name)
                        );
                    break;
                }
            }
        }

        public AddOnBasicInfo FindByName(string pName, string pPublisher)
        {
            return FindByQualifiedName(pPublisher + "." + pName);
        }

        public AddOnBasicInfo FindByQualifiedName(string pQualifiedName)
        {
            foreach (AddOnBasicInfo item in _addonList)
            {
                if (pQualifiedName == item.QualifiedName)
                    return item;
            }

            return null;
        }


    }
}