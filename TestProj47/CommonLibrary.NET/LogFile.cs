#if NetFX
using System;
using System.IO;

namespace HSNXT.ComLib.Logging
{
    /// <summary>
    /// File based logger.
    /// </summary>
    public class LogFile : LogBase, ILog, IDisposable
    {
        private readonly string _filepath;
        private string _filepathUnique;
        private StreamWriter _writer;
        private int _iterativeFlushCount;
        private readonly int _iterativeFlushPeriod = 4;
        private readonly object _lockerFlush = new object();
        private readonly int _maxFileSizeInMegs = 10;
        private readonly bool _rollFile = true;
        

        /// <summary>
        /// Initialize with path of the log file.
        /// </summary>
        /// <param name="name">Name of application.</param>
        /// <param name="filepath">File path, can contain substitutions.</param>
        public LogFile(string name, string filepath) : 
            this(name, filepath, DateTime.Now, "")
        {           
        }


        /// <summary>
        /// Initialize log file.
        /// </summary>
        /// <param name="name">Name of application.</param>
        /// <param name="filepath">File path, can contain substitutions. e.g. "%yyyy%.</param>
        /// <param name="date">Date to use in the name of the log file.</param>
        /// <param name="env">Environment name to put into the name of the log file.</param>
        public LogFile(string name, string filepath, DateTime date, string env)
            : this(name, filepath, date, env, true, 20)
        {
        }


        /// <summary>
        /// Initialize log file.
        /// </summary>
        /// <param name="name">Name of application.</param>
        /// <param name="filepath">File path, can contain substitutions. e.g. "%yyyy%.</param>
        /// <param name="date">Date to use in the name of the log file.</param>
        /// <param name="env">Environment name to put into the name of the log file.</param>
        /// <param name="rollFile">True to roll file once max size is reached.</param>
        /// <param name="maxSizeInMegs">Maximum size in megabytes.</param>
        public LogFile(string name, string filepath, DateTime date, string env, bool rollFile, int maxSizeInMegs)
            : base(name)
        {
            _rollFile = rollFile;
            _maxFileSizeInMegs = maxSizeInMegs;
            _filepath = LogHelper.BuildLogFileName(filepath, name, date, env);
            _filepathUnique = _filepath;
            _writer = new StreamWriter(_filepathUnique, true);
        }


        /// <summary>
        /// The full path to the log file.
        /// </summary>
        public string FilePath => _filepathUnique;


        /// <summary>
        /// Log the event to file.
        /// </summary>
        /// <param name="logEvent">Event to log.</param>
        public override void Log(LogEvent logEvent)
        {
            var finalMessage = logEvent.FinalMessage;
            if(string.IsNullOrEmpty(finalMessage))
                finalMessage = BuildMessage(logEvent);

            _writer.WriteLine(finalMessage);
            FlushCheck();
        }        


        /// <summary>
        /// Flush the output.
        /// </summary>
        public override void Flush()
        {
            _writer.Flush();
        }


        /// <summary>
        /// Shutsdown the logger.
        /// </summary>
        public override void ShutDown()
        {
            Dispose();
        }


        #region IDisposable Members
        /// <summary>
        /// Flushes the data to the file.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_writer != null)
                {                    
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion


        /// <summary>
        /// Destructor to close the writer
        /// </summary>
        ~LogFile()
        {
            Dispose();
        }


        /// <summary>
        /// Flush the data and check file size for rolling.
        /// </summary>
        private void FlushCheck()
        {
            lock (_lockerFlush)
            {
                // This is to flush the writer periodically after every N number of times.
                if (_iterativeFlushCount % _iterativeFlushPeriod == 0)
                {
                    _writer.Flush();
                    _iterativeFlushCount = 1;
                }
                else
                    _iterativeFlushCount++;

                // This rolls the log file. e.g. Creates a new log file if the current one
                // exceeds a logfile size.
                if (_rollFile && FileHelper.GetSizeInMegs(_filepath) > _maxFileSizeInMegs)
                {
                    Try.Catch(() =>
                    {
                        var searchPath = _filepath.Substring(0, _filepath.LastIndexOf('.'));
                        var file = new FileInfo(_filepath);
                        var files = Directory.GetFiles(file.DirectoryName, searchPath + "*", SearchOption.TopDirectoryOnly);

                        _filepathUnique = $"{searchPath}-part{files.Length}{file.Extension}";
                        _writer.Flush();
                        _writer.Close();
                        _writer = new StreamWriter(_filepathUnique);
                    });
                }
            }
        }
    }
}
#endif