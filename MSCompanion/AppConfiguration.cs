// #define FAKEPATH_INSTALL
// #define FAKEPATH_USERDATA
#define REMOVE_CONFIGFILE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Serialization;

namespace jamoram62.tools.MSCompanion
{
    [Serializable]
    public class AppConfiguration : ICloneable
    {
        public string MoviestormAppPath
        {
            get { return _appPath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _appPath = "";
                    return;
                }
                _appPath = value.Trim().ToLower();
            }
        }
        private string _appPath = "";

        public string MoviestormUserDataPath
        {
            get { return _userDataPath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _userDataPath = "";
                    return;
                }
                _userDataPath = value.Trim().ToLower();
            }
        }
        private string _userDataPath = "";


        public string TmpPath
        {
            get { return _tmpPath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _tmpPath = "";
                    return;
                }
                _tmpPath = value.Trim().ToLower();
            }           
        }
        private string _tmpPath = "";


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppConfiguration()
        {

            _tmpPath = Path.Combine(Path.GetTempPath(), "MSCompanionTemp");

            string execPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string temptativePath;
#if FAKEPATH_INSTALL
            MoviestormAppPath = execPath + @"\..\..\TestData\MsInstallDir";
#else
            temptativePath =
                Environment.GetFolderPath((Environment.Is64BitOperatingSystem)
                    ? Environment.SpecialFolder.ProgramFilesX86
                    : Environment.SpecialFolder.ProgramFiles) +  @"\moviestorm";

            if (Directory.Exists(temptativePath) && File.Exists(temptativePath + @"\moviestorm.exe"))
                MoviestormAppPath = temptativePath;
#endif

#if FAKEPATH_USERDATA
            MoviestormUserDataPath = execPath + @"\..\..\TestData\UserDataDir";
#else
            temptativePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\moviestorm";
            // if (Directory.Exists(temptativePath) && Directory.Exists(temptativePath + @"\movies"))
            if (Directory.Exists(temptativePath) && Directory.Exists(temptativePath + @"\movies") && File.Exists(temptativePath + @"\machinimascope.properties"))
                MoviestormUserDataPath = temptativePath;

            temptativePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\moviestorm";
            // if (Directory.Exists(temptativePath) && Directory.Exists(temptativePath + @"\movies"))
            if (Directory.Exists(temptativePath) && Directory.Exists(temptativePath + @"\movies") && File.Exists(temptativePath + @"\machinimascope.properties"))
                MoviestormUserDataPath = temptativePath;
#endif

        }


        /// <summary>
        /// Load configuration from file
        /// </summary>
        /// <param name="pFileName">File name</param>
        /// <returns>Configuration loaded or null if error</returns>
        public static AppConfiguration LoadConfiguration(string pFileName = "")
        {

            if (pFileName.Trim() == "")
                pFileName = Globals.DefaultConfigurationFileName;

#if FAKEPATH_INSTALL || FAKEPATH_USERDATA
            if(File.Exists(pFileName))
                File.Delete(pFileName);
#endif
#if REMOVE_CONFIGFILE
            if(File.Exists(Globals.DefaultConfigurationFileName))
                File.Delete(Globals.DefaultConfigurationFileName);
#endif

            AppConfiguration configurationInfo = new AppConfiguration();
            if (!File.Exists(pFileName))
            {
                configurationInfo.SaveConfiguration();
                return configurationInfo;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(configurationInfo.GetType());
                using (StreamReader reader = new StreamReader(pFileName))
                {
                    configurationInfo = (AppConfiguration)serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch { }
            return configurationInfo;
        }


        /// <summary>
        /// Saves configuration to file
        /// </summary>
        /// <param name="pFileName">File name</param>
        /// <returns>Operation result status</returns>
        public bool SaveConfiguration(string pFileName = "")
        {

            if (pFileName.Trim() == "")
                pFileName = Globals.DefaultConfigurationFileName;

            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                using (StreamWriter writer = new StreamWriter(pFileName, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, this);
                    writer.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;           
        }

        /// <summary>
        /// Check Moviestorm user path
        /// </summary>
        /// <param name="pErrorText">Detailed reason of error</param>
        /// <returns>Check result</returns>
        public bool CheckUserPath(out string pErrorText)
        {
            pErrorText = "";
            if (_userDataPath == "")
            {
                pErrorText = "Path not set";
                return false;
            }

            if (!Directory.Exists(_userDataPath))
            {
                pErrorText = "Path not found";
                return false;
            }

            if (!Directory.Exists(_userDataPath + @"\movies"))
            {
                pErrorText = "Movies Path not found";
                return false;
            }

            if (File.Exists(_userDataPath + @"\machinimascope.properties"))
            {
                pErrorText = "Properties file not found";
                return false;
            }

            return true;
        }

        // --------------------------------------------------------------------------------------------------------


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
