/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 04/08/2017
*/

using System;
using System.IO;

namespace HergBotLogging
{
    /// <summary>
    /// A general purpose logging class that outputs log entries to a text file.
    /// </summary>
    public class FileLogger : BaseLogger, ILogger
    {
        /// <summary>
        /// The log file locking object for thread safe logging
        /// </summary>
        private static object _logFileLock = new object();

        private const string DEFAULT_FILE_EXTENSION = ".log";

        private DateTime _currentLogFileDate = DateTime.Now.Date;

        private string _logFileExtension = ".log";

        private string _fullLogFilePath = null;

        private ILogMessageGenerator _messageGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileLogger(string configFile, ILogMessageGenerator generator, string fileExtension) : base(configFile)
        {
            _messageGenerator = generator;
            _logFileExtension = fileExtension;
            CreateFilePath();
        }

        public void LogDebug(string debugMessage, string methodName = "")
        {
            if (Configuration.IsDebugEnabled)
            {
                LogMessage(Configuration.DebugLabel, debugMessage, methodName);
            }
        }

        public void LogError(string errorMessage, string methodName = "")
        {
            if (Configuration.IsErrorEnabled)
            {
                LogMessage(Configuration.ErrorLabel, errorMessage, methodName);
            }
        }

        public void LogException(Exception exception, string methodName = "")
        {
            if (Configuration.IsExceptionEnabled)
            {
                LogMessage(Configuration.ExceptionLabel, exception.Message, methodName);
                LogMessage(Configuration.ExceptionLabel, exception.StackTrace, methodName);
            }
        }

        public void LogInfo(string infoMessage, string methodName = "")
        {
            if (Configuration.IsInfoEnabled)
            {
                LogMessage(Configuration.InfoLabel, infoMessage, methodName);
            }
        }

        public void LogWarning(string warningMessage, string methodName = "")
        {
            if (Configuration.IsWarningEnabled)
            {
                LogMessage(Configuration.InfoLabel, warningMessage, methodName);
            }
        }

        /// <summary>
        /// Logs a full message to the file with a date timestamp, thread name, and type
        /// </summary>
        /// <param name="type">The type of log message</param>
        /// <param name="message">The message to log</param>
        /// /// <param name="methodName">The method that called</param>
        private void LogMessage(string type, string message, string methodName)
        {
            WriteToFile(_messageGenerator.GenerateLogMessage(Timestamp, ThreadName, methodName, type, message));
        }

        private void WriteToFile(string message)
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
            _fullLogFilePath = $"{Configuration.LogDirectory}{Configuration.BaseFileName}_{_currentLogFileDate.ToString(DATE_FORMAT)}.{_logFileExtension}";
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
