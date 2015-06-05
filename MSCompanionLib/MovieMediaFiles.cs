using System;
using System.Collections.Generic;
using System.IO;

namespace jamoram62.tools.MSCompanion
{
    public class MovieMediaFile
    {
        private string _fullRelativePath = "";
        public string FullRelativeName { get { return _fullRelativePath; } }

        private string _fileName = "";
        public string FileName { get { return _fileName; } }

        private bool _isUsed = false;
        public bool IsUsed { get { return _isUsed; } }



        public MovieMediaFile(string pMoviePath, string pFilePath, string pMScope)
        {
            if(!pFilePath.ToLower().StartsWith(pMoviePath.ToLower()))
                throw new Exception("File is not inside the movie path");
            _fullRelativePath = pFilePath.Substring(pMoviePath.Length + 1);
            _fileName = Path.GetFileName(_fullRelativePath);

            string searchString = (_fullRelativePath.ToLower().StartsWith(@"dialogue\"))
                ? "<audioResource>" + _fileName + "</audioResource>"
                : ">" + _fullRelativePath.Replace('\\', '/') + "<";

            _isUsed = pMScope.Contains(searchString);
        }
    }



    public class MovieMediaFileManifest
    {
        private List<MovieMediaFile> _fileList = new List<MovieMediaFile>();
        public List<MovieMediaFile> FileList { get { return _fileList; } }

        public string MovieName { get { return _movieName; }}
        private string _movieName = "";

        private string _moviePath = "";

        public MovieMediaFileManifest(string pMoviePath)
        {

            string movieMScopeFile = Path.Combine(pMoviePath, Globals.MovieMScope);
            if (!File.Exists(movieMScopeFile))
                return;

            _moviePath = pMoviePath;
            _movieName = Path.GetFileName(pMoviePath);

            string movieMScope = File.ReadAllText(movieMScopeFile);


            // Scans media file folders
            string path = "";
            
            if (Directory.Exists(path = pMoviePath + @"\Images"))
            {
                foreach (string fileName in Directory.GetFiles(path))
                    _fileList.Add(new MovieMediaFile(pMoviePath, fileName, movieMScope));
            }

            if (Directory.Exists(path = pMoviePath + @"\Dialogue"))
            {
                foreach (string fileName in Directory.GetFiles(path))
                    _fileList.Add(new MovieMediaFile(pMoviePath, fileName, movieMScope));
            }

            if (Directory.Exists(path = pMoviePath + @"\ClipBin\Audio"))
            {
                foreach (string fileName in Directory.GetFiles(path))
                    _fileList.Add(new MovieMediaFile(pMoviePath, fileName, movieMScope));
            }

            if (Directory.Exists(path = pMoviePath + @"\ClipBin\Images"))
            {
                foreach (string fileName in Directory.GetFiles(path))
                    _fileList.Add(new MovieMediaFile(pMoviePath, fileName, movieMScope));
            }

            if (Directory.Exists(path = pMoviePath + @"\ClipBin\Video"))
            {
                foreach (string fileName in Directory.GetFiles(path))
                    _fileList.Add(new MovieMediaFile(pMoviePath, fileName, movieMScope));
            }
        }


        public void DeleteUnused()
        {
            foreach (MovieMediaFile item in _fileList)
            {
                if (!item.IsUsed)
                {
                    string fullPath = _moviePath + "\\" + item.FullRelativeName;
                    File.Delete(fullPath);
                }
            }
        }

    }
}
