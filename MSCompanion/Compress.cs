using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Core;
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

        /// <summary>
        /// Crea un archivo ZIP zipFilename con una lista de ficheros
        /// </summary>
        /// <param name="zipFilename">Nombre del archivo ZIP generado</param>
        /// <param name="listOfFiles">Lista de ficheros que se incluirán en el archivo</param>
        /// <param name="compressionLevel">Nivel de compresión (0-9)</param>
        /// <param name="zipPassword">Contraseña para encriptación de los ficheros archivados (opcional)</param>
        /// <returns>Estado (true=OK)</returns>
        public static bool ZipArchiveCreate(string zipFilename, List<string> listOfFiles, int compressionLevel = DefaultCompressionLevel, string zipPassword = "")
        {
            bool procOk = false;

            _lastErrorText = "";
            if (compressionLevel < 0)
                compressionLevel = DefaultCompressionLevel;

            try
            {
                if (File.Exists(zipFilename))
                    File.Delete(zipFilename);
            }
            catch { }

            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFilename)))
                {
                    zipStream.SetLevel(compressionLevel);
                    if (zipPassword != "")
                        zipStream.Password = zipPassword;

                    byte[] buffer = new byte[4096];

                    foreach (string file in listOfFiles)
                    {
                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        entry.DateTime = DateTime.Now;
                        zipStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                zipStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                            fs.Close();
                        }


                    }
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
                                    if (Utils.MatchesFileMask(entry.Name, regexpStr))
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
        /// Comprime una cadena de texto en memoria. 
        /// </summary>
        /// <param name="str">Cadena a comprimir</param>
        /// <returns>Vector de bytes resultante de la compresión de la cadena</returns>
        public static byte[] ZipTextString(string str)
        {
#if _NEVERDEFINED_
            var bytes = Encoding.UTF8.GetBytes(str);

            
            using (var mso = new MemoryStream())
            {
                /*
                
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    _copyStreamToStream(msi, gs);
                }
                */
                using (var outputZipStream = new ZipOutputStream(mso))
                {
                    byte[] buffer = new byte[4096];
                    using (var msi = new MemoryStream(bytes))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = msi.Read(buffer, 0, buffer.Length);
                            outputZipStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                        msi.Close();
                    }
                }

                return mso.ToArray();
            }
#else
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    _copyStreamToStream(msi, gs);
                }

                return mso.ToArray();
            }
#endif
        }

        /// <summary>
        /// Descomprimir una cadena de texto
        /// </summary>
        /// <param name="bytes">Vector de bytes de entrada (cadena comprimida)</param>
        /// <returns>Cadena descomprimida</returns>
        public static string UnzipTextString(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    _copyStreamToStream(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        /// <summary>
        /// Método utilizado internamente, para la copia entre streams
        /// </summary>
        /// <param name="src">Stream de origen</param>
        /// <param name="dest">Stream de destino</param>
        private static void _copyStreamToStream(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

    }
}
