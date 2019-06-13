using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HergBotLogging
{
    internal class LoggingTypeElement
    {
        public static string TAG = "LoggingType";

        private const string KEY_ATTRIBUTE = "key";

        /// <summary>
        /// The logging type enabled xml attribute
        /// </summary>
        private const string ENABLED_ATTRIBUTE = "enabled";

        private string _key;

        public string _enabled;

        public string _label;

        public LoggingTypeElement(string key, string enabled, string label)
        {
            _key = key;
            _enabled = enabled;
            _label = label;
        }

        public static  LoggingTypeElement Parse(XElement element)
        {
            if (element == null)
            {
                throw new FormatException($"'{TAG}' tag not found.");
            }

            XAttribute keyAttribute = element.Attribute(KEY_ATTRIBUTE);
            XAttribute enabledAttribute = element.Attribute(ENABLED_ATTRIBUTE);

            if (keyAttribute == null)
            {
                throw new FormatException($"'{KEY_ATTRIBUTE}' attribute of {TAG} element is missing.");
            }

            if (enabledAttribute == null)
            {
                throw new FormatException($"'{ENABLED_ATTRIBUTE}' attribute of LoggingType element is missing.");
            }

            return new LoggingTypeElement(keyAttribute.Value, enabledAttribute.Value, element.Value);
        }

        public LoggingType ToLoggingType()
        {
            if (!bool.TryParse(_enabled, out bool isTypeEnabled))
            {
                throw new FormatException($"'{ENABLED_ATTRIBUTE}' attribute '{_enabled}' could not be parsed as a bool.");
            }
            return new LoggingType(isTypeEnabled, _label);
        }
    }
}
