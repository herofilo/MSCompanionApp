﻿// #define FAKEPATH_INSTALL
// #define FAKEPATH_USERDATA
// #define REMOVE_CONFIGFILE
#define MSCFILES_IN_MSUSERPATH
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace jamoram62.tools.MSCompanion
{
    [Serializable]
    public class AppConfiguration : ICloneable
    {

        private const string
            BackupFilesFolder = "BackupFiles",
            TempFilesFolder = "Temp";

        /// <summary>
        /// Path to the Moviestorm installation folder
        /// </summary>
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

        /// <summary>
        /// Path to Moviestorm's user data folder
        /// </summary>
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

        /// <summary>
        /// Path to the MSCompanion data root folder
        /// </summary>
        public string MscAppPath { get { return _mscAppPath; } }
        private string _mscAppPath = "";

        /// <summary>
        /// Path to the temporary folder used by MSCompanion
        /// </summary>
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
        /// Path to the backup files generated by MSCompanion
        /// </summary>
        public string AppBackupFilesFolder
        {
            get { return _appBackupFilesFolder; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _appBackupFilesFolder = "";
                    return;
                }
                string temporaryPath = value.Trim().ToLower();
                try
                {
                    if (!Directory.Exists(temporaryPath))
                        Directory.CreateDirectory(temporaryPath);
                    string temporaryFile = Path.Combine(temporaryPath, "testfile.$$$");
                    if(File.Exists(temporaryFile))
                        File.Delete(temporaryFile);
                    FileStream fileStream = File.Create(temporaryFile);
                    fileStream.Close();
                    File.Delete(temporaryFile);
                    _appBackupFilesFolder = temporaryPath;
                }
                catch
                {
                    
                }
                
            }
        }
        private string _appBackupFilesFolder = "";

        // ------------------------------------------------------------------------------------------------

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppConfiguration()
        {


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
            _mscAppPath = Path.Combine(MoviestormUserDataPath, "MSCompanion");
#if MSCFILES_IN_MSUSERPATH
            _tmpPath = Path.Combine(_mscAppPath, TempFilesFolder);
            AppBackupFilesFolder = Path.Combine(_mscAppPath, BackupFilesFolder);
#else
            _tmpPath = Path.Combine(Path.GetTempPath(), "MSCompanionTemp");

            AppBackupFilesFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), BackupFilesFolder);
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

        /// <summary>
        /// Required to implement the ICloneable interface
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}