using HergBotUtilities.EnvironmentUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace HergBotLogging
{
    internal class LoggingConfiguration
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

        public LoggingConfiguration()
        {
            DefaultConfiguration();
        }

        public void LoadConfiguration(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Logging configuration file at '{configFilePath}' not found.");
            }

            XDocument configFile = XDocument.Load(configFilePath);
            XAttribute fileNameAttribute = configFile.Root.Attribute(FILE_NAME_ATTRIBUTE);
            XAttribute windowsDirectory = configFile.Root.Attribute(WINDOWS_PATH_ATTRIBUTE);
            XAttribute linuxDirectory = configFile.Root.Attribute(LINUX_PATH_ATTIRBUTE);
            BaseFileName = fileNameAttribute.Value;
            LogDirectory = OperatingSystemUtilities.IsWindows() ? windowsDirectory.Value : linuxDirectory.Value;
            foreach (XElement element in configFile.Root.Elements())
            {
                ReadConfigurationEntry(
                    element.Attribute(KEY_ATTRIBUTE).Value,
                    element.Attribute(ENABLED_ATTRIBUTE).Value,
                    element.Value
                );
            }
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
