using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace jamoram62.tools.MSCompanion
{
    /// <summary>
    /// Info about addons installed/configured, ready to be displayed in the data gried view.
    /// </summary>
    public class AddonGridData
    {
        /// <summary>
        /// List of addons found
        /// </summary>
        public List<AddonGridDataItem> AddonGridDataList { get { return _addonGridDataList; } }
        private List<AddonGridDataItem> _addonGridDataList = new List<AddonGridDataItem>();

        /// <summary>
        /// Number of addons found
        /// </summary>
        public int PackageCount { get { return _packageCount; } }

        /// <summary>
        /// Number of addons found and enabled
        /// </summary>
        public int PackageEnabledCount { get { return _packageEnabledCount; } }
        private int
            _packageCount = 0,
            _packageEnabledCount = 0;

        /// <summary>
        /// Total size of mesh data files in addons found
        /// </summary>
        public double MeshDataMbytes { get { return _meshDataMbytes; } }

        /// <summary>
        /// Total size of mesh data files in addons found and enabled
        /// </summary>
        public double MeshDataMbytesEnabled { get { return _meshDataMBytesEnabled; } }
        private double
            _meshDataMbytes = 0.0,
            _meshDataMBytesEnabled = 0.0;


        // ---------------------------------------------------------------------------------

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="pAddonEnabledMap">Addon enabled map</param>
        public AddonGridData(Dictionary<AddOnBasicInfo, bool> pAddonEnabledMap)
        {

            List<KeyValuePair<AddOnBasicInfo, bool>> addOnEnabledList = pAddonEnabledMap.ToList();

            // sorts the list of addons in the addon enabled map
            addOnEnabledList.Sort(
                delegate(KeyValuePair<AddOnBasicInfo, bool> firstPair, KeyValuePair<AddOnBasicInfo, bool> nextPair)
                {
                    return firstPair.Key.Name.CompareTo(nextPair.Key.Name);
                }
                );

            foreach (KeyValuePair<AddOnBasicInfo, bool> item in addOnEnabledList)
            {
                // AddonPackage package = pPackageSet.FindPackageByName(item.Key.Name);
                AddonGridDataItem gridItem = new AddonGridDataItem(item.Value, item.Key);
                _addonGridDataList.Add(gridItem);
                _packageCount++;
                if (item.Value)
                    _packageEnabledCount++;
                if (gridItem.MeshDataSizeMbytes.HasValue)
                {
                    _meshDataMbytes += gridItem.MeshDataSizeMbytes.Value;
                    if (item.Value)
                        _meshDataMBytesEnabled += gridItem.MeshDataSizeMbytes.Value;
                }

            }
        }


        // .............................................................................................

        /// <summary>
        /// Searchs an item in the vector by its name and publisher
        /// </summary>
        /// <param name="pAddonName">Addon name</param>
        /// <param name="pPublisher">Addon publisher</param>
        /// <returns>Item found or null</returns>
        public AddonGridDataItem FindItem(string pAddonName, string pPublisher)
        {
            foreach(AddonGridDataItem item in _addonGridDataList)
                if ((item.Name == pAddonName) && (item.Publisher == pPublisher))
                    return item;

            return null;
        }

        /// <summary>
        /// Searchs an item by its qualified name ::= publisher.addon_name
        /// </summary>
        /// <param name="pQualifiedName">Qualified name of the addon</param>
        /// <returns>Item found or null</returns>
        public AddonGridDataItem FindItemByQualifiedName(string pQualifiedName)
        {
            string[] splitStrings = pQualifiedName.Split('.');

            return FindItem(splitStrings[1], splitStrings[0]);
        }



    }


    // ------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Addon information for the data grid view
    /// </summary>
    public class AddonGridDataItem
    {
        /// <summary>
        /// Addon enabled in Moviestorm
        /// </summary>
        public bool Enabled { get { return _enabled; } }
        private bool _enabled = false;


        public string Name { get { return _name; } }
        private string _name = "";

        public string FriendlyName { get { return _friendlyName; } }
        private string _friendlyName = "";

        public string Publisher { get { return _publisher; } }
        private string _publisher = "";


        public string QualifiedName { get { return _publisher + "." + _name; } }

        /// <summary>
        /// Number of props declared in the asset manifest file
        /// </summary>
        public int PropCount { get { return _propCount; } }
        private int _propCount = 0;

        /// <summary>
        /// Number of body parts declared in the asset manifest file
        /// </summary>
        public int BodyCount { get { return _bodyPartCount; } }
        private int _bodyPartCount = 0;


        public int BodyAnimationCount { get { return _bodyAnimationCount; } }
        private int _bodyAnimationCount = 0;


        public int FilterCount { get { return _filterCount; } }
        private int _filterCount = 0;

        public int SoundCount { get { return _soundCount; } }
        private int _soundCount = 0;

        /// <summary>
        /// Size of the mesh data file in MBytes. It can be null (eg, filter/sound only packages)
        /// </summary>
        public double? MeshDataSizeMbytes { get { return _meshDataSizeMbytes; } }
        private double? _meshDataSizeMbytes = null;

        /// <summary>
        /// Actually installed
        /// </summary>
        public bool Installed { get { return _installed; } }
        private bool _installed = false;

        /// <summary>
        /// Referenced in the MS properties file
        /// </summary>
        [DisplayName("In config. file")]
        public bool InPropertiesFile { get { return _inPropertiesFile; } }
        private bool _inPropertiesFile = false;


        /// <summary>
        /// It's a contents package published by Moviestorm
        /// </summary>
        [DisplayName("MS Package")]
        public bool MoviestormPackage { get { return _moviestormPackage; } }
        private bool _moviestormPackage = false;

        /// <summary>
        /// It's a contents package published by Moviestorm
        /// </summary>
        public bool Preview { get { return _preview; } }
        private bool _preview = false;


        /// <summary>
        /// Summary of package info
        /// </summary>
        public string Summary { get { return _summary; } }
        private string _summary = "";


        // -------------------------------------------------------------------------------------------------

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pEnabled">Addon enabled</param>
        /// <param name="pAddonBasicInfo">Basic info about the addon</param>
        public AddonGridDataItem(bool pEnabled, AddOnBasicInfo pAddonBasicInfo)
        {
            _enabled = pEnabled;
            _name = pAddonBasicInfo.Name;
            _publisher = pAddonBasicInfo.Publisher;

            _inPropertiesFile = pAddonBasicInfo.InPropertiesFile;
            _installed = pAddonBasicInfo.Installed;

            if (!_installed && (pAddonBasicInfo.PackageInfo == null))
            {

                _friendlyName =
                    !string.IsNullOrEmpty(pAddonBasicInfo.FriendlyName)
                        ? pAddonBasicInfo.FriendlyName
                        : _name;
                return;
            }

            _preview = pAddonBasicInfo.PackageInfo.Preview;

            _friendlyName =
                !string.IsNullOrEmpty(pAddonBasicInfo.PackageInfo.FriendlyName)
                ? pAddonBasicInfo.PackageInfo.FriendlyName
                : _name;

            _propCount = pAddonBasicInfo.PackageInfo.AssetManifest.propModels.Count;
            
            _bodyPartCount = _bodyAnimationCount = 0;
            foreach (BodyModelItem item in pAddonBasicInfo.PackageInfo.AssetManifest.bodyModels)
            {
                _bodyPartCount += item.parts.Count;
                _bodyAnimationCount += item.animations.Count;
            }

            _filterCount = pAddonBasicInfo.PackageInfo.FilterList().Count;
            _soundCount = pAddonBasicInfo.PackageInfo.SoundList().Count;

            _meshDataSizeMbytes = pAddonBasicInfo.PackageInfo.MeshDataSizeMbytes;

            _moviestormPackage = pAddonBasicInfo.PackageInfo.MoviestormPackage;
            _summary = pAddonBasicInfo.PackageInfo.ToString();
        }

    }

}
