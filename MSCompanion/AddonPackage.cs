using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace jamoram62.tools.MSCompanion
{
    public class AddonPackage
    {
        private const string ManifestFilename = "ASSET_DATA.MF";

        public AddonPackage(string pAddonPath, string pTemporaryFolder)
        {

            string assetDataFile = Path.Combine(pAddonPath, "assetData.jar");
            if (!File.Exists(assetDataFile))
                throw new Exception("No assetData.jar file");

            string manifestFile = Path.Combine(pTemporaryFolder, ManifestFilename);
            if (File.Exists(manifestFile))
                File.Delete(manifestFile);

            List<string> filesToExtractList = new List<string> { ManifestFilename };
            if(Compress.ZipArchiveExtract(assetDataFile, pTemporaryFolder, filesToExtractList) <= 0)
                throw new Exception("Manifest file extraction failed");
            if(!File.Exists(manifestFile)) 
                throw new Exception("Manifest file not found");

            // TODO : analizar fichero de manifiesto
        }

    }
}
