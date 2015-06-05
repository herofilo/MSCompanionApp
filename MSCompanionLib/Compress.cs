using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;

namespace jamoram62.tools.MSCompanion
{
    /// <summary>
    /// Clase con métodos estáticos relacionados con la gestión de archivos comprimidos.
    /// <remarks>
    /// 1. Está basado en la librería ICSharCode
    /// 2. Actualemente sólo soporta métodos relacionados con la manipulación de archivos ZIP</remarks>
    /// </summary>
    public static class Compress
    {
        /// <summary>
        /// Versión de código de la clase
        /// </summary>
        public static readonly string _SourceVersion_ = "20140922.01";


        /// <summary>
        /// Texto del último error ocurrido en alguna operación
        /// </summary>
        private static string _lastErrorText = "";

        /// <summary>
        /// Propiedad para acceder públicamente al contenido de <see cref="_lastErrorText"/>
        /// </summary>
        public static string lastErrorText { get { return _lastErrorText; } }

        public const int DefaultCompressionLevel = 5;

        // ......................................................................................................

        /// <summary>
        /// Crea un archivo ZIP pZipFilename con una lista de ficheros
        /// </summary>
        /// <param name="pZipFilename">Nombre del archivo ZIP generado</param>
        /// <param name="listOfFiles">Lista de ficheros que se incluirán en el archivo</param>
        /// <param name="pCompressionLevel">Nivel de compresión (0-9)</param>
        /// <param name="pZipPassword">Contraseña para encriptación de los ficheros archivados (opcional)</param>
        /// <returns>Estado (true=OK)</returns>
        public static bool ZipArchiveFolder(string pZipFilename, string pFolderName, bool pRootFolder, int pCompressionLevel = DefaultCompressionLevel, string pZipPassword = "")
        {
            if (!Directory.Exists(pFolderName))
            {
                _lastErrorText = "Folder not found";
                return false;
            }

            bool procOk = false;

            _lastErrorText = "";
            if (pCompressionLevel < 0)
                pCompressionLevel = DefaultCompressionLevel;

            try
            {
                if (File.Exists(pZipFilename))
                    File.Delete(pZipFilename);
            }
            catch { }

            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(pZipFilename)))
                {
                    zipStream.SetLevel(pCompressionLevel);
                    if (pZipPassword != "")
                        zipStream.Password = pZipPassword;

                    string rootFolder = pFolderName;
                    if (!pRootFolder)
                    {
                        string tempRootFolder = Path.GetDirectoryName(pFolderName);
                        if (!string.IsNullOrEmpty(tempRootFolder))
                            rootFolder = tempRootFolder;
                    }

                    _ZipFolder(rootFolder, pFolderName, zipStream);

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    zipStream.Finish();
                    // Close is important to wrap things up and unlock the file.
                    zipStream.Close();
                }
                procOk = true;
                _lastErrorText = "OK";
            }
            catch (Exception exception)
            {
                _lastErrorText = "ZipArchiveCreate ERROR: " + exception.Message;
                procOk = false;
            }

            return procOk;
        }


        private static void _ZipFolder(string RootFolder, string CurrentFolder,
            ZipOutputStream zStream)
        {
            string[] SubFolders = Directory.GetDirectories(CurrentFolder);

            //calls the method recursively for each subfolder
            foreach (string Folder in SubFolders)
            {
                _ZipFolder(RootFolder, Folder, zStream);
            }

            string relativePath = CurrentFolder.Substring(RootFolder.Length) + "/";

            //the path "/" is not added or a folder will be created
            //at the root of the file
            if (relativePath.Length > 1)
            {
                ZipEntry dirEntry;
                dirEntry = new ZipEntry(relativePath);
                dirEntry.DateTime = DateTime.Now;
            }

            //adds all the files in the folder to the zip
            foreach (string file in Directory.GetFiles(CurrentFolder))
            {
                _AddFileToZip(zStream, relativePath, file);
            }
        }

        private static void _AddFileToZip(ZipOutputStream zStream, string relativePath, string file)
        {
            byte[] buffer = new byte[4096];

            //the relative path is added to the file in order to place the file within
            //this directory in the zip
            string fileRelativePath = (relativePath.Length > 1 ? relativePath : string.Empty)
                                      + Path.GetFileName(file);

            ZipEntry entry = new ZipEntry(fileRelativePath);
            entry.DateTime = DateTime.Now;
            zStream.PutNextEntry(entry);

            using (FileStream fs = File.OpenRead(file))
            {
                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    zStream.Write(buffer, 0, sourceBytes);
                } while (sourceBytes > 0);
            }
        }

        // ................................................................................................

        /// <summary>
        /// Extrae ficheros de un archivo ZIP
        /// </summary>
        /// <param name="zipFilename">Nombre del archivo zip</param>
        /// <param name="destinationPath">directorio de destino de los ficheros extraídos</param>
        /// <param name="listOfFiles">Lista con especificaciones de ficheros a extraer. Admite máscara de ficheros</param>
        /// <param name="zipPassword">Contraseña</param>
        /// <remarks>Actualmente, no funciona el filtro por lista de nombre de ficheros</remarks>
        /// <returns>Número de ficheros extraídos. -1 si error</returns>
        public static int ZipArchiveExtract(string zipFilename, string destinationPath, List<string> listOfFiles, string zipPassword = "")
        {
            int entriesCount = 0;
            _lastErrorText = "";

            if((destinationPath = destinationPath.Trim()) == "")
                destinationPath = ".";

            if (!File.Exists(zipFilename))
            {
                _lastErrorText = "Archivo '" + zipFilename + "' no existe";
                return -1;
            }

            try
            {
                ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilename));
                if (zipPassword != "")
                    zipStream.Password = zipPassword;

                ZipEntry entry;
                const int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];

                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(destinationPath + "\\" + entry.Name);
                        continue;
                    }
                    if (entry.IsFile)
                    {
                        if (listOfFiles != null)
                        {
                            bool foundMatch = false;
                            foreach (string regexpStr in listOfFiles)
                            {
                                try
                                {
                                    if (MatchesFileMask(entry.Name, regexpStr))
                                    {
                                        foundMatch = true;
                                        break;
                                    }
                                }
                                catch { }
                            }
                            if (!foundMatch)
                                continue;
                        }
                        entriesCount++;
                        using (FileStream fileStream = File.OpenWrite(destinationPath + "\\" + entry.Name))
                        {
                            int bytesRead;
                            for (; ; )
                            {
                                bytesRead = zipStream.Read(buffer, 0, buffer.Length);
                                if (bytesRead == 0)
                                    break;
                                fileStream.Write(buffer, 0, bytesRead);
                            }
                            fileStream.Close();
                        }
                        File.SetLastWriteTime(destinationPath + "\\" + entry.Name, entry.DateTime);
                    }

                }
                zipStream.Close();
                zipStream.Dispose();
            }
            catch (Exception exception)
            {
                _lastErrorText = "ZipArchiveList ERROR: " + exception.Message;
                entriesCount = -1;
            }

            return entriesCount;
        }



        /// <summary>
        /// Retorna el contenido de un archivo Zip
        /// </summary>
        /// <param name="zipFilename">Nombre del archivo</param>
        /// <param name="listEntries">Lista de entradas</param>
        /// <param name="zipPassword">Contraseña para desencriptación del archivo</param>
        /// <returns>Número de entradas en el archivo (-1=error)</returns>
        public static int ZipArchiveList(string zipFilename, ref List<ZipEntry> listEntries, string zipPassword = "")
        {
            int entriesCount = 0;
            _lastErrorText = "";
            try
            {
                ZipFile zipFile = new ZipFile(zipFilename);

                if (zipPassword != "")
                    zipFile.Password = zipPassword;

                listEntries.Clear();

                foreach (ZipEntry entry in zipFile)
                {
                    listEntries.Add(entry);
                    entriesCount++;
                }
                zipFile.Close();
            }
            catch (Exception exception)
            {
                _lastErrorText = "ZipArchiveList ERROR: " + exception.Message;
                entriesCount = -1;
            }

            return entriesCount;
        }

        // -------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determina si un nombre de fichero se ajusta a una máscar de fichero
        /// </summary>
        /// <param name="fileName">Nombre del fichero</param>
        /// <param name="fileMask">Máscara de fichero</param>
        /// <returns>Indicador de ajuste a máscara</returns>
        private static bool MatchesFileMask(string fileName, string fileMask)
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
