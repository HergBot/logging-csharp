using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HergBotLogging
{
    internal class LoggingConfigElement
    {
        public static string TAG = "LoggingConfig";
        
        private const string WINDOWS_PATH_ATTRIBUTE = "windowsPath";

        private const string LINUX_PATH_ATTIRBUTE = "linuxPath";

        /// <summary>
        /// The file name xml attribute
        /// </summary>
        private const string FILE_NAME_ATTRIBUTE = "fileName";

        private string _windowsPath;

        private string _linuxPath;

        private string _fileName;

        private List<LoggingTypeElement> _loggingTypes;

        public IEnumerable<LoggingTypeElement> LoggingTypes { get { return _loggingTypes; } }

        public LoggingConfigElement(string windowsPath, string linuxPath, string fileName)
        {
            _windowsPath = windowsPath;
            _linuxPath = linuxPath;
            _fileName = fileName;
            _loggingTypes = new List<LoggingTypeElement>();
        }

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

        public LoggingConfiguration ToLoggingConfiguration()
        {
            return new LoggingConfiguration(_fileName, _windowsPath, _linuxPath);
        }

        private void AddLoggingType(LoggingTypeElement type)
        {
            _loggingTypes.Add(type);
        }
    }
}
