using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace jamoram62.tools.MSCompanion
{
    /// <summary>
    /// Detailed information about a addon package
    /// </summary>
    public class AddonPackage
    {
        /// <summary>
        /// Name of the manifesto file
        /// </summary>
        private const string ManifestFilename = "ASSET_DATA.MF";

        private const double BytesPerMegabyte = 1024.0*1024.0;

        public string Name {get { return _addonBasicInfoFile.name;  }}

        public string FriendlyName { get { return string.IsNullOrEmpty(_friendlyName) ? _addonBasicInfoFile.name : _friendlyName; } }
        private string _friendlyName = "";

        public string Publisher { get { return _addonBasicInfoFile.Publisher; } }

        public string QualifiedName
        {
            get { return Publisher + "." + Name; }
        }


        public bool Free { get { return _addonBasicInfoFile.free; } }

        public string Description { get { return _addonBasicInfoFile.description; } }

        public string Blurb { get { return _blurb; } }
        private string _blurb = "";

        public string Revision { get { return _revision; } }
        private string _revision = "";

        private AddonBasicInfoFile _addonBasicInfoFile = null;


        public string AddonPath { get { return _addonPath; } }
        private string _addonPath = "";

        public bool MoviestormPackage { get { return _moviestormPackage; } }
        private bool _moviestormPackage = false;

        public bool Preview { get { return _preview; } }
        private bool _preview = false;


        /// <summary>
        /// Asset manifest contents
        /// </summary>
        public AssetManifest AssetManifest { get { return _assetManifest; } }
        private AssetManifest _assetManifest = null;


        public double? MeshDataSizeMbytes { get { return _meshDataSizeMbytes; } }
        private double? _meshDataSizeMbytes = null;

        public bool HasAddonFile { get { return _hasAddOnFile; } }
        public bool HasAssetFile { get { return _hasAssetFile; } }
        public bool HasDataFolder { get { return _hasDataFolder; } }

        private bool
            _hasAddOnFile = false,
            _hasAssetFile = false,
            _hasDataFolder = false;


        public bool HasIssues { get { return !string.IsNullOrEmpty(_issuesStringBuilder.ToString()); } }
        public string Issues { get { return _issuesStringBuilder.ToString(); } }
        private StringBuilder _issuesStringBuilder = new StringBuilder();


        // -----------------------------------------------------------------------------------------------

        public AddonPackage(string pAddonPath, string pTemporaryFolder, bool pMoviestormPackage, bool pPreview)
        {
            _addonPath = pAddonPath;
            _moviestormPackage = pMoviestormPackage;
            _preview = pPreview;

            RetrieveAddonBasicInfo(pAddonPath);
            try
            {
                RetrieveAssetManifest(pAddonPath, pTemporaryFolder);
            }
            catch (Exception exception)
            {
                _issuesStringBuilder.AppendLine(exception.Message);
            }
            RetrieveVersionInfo(pAddonPath);
            RetrievePropertiesInfo(pAddonPath);

            _meshDataSizeMbytes = GetMeshDataSize();

            _hasDataFolder = Directory.Exists(Path.Combine(pAddonPath, "Data"));

        }



        /// <summary>
        /// Retrieves addon basic info from .AddOn file
        /// </summary>
        /// <param name="pAddonPath">Path to the .AddOn file</param>
        private void RetrieveAddonBasicInfo(string pAddonPath)
        {
            string addOnFile = Path.Combine(pAddonPath, ".AddOn");

            _addonBasicInfoFile = AddonBasicInfoFile.Load(addOnFile);
            _hasAddOnFile = true;
        }


        /// <summary>
        /// Retrieves the asset manifest from the file inside the assetData.jar class library
        /// </summary>
        /// <param name="pAddonPath">Path to the addon</param>
        /// <param name="pTemporaryFolder">Path to temporary folder</param>
        private void RetrieveAssetManifest(string pAddonPath, string pTemporaryFolder)
        {
            string assetDataFile = Path.Combine(pAddonPath, "assetData.jar");
            if (!File.Exists(assetDataFile))
                throw new Exception("No assetData.jar file");
            _hasAssetFile = true;

            string manifestFile = Path.Combine(pTemporaryFolder, ManifestFilename);
            if (File.Exists(manifestFile))
                File.Delete(manifestFile);

            List<string> filesToExtractList = new List<string> { ManifestFilename };
            if (Compress.ZipArchiveExtract(assetDataFile, pTemporaryFolder, filesToExtractList) <= 0)
                throw new Exception("Manifest file extraction failed");
            if (!File.Exists(manifestFile))
                throw new Exception("Manifest file not found");

            AssetManifest assetManifest = AssetManifest.Load(manifestFile);
            if (assetManifest == null)
                throw new Exception("Error loading manifest file");

            _assetManifest = assetManifest;
        }


        private void RetrieveVersionInfo(string pAddonPath)
        {
            string versionFile = Path.Combine(pAddonPath, "version.xml");

            AddonVersionInfo addonVersionInfo = AddonVersionInfo.Load(versionFile);
            if(addonVersionInfo != null)
                _revision = addonVersionInfo.revision;
        }

        private void RetrievePropertiesInfo(string pAddonPath)
        {

            string propertiesInfo = Path.Combine(pAddonPath, ".properties");

            AddonPropertiesInfo addonPropertiesInfo = AddonPropertiesInfo.Load(propertiesInfo);
            if (addonPropertiesInfo != null)
            {
                _friendlyName = addonPropertiesInfo.Name;
                _blurb = addonPropertiesInfo.Blurb;
            }
            else
            {
                _friendlyName = Name;
            }
        }


        // ...........................................................................................

        public List<string> PropList()
        {
            List<string> propList = new List<string>();

            foreach (PropModelItem propModelItem in AssetManifest.propModels)
            {
                foreach (Template template in propModelItem.templates)
                {
                    string templateName = template.name.Replace("Data/Props/", "").Replace(".template", "");
                    propList.Add(templateName);
                }
            }
            return propList;
        }


        public List<string> BodyPartList()
        {
            List<string> bodyPartList = new List<string>();

            foreach (BodyModelItem bodyModelItem in AssetManifest.bodyModels)
            {
                foreach (BodyPart item in bodyModelItem.parts)
                {
                    string bodyPartName = item.name.Replace("Data/Puppets/", "").Replace(".bodypart", "");
                    bodyPartList.Add(bodyPartName);
                }


            }
            return bodyPartList;
        }


        public List<string> BodyAnimationList()
        {
            List<string> bodyPartList = new List<string>();

            foreach (BodyModelItem bodyModelItem in AssetManifest.bodyModels)
            {
                foreach (string item in bodyModelItem.animations)
                {
                    string bodyPartName =
                        string.Format("{0}:{1}", 
                            bodyModelItem.name, 
                            item.Replace("Animations/", "").Replace(".CAF", ""));
                    bodyPartList.Add(bodyPartName);
                }


            }
            return bodyPartList;
        }



        public List<string> FilterList()
        {
            List<string> filterList = new List<string>();

            foreach (string file in _addonBasicInfoFile.files)
            {
                if (file.Contains("Filters"))
                {
                    string filterName = file.Replace("Data/CuttingRoom/Filters/", "");
                    filterName = Path.GetDirectoryName(filterName);
                    if(!filterList.Contains(filterName))
                        filterList.Add(filterName);
                }
            }
                return filterList;
        }


        public List<string> SoundList()
        {
            List<string> soundList = new List<string>();

            foreach (string file in _addonBasicInfoFile.files)
            {
                if (file.Contains("Data/Sound/"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    soundList.Add(fileName);
                }
            }
            return soundList;
        }




        private double? GetMeshDataSize()
        {
            string meshDataFilename = Path.Combine(_addonPath, "meshData.data");
            if (!File.Exists(meshDataFilename))
                return null;

            FileInfo fileInfo = new FileInfo(meshDataFilename);
            return fileInfo.Length/BytesPerMegabyte;
        }


        // -----------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder summary = new StringBuilder();
            summary.AppendLine(string.Format("Name: {0}", Name));
            summary.AppendLine(string.Format("FriendlyName: {0}", _friendlyName));
            summary.AppendLine(string.Format("Publisher: {0}", Publisher));
            summary.AppendLine(string.Format("Free: {0}", Free));
            summary.AppendLine(string.Format("Description: {0}", Description));
            summary.AppendLine(string.Format("Blurb: {0}", _blurb));
            summary.AppendLine(string.Format("Revision: {0}", _revision));
            summary.AppendLine(string.Format("Path: {0}", _addonPath));
            summary.AppendLine(string.Format("Mesh Data File size (MB): {0:#.###}", _meshDataSizeMbytes));

            List<string> assetList = PropList();
            if (assetList.Count > 0)
            {
                summary.AppendLine("Props:");
                foreach(string assetName in assetList)
                    summary.AppendLine(string.Format("  {0}", assetName));
            }

            assetList = BodyPartList();
            if (assetList.Count > 0)
            {
                summary.AppendLine("Body parts:");
                foreach (string assetName in assetList)
                    summary.AppendLine(string.Format("  {0}", assetName));
            }

            assetList = BodyAnimationList();
            if (assetList.Count > 0)
            {
                summary.AppendLine("Body animations:");
                foreach (string assetName in assetList)
                    summary.AppendLine(string.Format("  {0}", assetName));
            }



            List<string> miscList = FilterList();
            if (miscList.Count > 0)
            {
                summary.AppendLine("Filters:");
                foreach (string item in miscList)
                    summary.AppendLine(string.Format("  {0}", item));
            }

            miscList = SoundList();
            if (miscList.Count > 0)
            {
                summary.AppendLine("Sounds:");
                foreach (string item in miscList)
                    summary.AppendLine(string.Format("  {0}", item));
            }

            return summary.ToString();
        }


    }

    // -------------------------------------------------------------------------------------------------

    public class AssetManifest
    {
        [XmlArrayItem("Prop")]
        public List<PropModelItem> propModels = new List<PropModelItem>();

        [XmlArrayItem("Body")]
        public List<BodyModelItem> bodyModels = new List<BodyModelItem>();



        public AssetManifest()
        {


        }


        public static AssetManifest Load(string pFilename)
        {
            if (!File.Exists(pFilename))
                return null;

            AssetManifest assetManifest = new AssetManifest();

            try
            {
                XmlSerializer serializer = new XmlSerializer(assetManifest.GetType());
                using (StreamReader reader = new StreamReader(pFilename))
                {
                    assetManifest = (AssetManifest)serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                assetManifest = null;
            }

            return assetManifest;
        }
    }


    public class PropModelItem
    {
        // <tags/>

        [XmlArrayItem("Entry")]
        public List<Template> templates = new List<Template>();

        public List<ModelPart> parts = new List<ModelPart>();


        public string skeleton;

        [XmlArrayItem("string")]
        public List<string> meshes = new List<string>();

        // <animations/>

        [XmlArrayItem("string")]
        public List<string> animations = new List<string>();


        public string name;

    }


    public class Template
    {
        public string name { get; set; }
    }


    public class ModelPart
    {
        public int slot { get; set; }
        public string name { get; set; }
    }


    public class BodyModelItem
    {
        //   <templates/>

        public List<BodyPart> parts = new List<BodyPart>();


        [XmlArrayItem("string")]
        public List<string> meshes = new List<string>();


        [XmlArrayItem("string")]
        public List<string> animations = new List<string>();

        public string name { get; set; }

    }

    public class BodyPart
    {
        // public BodyPartPartsCovered partsCovered;
        public string partsCovered { get; set; }

        public string instanceClass { get; set; }
        public string name { get; set; }
    }

    // --------------------------------------------------------------------------------------------------

    [XmlRoot("addon")]
    public class AddonBasicInfoFile
    {

        public string name;

        public string description;

        public bool free = false;

        [XmlArrayItem("string")]
        public List<string> files = new List<string>();

        public string Publisher;


        // .......................................................................................


        public static AddonBasicInfoFile Load(string pFileName)
        {
            if (!File.Exists(pFileName))
                throw new Exception("No .AddOn file");



            byte[] addOnFileContents = File.ReadAllBytes(pFileName);

            byte[] lengthBytes = addOnFileContents.Take(4).ToArray();
            int length = BitConverter.ToInt32(lengthBytes.Reverse().ToArray(), 0);

            string developer = Encoding.Default.GetString(addOnFileContents, 4, length);

            // BitConverter.T .ToString(addOnFileContents, 4, length);
            byte[] startBytes = Encoding.ASCII.GetBytes("<addon>");

            int position = -1;
            for (int idx = 4 + length; idx < (addOnFileContents.Length - startBytes.Length); ++idx)
            {
                if (addOnFileContents[idx] == '<')
                {
                    position = idx;
                    for (int srcIdx = idx, destIdx = 0, count = 0; count < startBytes.Length; ++srcIdx, ++destIdx, ++count)
                        if (addOnFileContents[srcIdx] != startBytes[destIdx])
                        {
                            position = -1;
                            break;
                        }
                    if (position > 0)
                        break;
                }
            }

            if (position < 0)
                throw new Exception("Invalid format .AddOn file");


            string addonXmlText = Encoding.Default.GetString(addOnFileContents, position,
                    addOnFileContents.Length - position);

            AddonBasicInfoFile addonBasicInfoFile = new AddonBasicInfoFile();
            try
            {
                XmlSerializer serializer = new XmlSerializer(addonBasicInfoFile.GetType());
                using (StringReader reader = new StringReader(addonXmlText))
                {
                    addonBasicInfoFile = (AddonBasicInfoFile)serializer.Deserialize(reader);
                    reader.Close();
                }
                addonBasicInfoFile.Publisher = developer;
            }
            catch
            {
                addonBasicInfoFile = null;
            }

            return addonBasicInfoFile;
        }




    }


    // -----------------------------------------------------------------------------------------------

    [XmlRoot("patch")]
    public class AddonVersionInfo
    {
        public string component;
        public string revision;



        public static AddonVersionInfo Load(string pFilename)
        {
            if (!File.Exists(pFilename))
                return null;

            AddonVersionInfo addonVersionInfo = new AddonVersionInfo();

            try
            {
                XmlSerializer serializer = new XmlSerializer(addonVersionInfo.GetType());
                using (StreamReader reader = new StreamReader(pFilename))
                {
                    addonVersionInfo = (AddonVersionInfo)serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                addonVersionInfo = null;
            }

            return addonVersionInfo;
        }



    }


    public class AddonPropertiesInfo
    {
        public string Name;
        public string Blurb;
        public string Url;

        public static AddonPropertiesInfo Load(string pFilename)
        {
            if (!File.Exists(pFilename))
                return null;

            AddonPropertiesInfo addonPropertiesInfo = new AddonPropertiesInfo();

            using (TextReader reader = File.OpenText(pFilename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitStrings = line.Split('=');
                    if (splitStrings.Length > 1)
                    {
                        switch (splitStrings[0].ToLower())
                        {
                            case "name": addonPropertiesInfo.Name = splitStrings[1]; break;
                            case "blurb": addonPropertiesInfo.Blurb = splitStrings[1]; break;
                            case "url": addonPropertiesInfo.Url = splitStrings[1]; break;
                        }
                    }
                }
                reader.Close();
            }
            return addonPropertiesInfo;
        }
    }

}
