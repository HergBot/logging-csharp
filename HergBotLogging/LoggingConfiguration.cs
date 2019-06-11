using HergBotUtilities.EnvironmentUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace HergBotLogging
{
    public class LoggingConfiguration
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

        private Dictionary<string, LoggingType> _loggingTypes;

        public string LogDirectory { get; private set; }

        public string BaseFileName { get; private set; }

        public bool IsDebugEnabled { get { return IsTypeEnabled(LoggingType.DEBUG_KEY); } }

        public string DebugLabel { get { return GetTypeLabel(LoggingType.DEBUG_KEY); } }

        public bool IsErrorEnabled { get { return IsTypeEnabled(LoggingType.ERROR_KEY); } }

        public string ErrorLabel { get { return GetTypeLabel(LoggingType.ERROR_KEY); } }

        public bool IsExceptionEnabled { get { return IsTypeEnabled(LoggingType.EXCEPTION_KEY); } }

        public string ExceptionLabel { get { return GetTypeLabel(LoggingType.EXCEPTION_KEY); } }

        public bool IsInfoEnabled { get { return IsTypeEnabled(LoggingType.INFO_KEY); } }

        public string InfoLabel { get { return GetTypeLabel(LoggingType.INFO_KEY); } }

        public bool IsWarningEnabled { get { return IsTypeEnabled(LoggingType.WARNING_KEY); } }

        public string WarningLabel { get { return GetTypeLabel(LoggingType.WARNING_KEY); } }

        public LoggingConfiguration()
        {
            DefaultConfiguration();
        }

        public static LoggingConfiguration LoadFromFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Logging configuration file at '{configFilePath}' not found.");
            }

            XDocument config = XDocument.Load(configFilePath);
            return LoadConfiguration(config);
        }

        public static LoggingConfiguration LoadFromString(string configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration))
            {
                throw new ArgumentNullException("Configuration is null, empty, or whitespace.");
            }

            XDocument config = XDocument.Parse(configuration);
            return LoadConfiguration(config);
        }

        public bool IsTypeEnabled(string key)
        {
            if (!_loggingTypes.ContainsKey(key))
            {
                return false;
            }

            return _loggingTypes[key].Enabled;
        }

        public string GetTypeLabel(string key)
        {
            if (!_loggingTypes.ContainsKey(key))
            {
                return "UNKNOWN";
            }

            return _loggingTypes[key].Label;
        }

        private static LoggingConfiguration LoadConfiguration(XDocument configDoc)
        {
            LoggingConfiguration loadedConfig = new LoggingConfiguration();
            XAttribute fileNameAttribute = configDoc.Root.Attribute(FILE_NAME_ATTRIBUTE);
            XAttribute windowsDirectory = configDoc.Root.Attribute(WINDOWS_PATH_ATTRIBUTE);
            XAttribute linuxDirectory = configDoc.Root.Attribute(LINUX_PATH_ATTIRBUTE);
            loadedConfig.BaseFileName = fileNameAttribute.Value;
            loadedConfig.LogDirectory = OperatingSystemUtilities.IsWindows() ? windowsDirectory.Value : linuxDirectory.Value;
            foreach (XElement element in configDoc.Root.Elements())
            {
                loadedConfig.ReadConfigurationEntry(
                    element.Attribute(KEY_ATTRIBUTE).Value,
                    element.Attribute(ENABLED_ATTRIBUTE).Value,
                    element.Value
                );
            }

            return loadedConfig;
        }

        private void DefaultConfiguration()
        {
            LogDirectory = "./logs/";
            BaseFileName = "HergBotLog";
            _loggingTypes = new Dictionary<string, LoggingType>();
            _loggingTypes.Add(LoggingType.DEBUG_KEY, new LoggingType(true, "Debug"));
            _loggingTypes.Add(LoggingType.ERROR_KEY, new LoggingType(true, "Error"));
            _loggingTypes.Add(LoggingType.EXCEPTION_KEY, new LoggingType(true, "Exception"));
            _loggingTypes.Add(LoggingType.INFO_KEY, new LoggingType(true, "Info"));
            _loggingTypes.Add(LoggingType.WARNING_KEY, new LoggingType(true, "Warning"));
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
            _loggingTypes.Add(type, new LoggingType(isTypeEnabled, label));
        }
    }
}
