/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 04/08/2017
*/

using System;

namespace HergBotLogging
{
    /// <summary>
    /// A general purpose logging class that outputs log entries to a text file.
    /// </summary>
    public class TextLogger : BaseLogger, ILogger
    {
        /// <summary>
        /// The log file locking object for thread safe logging
        /// </summary>
        private static object _logFileLock = new object();

        private DateTime _currentLogFileDate = DateTime.Now.Date;

        private string _logFileExtension = ".log";

        private string _fullLogFilePath = null;

        private const string TEXT_LOGGER_EXTENSION = ".log";
        /// <summary>
        /// Constructor
        /// </summary>
        public TextLogger(string configFile) : base(configFile, TEXT_LOGGER_EXTENSION)
        {
            
        }

        /// <summary>
        /// Logs a debug type entry
        /// </summary>
        /// <param name="debugMessage">The message to log</param>
        /// /// <param name="methodName">The method that called</param>
        public void LogDebug(string debugMessage, string methodName = EMPTY_METHOD_NAME)
        {
            if (IsDebugEnabled)
            {
                LogMessage(DebugLabel, debugMessage, methodName);
            }
        }

        /// <summary>
        /// Logs an error type message
        /// </summary>
        /// <param name="errorMessage">The message to log</param>
        /// /// <param name="methodName">The method that called</param>
        public void LogError(string errorMessage, string methodName = EMPTY_METHOD_NAME)
        {
            if (IsErrorEnabled)
            {
                LogMessage(ErrorLabel, errorMessage, methodName);
            }
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// /// <param name="methodName">The method that called</param>
        public void LogException(Exception exception, string methodName = EMPTY_METHOD_NAME)
        {
            if (IsExceptionEnabled)
            {
                LogMessage(ExceptionLabel, exception.Message, methodName);
                LogMessage(ExceptionLabel, exception.StackTrace, methodName);
            }
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="infoMessage">The message to log</param>
        /// /// <param name="methodName">The method that called</param>
        public void LogInfo(string infoMessage, string methodName = EMPTY_METHOD_NAME)
        {
            if (IsInfoEnabled)
            {
                LogMessage(InfoLabel, infoMessage, methodName);
            }
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="warningMessage">The message to log</param>
        /// /// <param name="methodName">The method that called</param>
        public void LogWarning(string warningMessage, string methodName = EMPTY_METHOD_NAME)
        {
            if (IsWarningEnabled)
            {
                LogMessage(WarningLabel, warningMessage, methodName);
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
            WriteToFile($"[{Timestamp}] <{ThreadName}> @{methodName} {type}: {message}");
        }
    }
}
