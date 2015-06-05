using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{
    /* --- NOT USED ---
    public class  Prop
    {
        public string Name { get; set; }

        public string Template { get; set; }

        // ...........................................................................................

        public Prop(string pName, string pTemplate)
        {
            Name = pName;
            Template = pTemplate;
        }

    }
     */

    public class MoviePropList 
    {
        private Dictionary<string, int> _propList = new Dictionary<string, int>();

        public Dictionary<string, int> PropList
        {
            get { return _propList; }
        }

        public string MovieName
        {
            get { return _movieName; }
        }

        private string _movieName = "";


        public MoviePropList(string pMoviePath)
        {
            string movieDetailFile = Path.Combine(pMoviePath, Globals.MovieMScope);
            if (!File.Exists(movieDetailFile))
                return;

            _movieName = Path.GetFileName(pMoviePath);


            XmlDocument detailXmlDocument = new XmlDocument();
            detailXmlDocument.Load(movieDetailFile);

            SearchPropXmlNode(detailXmlDocument.FirstChild);
        }

        private void SearchPropXmlNode(XmlNode currentNode)
        {
            if (currentNode.Name.ToLower() == "prop")
            {
                string propName = "", propTemplate = "";
                foreach (XmlNode childNode in currentNode.ChildNodes)
                {
                    string childName;
                    if ((childName = childNode.Name.ToLower()) == "name")
                        propName = childNode.InnerText;
                    else if (childName == "template")
                    {
                        try
                        {
                            propTemplate = childNode.Attributes["name"].Value;
                        }
                        catch { }
                    } else if (childName == "children")
                        SearchPropXmlNode(childNode);

                }
                string key =
                    (propTemplate != "")
                        ? propTemplate
                        : ((propName != "") ? propName : "");
                if (key != "")
                {
                    if (_propList.ContainsKey(key))
                        _propList[key]++;
                    else
                        _propList.Add(key, 1);                    
                }

                return;
            }

            foreach(XmlNode childNode in currentNode.ChildNodes)
                SearchPropXmlNode(childNode);
        }



    }


}
