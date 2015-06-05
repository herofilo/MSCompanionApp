using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    public static class Globals
    {
        public const string DefaultConfigurationFileName = "MSCompanionConfig.xml";
        private const string LogFileName = "MSCompanion.log";

        public const int EncodingPage = 1252;

        private const string MoviesFolder = @"\movies",
            MachinimaPropertiesFileName = @"machinimascope.properties";

        public const string MovieSummary = @"movie.summary",
            MovieMScope = @"movie.mscope";



        /// <summary>
        /// Configuration info
        /// </summary>
        public static AppConfiguration ConfigurationInfo = null;


        public static string
            MachinimaPropertiesPath = "",
            MoviesPath = "";


        public static List<string> MovieList = null;
        public static MovieStormPropertiesFile PropertiesFile { get { return _propertiesFile; } }
        private static MovieStormPropertiesFile _propertiesFile = null;

        public static Dictionary<AddOnBasicInfo, bool> AddonEnabledMap
        {
            get
            {
                return (_propertiesFile == null) ? null : _propertiesFile.AddonEnabledMap;
            }
        }


        public static AddonPackageSet PackageSet = null;

        public static Dictionary<string, string>
            MapPropsToPackages = null,
            MapBodyPartsToPackages = null,
            MapFiltersToPackages = null;


        private static TextBox _mainLogTextBox = null;
        private static StreamWriter _LogStream = null;

        public const string HelpFilenameBase = @"MSCompanion.chm";
        public static string
            HelpFilename = "",
            HelpFileUri = "";

        // -------------------------------------------------------------------------------------------


        public static bool Initialization(TextBox pLogTextBox = null)
        {
            _mainLogTextBox = pLogTextBox;
            LogIt("Initializing...");
            if ((ConfigurationInfo = AppConfiguration.LoadConfiguration()) == null)
                return false;

            HelpFilename = Path.Combine(Application.StartupPath, HelpFilenameBase);
            HelpFileUri = (!File.Exists(HelpFilename)) ?
                "" :
                string.Format("file://{0}", HelpFilename);

            return InitPostConfigurationLoad();
        }

        public static bool InitPostConfigurationLoad()
        {
            if (!Directory.Exists(ConfigurationInfo.TmpPath))
                Directory.CreateDirectory(ConfigurationInfo.TmpPath);
            else
            {
                foreach (string file in Directory.GetFiles(ConfigurationInfo.TmpPath))
                    File.Delete(file);
            }

            MovieStormPropertiesFile.BackupFilesFolder = ConfigurationInfo.AppBackupFilesFolder;

            // Retrieves info about the packages actually installed
            PackageSet = new AddonPackageSet(ConfigurationInfo.TmpPath, ConfigurationInfo.MoviestormAppPath, ConfigurationInfo.MoviestormUserDataPath);
            if (PackageSet.ErrorLog.Length > 0)
                LogIt(PackageSet.ErrorLog);

            GetManifests(ConfigurationInfo.MoviestormUserDataPath, PackageSet);

            MapPropsToPackages = PackageSet.MapPropToPackage(_propertiesFile);
            MapBodyPartsToPackages = PackageSet.MapBodyPartToPackage(_propertiesFile);
            MapFiltersToPackages = PackageSet.MapFiltersToPackage(_propertiesFile);

            LogIt("Initialization finished.");

            // _mainLogTextBox.SelectionStart = _mainLogTextBox.Text.Length;
            // _mainLogTextBox.ScrollToCaret();

            return true;

        }

        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get the lists of Addons and movies
        /// </summary>
        /// <param name="pMoviestormUserDataPath">Path to the Moviestorm user data folder</param>
        /// <param name="pPackageSet">Packages actually installed</param>
        public static void GetManifests(string pMoviestormUserDataPath = "", AddonPackageSet pPackageSet = null)
        {
            if (pMoviestormUserDataPath == "")
                pMoviestormUserDataPath = ConfigurationInfo.MoviestormUserDataPath;
            MoviesPath = pMoviestormUserDataPath + MoviesFolder;
            MachinimaPropertiesPath = Path.Combine(pMoviestormUserDataPath, MachinimaPropertiesFileName);

            if (pPackageSet == null)
                pPackageSet = PackageSet;

            LogIt("Getting movie list in Moviestorm user folder...");
            GetMovieList(MoviesPath);
            LogIt("   {0} movies found.", MovieList.Count);

            LogIt("Getting general info about installed addons...");
            _propertiesFile = new MovieStormPropertiesFile(MachinimaPropertiesPath);
            _propertiesFile.GetAddonStatus(pPackageSet);
            // _propertiesFile = new MovieStormPropertiesFile(MachinimaPropertiesPath, pPackageSet);
            LogIt("   {0} addons found.", AddonEnabledMap.Count);
        }

        /// <summary>
        /// Get the list of movies
        /// </summary>
        /// <param name="pMoviesPath">Movies folder</param>
        /// <returns>List of movies</returns>
        public static List<string> GetMovieList(string pMoviesPath)
        {
            if (MovieList == null)
                MovieList = new List<string>();
            else
                MovieList.Clear();

            try
            {
                foreach (string item in Directory.EnumerateDirectories(pMoviesPath))
                {
                    if (File.Exists(Path.Combine(item, MovieSummary)))
                        MovieList.Add(Path.GetFileName(item));
                }


                MovieList.Sort(
                    (first, next) => String.CompareOrdinal(first, next)
                    );

            }
            catch { }
            return MovieList;
        }




        public static string GetMoviePath(string pMovieName)
        {
            return Path.Combine(MoviesPath, pMovieName);
        }

        // --------------------------------------------------------------------------------------------------------

        public static void RefreshAddonListAndStatus()
        {
            LogIt("Refreshing Addons list and status");
            _propertiesFile.GetAddonStatus(PackageSet);
        }


        // --------------------------------------------------------------------------------------------------------

        public static void LogIt(string text, params object[] argsv)
        {
            string textToLog;
            try
            {
                textToLog =
                    (argsv.Length == 0) ?
                    text :
                    String.Format(text, argsv)
                    ;

                if (_mainLogTextBox != null)
                    _mainLogTextBox.AppendText(textToLog + Environment.NewLine);

                LogToFile(textToLog);

            }
            catch (Exception e)
            {
                textToLog = String.Format("Logger.LogIt(): error interno [{0}], texto: '{1}'", e.Message, text);
                if (_mainLogTextBox != null)
                    _mainLogTextBox.AppendText(textToLog + Environment.NewLine);

                LogToFile(textToLog);
            }
        }


        /// <summary>
        /// Registra mensaje a fichero de auditoría externo
        /// </summary>
        /// <param name="text">Cadena de texto a registrar</param>
        public static void LogToFile(string text)
        {
            // log a fichero
            if (_LogStream == null)
                _LogStream = File.AppendText(LogFileName);

            _LogStream.WriteLine(text);
            _LogStream.Flush();
        }


    }
}
