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

        protected bool IsDebugEnabled { get { return _configuration.IsTypeEnabled(LoggingType.DEBUG_KEY); } }

        protected string DebugLabel { get { return _configuration.GetTypeLabel(LoggingType.DEBUG_KEY); } }

        protected bool IsErrorEnabled { get { return _configuration.IsTypeEnabled(LoggingType.ERROR_KEY); } }

        protected string ErrorLabel { get { return _configuration.GetTypeLabel(LoggingType.ERROR_KEY); } }

        protected bool IsExceptionEnabled { get { return _configuration.IsTypeEnabled(LoggingType.EXCEPTION_KEY); } }

        protected string ExceptionLabel { get { return _configuration.GetTypeLabel(LoggingType.EXCEPTION_KEY); } }

        protected bool IsInfoEnabled { get { return _configuration.IsTypeEnabled(LoggingType.INFO_KEY); } }

        protected string InfoLabel { get { return _configuration.GetTypeLabel(LoggingType.INFO_KEY); } }

        protected bool IsWarningEnabled { get { return _configuration.IsTypeEnabled(LoggingType.WARNING_KEY); } }

        protected string WarningLabel { get { return _configuration.GetTypeLabel(LoggingType.WARNING_KEY); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseLogger(string configFilePath, string fileExtension)
        {
            _logFileExtension = fileExtension;
            _configuration = new LoggingConfiguration();
            
            if (configFilePath != null)
            {
                _configuration = LoggingConfiguration.LoadFromFile(configFilePath);
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
