/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace HergBotLogging.Configuration
{
    /// <summary>
    /// Parses and contains the information in a LoggingConfig XML element.
    /// </summary>
    internal class LoggingConfigElement
    {
        /// <summary>
        /// The XML tag identifier
        /// </summary>
        public static string TAG = "LoggingConfig";
        
        /// <summary>
        /// The XML attribute for the Windows logging path
        /// </summary>
        private const string WINDOWS_PATH_ATTRIBUTE = "windowsPath";

        /// <summary>
        /// The XML attribute for the Linux logging path
        /// </summary>
        private const string LINUX_PATH_ATTIRBUTE = "linuxPath";

        /// <summary>
        /// The XML attribute for the log file name
        /// </summary>
        private const string FILE_NAME_ATTRIBUTE = "fileName";

        /// <summary>
        /// The path to use for Windows logging
        /// </summary>
        private string _windowsPath;

        /// <summary>
        /// The path to use for Linux logging
        /// </summary>
        private string _linuxPath;

        /// <summary>
        /// The base file name for the log
        /// </summary>
        private string _fileName;

        /// <summary>
        /// The LoggingType children of the LoggingConfig element
        /// </summary>
        private List<LoggingTypeElement> _loggingTypes;

        /// <summary>
        /// Constructs an empty LoggingConfig element
        /// </summary>
        /// <param name="windowsPath">The file path to use for logging on Windows</param>
        /// <param name="linuxPath">The file path to use for logging on Linux</param>
        /// <param name="fileName">The base file name for the log</param>
        public LoggingConfigElement(string windowsPath, string linuxPath, string fileName)
        {
            _windowsPath = windowsPath;
            _linuxPath = linuxPath;
            _fileName = fileName;
            _loggingTypes = new List<LoggingTypeElement>();
        }

        /// <summary>
        /// Parses a LoggingConfig XML element
        /// </summary>
        /// <param name="element">The XML element</param>
        /// <returns>A populated LoggingConfigElement</returns>
        public static LoggingConfigElement Parse(XElement element)
        {
            if (element == null)
            {
                throw new FormatException($"'{TAG}' tag not found.");
            }

            XAttribute fileNameAttribute = element.Attribute(FILE_NAME_ATTRIBUTE);
            XAttribute windowsPathAttribute = element.Attribute(WINDOWS_PATH_ATTRIBUTE);
            XAttribute linuxPathAttribute = element.Attribute(LINUX_PATH_ATTIRBUTE);

            if (fileNameAttribute == null)
            {
                throw new FormatException($"'{FILE_NAME_ATTRIBUTE}' attribute of {TAG} element is missing.");
            }

            if (windowsPathAttribute == null)
            {
                throw new FormatException($"'{WINDOWS_PATH_ATTRIBUTE}' attribute of {TAG} element is missing.");
            }

            if (linuxPathAttribute == null)
            {
                throw new FormatException($"'{LINUX_PATH_ATTIRBUTE}' attribute of {TAG} element is missing.");
            }

            LoggingConfigElement configElement = new LoggingConfigElement(
                windowsPathAttribute.Value,
                linuxPathAttribute.Value,
                fileNameAttribute.Value
            );

            foreach (XElement typeElement in element.Elements(LoggingTypeElement.TAG))
            {
                configElement.AddLoggingType(LoggingTypeElement.Parse(typeElement));
            }

            return configElement;
        }

        /// <summary>
        /// Converts the LoggingConfigElement into a LoggingConfiguration
        /// </summary>
        /// <returns>A populated LoggingConfiguration object</returns>
        public LoggingConfiguration ToLoggingConfiguration()
        {
            LoggingConfiguration config = new LoggingConfiguration(_fileName, _windowsPath, _linuxPath);
            foreach(LoggingTypeElement element in _loggingTypes)
            {
                config.AddLoggingType(element.Key, element.ToLoggingType());
            }
            return config;
        }

        /// <summary>
        /// Adds a LoggingTypeElement to the list
        /// </summary>
        /// <param name="type">The LoggingTypeElement to add</param>
        private void AddLoggingType(LoggingTypeElement type)
        {
            _loggingTypes.Add(type);
        }
    }
}
