/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.IO;

using HergBot.Logging.Helpers;

namespace HergBot.Logging
{
    /// <summary>
    /// A general purpose logging class that outputs log entries to a file
    /// </summary>
    public class FileLogger : BaseLogger, ILogger
    {
        /// <summary>
        /// The log file locking object for thread safe logging
        /// </summary>
        private static object _logFileLock = new object();

        /// <summary>
        /// The full file path to log to
        /// </summary>
        private LogFilePath _logFilePath;

        /// <summary>
        /// The log message generator
        /// </summary>
        private ILogMessageGenerator _messageGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configFile">The path to the logging configuration file</param>
        /// <param name="generator">The generator to use for log messages</param>
        /// <param name="fileExtension">The extension for the log file</param>
        public FileLogger(string configFile, ILogMessageGenerator generator, string fileExtension) : base(configFile)
        {
            _messageGenerator = generator;
            _logFilePath = new LogFilePath(Configuration.LogDirectory, Configuration.BaseFileName, fileExtension);
        }

        /// <summary>
        /// Logs a Debug type message if its enabled in the configuration
        /// </summary>
        /// <param name="debugMessage">The debug message to log</param>
        /// <param name="methodName">The method name logging the message</param>
        public void LogDebug(string debugMessage, string methodName = "")
        {
            if (Configuration.IsDebugEnabled)
            {
                LogMessage(Configuration.DebugLabel, debugMessage, methodName);
            }
        }

        /// <summary>
        /// Logs a Error type message if its enabled in the configuration
        /// </summary>
        /// <param name="errorMessage">The error message to log</param>
        /// <param name="methodName">The method name logging the message</param>
        public void LogError(string errorMessage, string methodName = "")
        {
            if (Configuration.IsErrorEnabled)
            {
                LogMessage(Configuration.ErrorLabel, errorMessage, methodName);
            }
        }

        /// <summary>
        /// Logs an Exception if its enabled in the configuration
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="methodName">The method name logging the message</param>
        public void LogException(Exception exception, string methodName = "")
        {
            if (Configuration.IsExceptionEnabled)
            {
                LogMessage(Configuration.ExceptionLabel, exception.Message, methodName);
                LogMessage(Configuration.ExceptionLabel, exception.StackTrace, methodName);
            }
        }

        /// <summary>
        /// Logs an Info type message if its enabled in the configuration
        /// </summary>
        /// <param name="infoMessage">The info message to log</param>
        /// <param name="methodName">The method name logging the message</param>
        public void LogInfo(string infoMessage, string methodName = "")
        {
            if (Configuration.IsInfoEnabled)
            {
                LogMessage(Configuration.InfoLabel, infoMessage, methodName);
            }
        }

        /// <summary>
        /// Logs a Warning type message if its enabled in the configuration
        /// </summary>
        /// <param name="warningMessage">The warning message to log</param>
        /// <param name="methodName">The method name logging the message</param>
        public void LogWarning(string warningMessage, string methodName = "")
        {
            if (Configuration.IsWarningEnabled)
            {
                LogMessage(Configuration.InfoLabel, warningMessage, methodName);
            }
        }

        /// <summary>
        /// Checks if the file name needs to be updated and logs the message
        /// </summary>
        /// <param name="type">The type of log message</param>
        /// <param name="message">The message to log</param>
        /// <param name="methodName">The method that called</param>
        private void LogMessage(string type, string message, string methodName)
        {
            _logFilePath.CheckDate();
            WriteToFile(_messageGenerator.GenerateLogMessage(Timestamp, ThreadName, methodName, type, message));
        }

        /// <summary>
        /// Writes a string to the log file
        /// </summary>
        /// <param name="message">The full log message to write</param>
        private void WriteToFile(string message)
        {
            lock (_logFileLock)
            {
                using (StreamWriter writer = File.AppendText(_logFilePath.FullFilePath))
                {
                    writer.WriteLine(message);
                }
            }
        }
    }
}
