using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jamoram62.tools.MSCompanion
{
    public class Movie
    {



        public static bool CreateArchive(string pMoviePath, string pMovieArchivePath, out string pErrorText)
        {
            pErrorText = "";

            bool compressResult = Compress.ZipArchiveFolder(pMovieArchivePath, pMoviePath, false, 9);
            if (!compressResult)
                pErrorText = Compress.lastErrorText;

            return compressResult;
        }

    }
}
