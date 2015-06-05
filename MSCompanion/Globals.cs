using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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


        public static Regex AddonStatusRegex = new Regex(@"addon\.[a-z0-9].*\.[a-z0-9].*\.enabled=[tf].*", RegexOptions.IgnoreCase);


        /// <summary>
        /// Configuration info
        /// </summary>
        public static AppConfiguration ConfigurationInfo = null;


        public static string
            MachinimaPropertiesPath = "",
            MoviesPath = "";


        public static List<string> MovieList = null;
        public static Dictionary<AddOnBasicInfo, bool> AddonEnabledMap = null;


        private static System.Windows.Forms.TextBox _mainLogTextBox = null;
        private static StreamWriter _LogStream = null;

        public static bool Initialization(TextBox pLogTextBox = null)
        {
            _mainLogTextBox = pLogTextBox;
            LogIt("Initializing...");
            if ((ConfigurationInfo = AppConfiguration.LoadConfiguration()) == null)
            {
                // MessageBox.Show("Something's gone wrong. Can't find Moviestorm folders. ")
                FormSettings settingsForm = new FormSettings();
                if (settingsForm.ShowDialog() != DialogResult.OK)
                    return false;
            }

            if (!Directory.Exists(ConfigurationInfo.TmpPath))
                Directory.CreateDirectory(ConfigurationInfo.TmpPath);
            else
            {
                foreach(string file in Directory.GetFiles(ConfigurationInfo.TmpPath))
                    File.Delete(file);
            }

            GetManifests(ConfigurationInfo.MoviestormUserDataPath);


            // TODO : crear lista de assets
            AddonPackageSet packageSet = new AddonPackageSet(ConfigurationInfo.TmpPath, ConfigurationInfo.MoviestormAppPath, ConfigurationInfo.MoviestormUserDataPath);

            LogIt("Initialization finished.");

            // _mainLogTextBox.SelectionStart = _mainLogTextBox.Text.Length;
            // _mainLogTextBox.ScrollToCaret();

            return true;

        }

        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get the lists of Addons and movies
        /// </summary>
        /// <param name="pMoviestormUserDataPath"></param>
        public static void GetManifests(string pMoviestormUserDataPath = "")
        {
            if (pMoviestormUserDataPath == "")
                pMoviestormUserDataPath = ConfigurationInfo.MoviestormUserDataPath;
            MoviesPath = pMoviestormUserDataPath + MoviesFolder;
            MachinimaPropertiesPath = Path.Combine(pMoviestormUserDataPath, MachinimaPropertiesFileName);

            LogIt("Getting movie list in Moviestorm user folder...");
            GetMovieList(MoviesPath);
            LogIt("   {0} movies found.", MovieList.Count);

            LogIt("Getting general info about installed addons...");
            MachinimaPropertiesFile propertiesFile = new MachinimaPropertiesFile(MachinimaPropertiesPath);
            AddonEnabledMap = propertiesFile.AddonEnabledMap;
            LogIt("   {0} addons found.", AddonEnabledMap.Count);
        }

        /// <summary>
        /// Get the list of movies
        /// </summary>
        /// <param name="pMoviesPath">Movies folder</param>
        /// <returns>List of movies</returns>
        public static List<string> GetMovieList(string pMoviesPath)
        {
            if(MovieList == null)
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
                    (first, next) => System.String.CompareOrdinal(first, next)
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
            MachinimaPropertiesFile propertiesFile = new MachinimaPropertiesFile(Globals.MachinimaPropertiesPath);
            AddonEnabledMap = propertiesFile.AddonEnabledMap;
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
