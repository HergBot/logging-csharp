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

        public LoggingConfiguration(string baseFileName, string windowsLogPath, string linuxLogPath)
        {
            BaseFileName = baseFileName;
            LogDirectory = OperatingSystemUtilities.IsWindows() ? windowsLogPath : linuxLogPath;
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
            LoggingConfigElement loggingConfigElement = LoggingConfigElement.Parse(
                configDoc.Element(LoggingConfigElement.TAG)
            );
            return loggingConfigElement.ToLoggingConfiguration();
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
    }
}
