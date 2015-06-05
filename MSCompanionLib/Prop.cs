using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{


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
            // TODO : requiere amplia revisión, buscando un nodo "template"
            if (currentNode.Name.ToLower() == "template")
            {
                string propTemplate = "";
                try
                {
                    propTemplate = currentNode.Attributes["name"].Value;
                    if (_propList.ContainsKey(propTemplate))
                        _propList[propTemplate]++;
                    else
                        _propList.Add(propTemplate, 1);
                }
                catch { }
            }

            foreach (XmlNode childNode in currentNode.ChildNodes)
                SearchPropXmlNode(childNode);
        }


        /*
        private void SearchPropXmlNode(XmlNode currentNode)
        {
            // TODO : requiere amplia revisión, buscando un nodo "template"
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
        */


    }


}
