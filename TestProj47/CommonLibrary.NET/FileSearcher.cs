using System;
using System.IO;

namespace HSNXT.ComLib.IO
{

    /// <summary>
    /// Search for directories and files using a pattern.
    /// </summary>
    public class FileSearcher
    {
        private readonly Action<FileInfo> _fileHandler;
        private readonly Action<DirectoryInfo> _directoryHandler = null;
        private string _directoryPattern = "**";
        private string _filePattern = "**";
        private readonly string _pattern = "**/**";
        private readonly bool _handleFiles = true;


        /// <summary>
        /// Initialize with file handler
        /// </summary>
        /// <param name="directoryHandler">Handler for each directory.</param>
        /// <param name="fileHandler">Handler for each file.</param>
        /// <param name="handleFiles">Flag indicating to handle files.</param>
        /// <param name="pattern">Search pattern for directories files.
        /// e.g. **/**.</param>
        public FileSearcher(Action<FileInfo> fileHandler, Action<DirectoryInfo> directoryHandler, 
            string pattern, bool handleFiles)
        {
            _pattern = pattern;
            _fileHandler = fileHandler;
            _handleFiles = handleFiles;
            Init();
        }


        /// <summary>
        /// Initialize directory and file pattern.
        /// </summary>
        public void Init()
        {
            // Determine directory pattern and file pattern.
            if (!string.IsNullOrEmpty(_pattern))
            {
                var ndxSlash = _pattern.IndexOf("/");
                if (ndxSlash < 0)
                    ndxSlash = _pattern.IndexOf("\\");

                _directoryPattern = _pattern.Substring(0, ndxSlash);
                _filePattern = _pattern.Substring(ndxSlash + 1);
            }
        }

        
        /// <summary>
        /// Search directory for directories/files using pattern.
        /// </summary>
        /// <param name="startDir"></param>
        public void Search(DirectoryInfo startDir)
        {
            DirectoryInfo[] directories = null;
            if (_directoryPattern == "**")
                directories = startDir.GetDirectories();
            else
                directories = startDir.GetDirectories(_directoryPattern);

            if ((directories != null) && (directories.Length > 0))
            {
                foreach (var currentDir in directories)
                {
                    if (_handleFiles)
                    {
                        SearchFiles(currentDir);
                    }
                    Search(currentDir);

                    // Call the directory handler.
                    if (_directoryHandler != null)
                        _directoryHandler(currentDir);
                }
            }
        }


        /// <summary>
        /// Search all the files.
        /// </summary>
        /// <param name="directory"></param>
        private void SearchFiles(DirectoryInfo directory)
        {
            var files = directory.GetFiles(_filePattern);
            if (files == null || files.Length == 0)
                return;

            foreach (var file in files)
            {
                if (_fileHandler != null)
                {
                    _fileHandler(file);
                }
            }
        }
    }
}
