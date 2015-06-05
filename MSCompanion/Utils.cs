using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    public static class Utils
    {
        private const string TemporaryFileName = "temp.txt";


        /// <summary>
        /// Gets the information required to restore the properties file
        /// </summary>
        /// <param name="pMachinimaPropertiesPath">Properties file</param>
        /// <returns>Restoration info</returns>
        public static Dictionary<string, string> GetRestoreInfo(string pMachinimaPropertiesPath)
        {
            Dictionary<string, string> addons = new Dictionary<string, string>();

            string line;
            using (StreamReader reader = new StreamReader(pMachinimaPropertiesPath, true))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (Globals.AddonStatusRegex.IsMatch(line))
                    {
                        string[] splitStrings = line.Split(new char[] { '.', '=' },
                            StringSplitOptions.RemoveEmptyEntries);
                        addons.Add(
                                splitStrings[1] + "." + splitStrings[2],
                                splitStrings[4]
                            );
                    }
                }
            }
            return addons;
        }




        /// <summary>
        /// Sets the list of enabled addons according to those used in a movie
        /// </summary>
        /// <param name="pMachinimaPropertiesPath">Properties file</param>
        /// <param name="pMovieAddonList">List of addons for the movie</param>
        public static void SetAddonEnableStatus(string pMachinimaPropertiesPath, MovieAddonList pMovieAddonList)
        {
            string line;

            Globals.LogIt("Setting the list of enabled addons");

            List<string> addonList = new List<string>();
            foreach (AddOnBasicInfo item in pMovieAddonList.AddonList)
                addonList.Add(item.QualifiedName);

            // Write the temporary new file
            using (StreamReader reader = new StreamReader(pMachinimaPropertiesPath, true))
            {
                // Encoding oldEncoding = reader.CurrentEncoding;
                using (StreamWriter writer = new StreamWriter(TemporaryFileName, false, Encoding.GetEncoding(Globals.EncodingPage)))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Globals.AddonStatusRegex.IsMatch(line))
                        {
                            string[] splitStrings = line.Split(new char[] { '.', '=' },
                                StringSplitOptions.RemoveEmptyEntries);

                            bool value = (addonList.Contains(splitStrings[1] + "." + splitStrings[2]));
                            line = string.Format("addon.{0}.{1}.enabled={2}",
                                    splitStrings[1], splitStrings[2],
                                    value ? "true" : "false"
                                    );
                        }
                        writer.WriteLine(line);
                    }
                    writer.Close();
                }
                reader.Close();
            }

            string backupFile = string.Format("machinimascope.properties-{0}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Globals.LogIt("  Saving properties file: " + backupFile);
            File.Copy(pMachinimaPropertiesPath, backupFile);
            try
            {
                Globals.LogIt("Updating properties file");
                File.Delete(pMachinimaPropertiesPath);
                File.Move(TemporaryFileName, pMachinimaPropertiesPath);
            }
            catch (Exception exception)
            {
                string errorText = "Error: " + exception.Message;
                Globals.LogIt(errorText);
                MessageBox.Show(errorText);

            }
           
        }



        /// <summary>
        /// Restore addons enable status
        /// </summary>
        /// <param name="pMachinimaPropertiesPath">Properties file</param>
        /// <param name="pRestoreInfo">Restore information</param>
        public static void RestoreProperties(string pMachinimaPropertiesPath, Dictionary<string, string> pRestoreInfo)
        {

            string line;

            Globals.LogIt("Restoring the list of enabled addons");

            // Write the temporary new file
            using (StreamReader reader = new StreamReader(pMachinimaPropertiesPath, true))
            {
                // Encoding oldEncoding = reader.CurrentEncoding;
                using (StreamWriter writer = new StreamWriter(TemporaryFileName, false, Encoding.GetEncoding(Globals.EncodingPage)))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Globals.AddonStatusRegex.IsMatch(line))
                        {
                            string[] splitStrings = line.Split(new char[] { '.', '=' },
                                StringSplitOptions.RemoveEmptyEntries);

                            string oldValue;
                            if (!pRestoreInfo.TryGetValue(splitStrings[1] + "." + splitStrings[2], out oldValue))
                                oldValue = "true";

                            line = string.Format("addon.{0}.{1}.enabled={2}",
                                    splitStrings[1], splitStrings[2],
                                    oldValue
                                    );
                        }
                        writer.WriteLine(line);
                    }
                    writer.Close();
                }
                reader.Close();
            }

            try
            {
                Globals.LogIt("Updating properties file");
                File.Delete(pMachinimaPropertiesPath);
                File.Move(TemporaryFileName, pMachinimaPropertiesPath);
            }
            catch (Exception exception)
            {
                string errorText = "Error: " + exception.Message;
                Globals.LogIt(errorText);
                MessageBox.Show(errorText);
            }

        }


        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determina si un nombre de fichero se ajusta a una máscar de fichero
        /// </summary>
        /// <param name="fileName">Nombre del fichero</param>
        /// <param name="fileMask">Máscara de fichero</param>
        /// <returns>Indicador de ajuste a máscara</returns>
        public static bool MatchesFileMask(string fileName, string fileMask)
        {
            // const string specialChars = ".[]()^$+=!{,}";
            const string specialChars = ".()^$+=!{,}";
            bool matchResult = false;
            try
            {
                StringBuilder fileMaskConverted = new StringBuilder(fileMask);
                foreach (char specialChar in specialChars)
                {
                    fileMaskConverted.Replace(specialChar.ToString(), String.Format(@"\{0}", specialChar));
                }
                fileMaskConverted.Replace("*", ".*").Replace("?", ".");

                Regex mask = new Regex(String.Format("^{0}$", fileMaskConverted.ToString()),
                    RegexOptions.IgnoreCase);
                matchResult = mask.IsMatch(fileName);
            }
            catch
            {
                matchResult = false;
            }
            return matchResult;
        }

    }
}
