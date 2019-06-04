/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 04/08/2017
*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace HergBotLogging
{
    /// <summary>
    /// A base class that reads and handles the logging configuration
    /// </summary>
    public abstract class BaseLogger
    {
        /// <summary>
        /// The log file locking object for thread safe logging
        /// </summary>
        private static object _logFileLock = new object();

        private DateTime _currentLogFileDate = DateTime.Now.Date;

        private string _logFileExtension = ".log";

        private string _fullLogFilePath = null;

        private LoggingConfiguration _configuration;

        /// <summary>
        /// The format string for just the date
        /// </summary>
        protected const string DATE_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// The format string for a date with time
        /// </summary>
        protected const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Empty method name constant for the parameter default
        /// </summary>
        protected const string EMPTY_METHOD_NAME = "";

        protected string Timestamp { get { return DateTime.Now.ToString(DATE_TIME_FORMAT); } }

        protected string ThreadName { get { return Thread.CurrentThread.Name;  } }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseLogger(string configFilePath, string fileExtension)
        {
            _logFileExtension = fileExtension;
            _configuration = new LoggingConfiguration();
            
            if (configFilePath != null)
            {
                _configuration.LoadConfiguration(configFilePath);
            }

            CreateFilePath();
        }

        protected void WriteToFile(string message)
        {
            lock (_logFileLock)
            {
                using (StreamWriter writer = File.AppendText(_fullLogFilePath))
                {
                    writer.WriteLine(message);
                }
            }
        }

        private void CreateFilePath()
        {
            _fullLogFilePath = $"{_configuration.LogDirectory}{_configuration.BaseFileName}_{_currentLogFileDate.ToString(DATE_FORMAT)}.{_logFileExtension}";
        }

        private void CheckLogFileDate()
        {
            if (_currentLogFileDate < DateTime.Now.Date)
            {
                CreateFilePath();
            }
        }
    }
}
