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

namespace HergBot.Common.Logging.Logger
{
    /// <summary>
    /// A base class that reads and handles the logging configuration
    /// </summary>
    public abstract class BaseLogger
    {
        private const string LOGGING_CONFIG_TAG = "LoggingConfig";

        private const string WINDOWS_PATH_ATTRIBUTE = "windowsPath";

        private const string LINUX_PATH_ATTIRBUTE = "linuxPath";

        /// <summary>
        /// The file name xml attribute
        /// </summary>
        private const string FILE_NAME_ATTRIBUTE = "fileName";

        private const string LOGGING_TYPE_TAG = "LoggingType";

        private const string KEY_ATTRIBUTE = "key";

        /// <summary>
        /// The logging type enabled xml attribute
        /// </summary>
        private const string ENABLED_ATTRIBUTE = "enabled";

        /// <summary>
        /// The debug logging type key
        /// </summary>
        private const string DEBUG_KEY = "DEBUG";

        /// <summary>
        /// The error logging type key
        /// </summary>
        private const string ERROR_KEY = "ERROR";

        /// <summary>
        /// The exception logging type key
        /// </summary>
        private const string EXCEPTION_KEY = "EXCEPTION";

        /// <summary>
        /// The information logging type key
        /// </summary>
        private const string INFO_KEY = "INFO";

        /// <summary>
        /// The warning logging type xml value
        /// </summary>
        private const string WARNING_KEY = "WARNING";

        /// <summary>
        /// The log file locking object for thread safe logging
        /// </summary>
        private static object _logFileLock = new object();

        private string _configFilePath = null;

        private DateTime _currentLogFileDate = DateTime.Now.Date;

        private string _logFileExtension = ".log";

        /// <summary>
        /// The base file name for the log file (Without extension and date)
        /// </summary>
        private string _baseFileName = "HergBotLog";

        private string _logDirectory = "./";

        private string _fullLogFilePath = null;

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

        /// <summary>
        /// The label to log debug messages with
        /// </summary>
        protected string debugLabel;

        /// <summary>
        /// The label to log error messages with
        /// </summary>
        protected string errorLabel;

        /// <summary>
        /// The label to log exception messages with
        /// </summary>
        protected string excpetionLabel;

        /// <summary>
        /// The label to log information messages with
        /// </summary>
        protected string informationLabel;

        /// <summary>
        /// The label to log warning messages with
        /// </summary>
        protected string warningLabel;

        /// <summary>
        /// Whether debug log messages are enabled
        /// </summary>
        protected bool debugEnabled;

        /// <summary>
        /// Whether error log messages are enabled
        /// </summary>
        protected bool errorEnabled;

        /// <summary>
        /// Whether exception log messages are enabled
        /// </summary>
        protected bool exceptionEnabled;

        /// <summary>
        /// Whether information log messages are enabled
        /// </summary>
        protected bool informationEnabled;

        /// <summary>
        /// Whether warning log messages are enabled
        /// </summary>
        protected bool warningEnabled;

        protected string Timestamp { get { return DateTime.Now.ToString(DATE_TIME_FORMAT); } }

        protected string ThreadName { get { return Thread.CurrentThread.Name;  } }

        /// <summary>
        /// Constructor
        /// </summary>
        private BaseLogger()
        {
            // The defaults, by default we want everything
            debugEnabled = true;
            errorEnabled = true;
            exceptionEnabled = true;
            informationEnabled = true;
            warningEnabled = true;

            // Default labels
            debugLabel = "DB";
            errorLabel = "ER";
            excpetionLabel = "EX";
            informationLabel = "IN";
            warningLabel = "WA";
        }

        public BaseLogger(string configFilePath, string fileExtension) : this()
        {
            _configFilePath = configFilePath;
            _logFileExtension = fileExtension;
            
            if (_configFilePath != null)
            {
                ReadConfigurationFile();
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

        /// <summary>
        /// Reads the logging configuration file
        /// </summary>
        private void ReadConfigurationFile()
        {
            if (File.Exists(_configFilePath))
            {
                XDocument configFile = XDocument.Load(_configFilePath);
                XAttribute fileNameAttribute = configFile.Root.Attribute(FILE_NAME_ATTRIBUTE);
                XAttribute windowsDirectory = configFile.Root.Attribute(WINDOWS_PATH_ATTRIBUTE);
                XAttribute linuxDirectory = configFile.Root.Attribute(LINUX_PATH_ATTIRBUTE);
                _baseFileName = fileNameAttribute.Value;
                _logDirectory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? windowsDirectory.Value : linuxDirectory.Value;
                foreach (XElement element in configFile.Root.Elements())
                {
                    ReadConfigurationEntry(
                        element.Attribute(KEY_ATTRIBUTE).Value,
                        element.Attribute(ENABLED_ATTRIBUTE).Value,
                        element.Value
                    );
                }
            }
        }

        /// <summary>
        /// Parses an xml element for the logging info
        /// </summary>
        /// <param name="type">The type of log entry</param>
        /// <param name="enabledValue">Whether this type is enabled</param>
        /// <param name="label">The label for this type of entry</param>
        private void ReadConfigurationEntry(string type, string enabledValue, string label)
        {
            bool isTypeEnabled = true;

            // Try to parse out the vale, if it doesn't work it will default to true
            bool.TryParse(enabledValue, out isTypeEnabled);

            switch (type)
            {
                case DEBUG_KEY:
                    debugEnabled = isTypeEnabled;
                    debugLabel = label;
                    break;
                case ERROR_KEY:
                    errorEnabled = isTypeEnabled;
                    errorLabel = label;
                    break;
                case EXCEPTION_KEY:
                    exceptionEnabled = isTypeEnabled;
                    excpetionLabel = label;
                    break;
                case INFO_KEY:
                    informationEnabled = isTypeEnabled;
                    informationLabel = label;
                    break;
                case WARNING_KEY:
                    warningEnabled = isTypeEnabled;
                    warningLabel = label;
                    break;
                default:
                    // If there is an unknown logging type it will be defaulted so we don't need to do anything special
                    break;
            }
        }

        private void CreateFilePath()
        {
            _fullLogFilePath = $"{_logDirectory}{_baseFileName}_{_currentLogFileDate.ToString(DATE_FORMAT)}.{_logFileExtension}";
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
