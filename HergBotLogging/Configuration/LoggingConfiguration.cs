/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

using HergBot.Utilities.EnvironmentUtilities;

namespace HergBot.Logging.Configuration
{
    /// <summary>
    /// Contains the configuration for using HergBot Logging
    /// </summary>
    public class LoggingConfiguration
    {
        /// <summary>
        /// Current logging types used in the configuration
        /// </summary>
        private Dictionary<string, LoggingType> _loggingTypes;

        /// <summary>
        /// The directory to save log files to
        /// </summary>
        public string LogDirectory { get; private set; }

        /// <summary>
        /// The base file name of the log (excluding the extension because that is log specific)
        /// </summary>
        public string BaseFileName { get; private set; }

        /// <summary>
        /// Checks if the Debug logging type is enabled
        /// </summary>
        public bool IsDebugEnabled { get { return IsTypeEnabled(LoggingType.DEBUG_KEY); } }

        /// <summary>
        /// The label used for Debug type messages
        /// </summary>
        public string DebugLabel { get { return GetTypeLabel(LoggingType.DEBUG_KEY); } }

        /// <summary>
        /// Checks if the Error logging type is enabled
        /// </summary>
        public bool IsErrorEnabled { get { return IsTypeEnabled(LoggingType.ERROR_KEY); } }

        /// <summary>
        /// The label used for Error type messages
        /// </summary>
        public string ErrorLabel { get { return GetTypeLabel(LoggingType.ERROR_KEY); } }

        /// <summary>
        /// Checks if the Exception logging type is enabled
        /// </summary>
        public bool IsExceptionEnabled { get { return IsTypeEnabled(LoggingType.EXCEPTION_KEY); } }

        /// <summary>
        /// The label used for Exception type messages
        /// </summary>
        public string ExceptionLabel { get { return GetTypeLabel(LoggingType.EXCEPTION_KEY); } }

        /// <summary>
        /// Checks if the Info logging type is enabled
        /// </summary>
        public bool IsInfoEnabled { get { return IsTypeEnabled(LoggingType.INFO_KEY); } }

        /// <summary>
        /// The label used for Info type messages
        /// </summary>
        public string InfoLabel { get { return GetTypeLabel(LoggingType.INFO_KEY); } }

        /// <summary>
        /// Checks if the Warning logging type is enabled
        /// </summary>
        public bool IsWarningEnabled { get { return IsTypeEnabled(LoggingType.WARNING_KEY); } }

        /// <summary>
        /// The label used for Warning type messages
        /// </summary>
        public string WarningLabel { get { return GetTypeLabel(LoggingType.WARNING_KEY); } }

        /// <summary>
        /// Constructs the default configuration
        /// </summary>
        public LoggingConfiguration()
        {
            DefaultConfiguration();
        }

        /// <summary>
        /// Constructs a configuration with no logging types
        /// </summary>
        /// <param name="baseFileName">The base file name to use for the log</param>
        /// <param name="windowsLogPath">The path to place the log file in on Windows</param>
        /// <param name="linuxLogPath">The path to place the log file in on Linux</param>
        public LoggingConfiguration(string baseFileName, string windowsLogPath, string linuxLogPath)
        {
            BaseFileName = baseFileName;
            LogDirectory = OperatingSystemUtilities.IsWindows() ? windowsLogPath : linuxLogPath;
            _loggingTypes = new Dictionary<string, LoggingType>();
        }

        /// <summary>
        /// Loads a LoggingConfiguration from a file
        /// </summary>
        /// <param name="configFilePath">The path to the config file</param>
        /// <returns>A populated LoggingConfiguration object</returns>
        public static LoggingConfiguration LoadFromFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Logging configuration file at '{configFilePath}' not found.");
            }

            XDocument config = XDocument.Load(configFilePath);
            return LoadConfiguration(config);
        }

        /// <summary>
        /// Loads a LoggingConfiguration from a string
        /// </summary>
        /// <param name="configuration">The string containing the configuration</param>
        /// <returns>A populated LoggingConfiguration object</returns>
        public static LoggingConfiguration LoadFromString(string configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration))
            {
                throw new ArgumentNullException("Configuration is null, empty, or whitespace.");
            }

            XDocument config = XDocument.Parse(configuration);
            return LoadConfiguration(config);
        }

        /// <summary>
        /// Checks if a logging type is currently enabled
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsTypeEnabled(string key)
        {
            if (!_loggingTypes.ContainsKey(key))
            {
                return false;
            }

            return _loggingTypes[key].Enabled;
        }
        /// <summary>
        /// Gets the label associated with a logging type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTypeLabel(string key)
        {
            if (!_loggingTypes.ContainsKey(key))
            {
                return "???";
            }

            return _loggingTypes[key].Label;
        }

        /// <summary>
        /// Adds a logging type to the configuration
        /// </summary>
        /// <param name="key">The logging type key to add</param>
        /// <param name="type">The logging type information</param>
        /// <returns>True if successfully added or false</returns>
        public bool AddLoggingType(string key, LoggingType type)
        {
            if (_loggingTypes.ContainsKey(key))
            {
                return false;
            }

            _loggingTypes.Add(key, type);
            return true;
        }

        /// <summary>
        /// Loads the configuration from an XDocument object
        /// </summary>
        /// <param name="configDoc">The XML document object</param>
        /// <returns>The populated LoggingConfiguration object</returns>
        private static LoggingConfiguration LoadConfiguration(XDocument configDoc)
        {
            LoggingConfigElement loggingConfigElement = LoggingConfigElement.Parse(
                configDoc.Element(LoggingConfigElement.TAG)
            );
            return loggingConfigElement.ToLoggingConfiguration();
        }

        /// <summary>
        /// Sets up a default configuration
        /// </summary>
        private void DefaultConfiguration()
        {
            LogDirectory = "./logs/";
            BaseFileName = "HergBotLog";
            _loggingTypes = new Dictionary<string, LoggingType>();
            _loggingTypes.Add(LoggingType.DEBUG_KEY, new LoggingType(true, "Debug"));
            _loggingTypes.Add(LoggingType.ERROR_KEY, new LoggingType(true, "Error"));
            _loggingTypes.Add(LoggingType.EXCEPTION_KEY, new LoggingType(true, "Exception"));
            _loggingTypes.Add(LoggingType.INFO_KEY, new LoggingType(true, "Information"));
            _loggingTypes.Add(LoggingType.WARNING_KEY, new LoggingType(true, "Warning"));
        }
    }
}
