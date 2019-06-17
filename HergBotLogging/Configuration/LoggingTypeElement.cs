/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.Xml.Linq;

namespace HergBot.Logging.Configuration
{
    /// <summary>
    /// Parses and contains the information in a LoggingType XML element
    /// </summary>
    internal class LoggingTypeElement
    {
        /// <summary>
        /// The LoggingType element identifier
        /// </summary>
        public static string TAG = "LoggingType";

        /// <summary>
        /// The XML attribute for the type's key
        /// </summary>
        private const string KEY_ATTRIBUTE = "key";

        /// <summary>
        /// The XML attribute for the enabled attribute
        /// </summary>
        private const string ENABLED_ATTRIBUTE = "enabled";

        /// <summary>
        /// The value of the key attribute
        /// </summary>
        public string Key;

        /// <summary>
        /// The value of the enabled attribute
        /// </summary>
        public string Enabled;

        /// <summary>
        /// The value inside the tag
        /// </summary>
        public string Label;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">The value of the key attribute</param>
        /// <param name="enabled">The value of the enabled attribute</param>
        /// <param name="label">The value inside the tag</param>
        public LoggingTypeElement(string key, string enabled, string label)
        {
            Key = key;
            Enabled = enabled;
            Label = label;
        }

        /// <summary>
        /// Parses a LoggingTypeElement from an XML element
        /// </summary>
        /// <param name="element">The XML element to parse</param>
        /// <returns>A populated LoggingTypeElement</returns>
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

        /// <summary>
        /// Converts a LoggingTypeElement into a LoggingType
        /// </summary>
        /// <returns>A populated LoggingType object</returns>
        public LoggingType ToLoggingType()
        {
            if (!bool.TryParse(Enabled, out bool isTypeEnabled))
            {
                throw new FormatException($"'{ENABLED_ATTRIBUTE}' attribute '{Enabled}' could not be parsed as a bool.");
            }
            return new LoggingType(isTypeEnabled, Label);
        }
    }
}
