/*
* PROJECT: HergBot Logging Tests
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

namespace HergBot.Logging_Tests.Data
{
    /// <summary>
    /// String versions of logging configurations for testing
    /// </summary>
    public class ConfigTestData
    {
        /// <summary>
        /// A basic logging configuration with all the default types enabled
        /// </summary>
        public string PrimaryConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <LoggingConfig
                            linuxPath=""/opt/hergbot/appname/logs/""
                            windowsPath=""./logs/""
                            fileName=""PrimaryTestLog""
                        >
                            <LoggingType key=""DEBUG"" enabled=""True"">Debug</LoggingType>
                            <LoggingType key=""ERROR"" enabled=""True"">Error</LoggingType>
                            <LoggingType key=""EXCEPTION"" enabled=""True"">Exception</LoggingType>
                            <LoggingType key=""INFO"" enabled=""True"">Information</LoggingType>
                            <LoggingType key=""WARNING"" enabled=""True"">Warning</LoggingType>
                        </LoggingConfig>";
            }
        }

        /// <summary>
        /// An alternate logging configuration with all the default types enabled except Debug
        /// </summary>
        public string AlternativeConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <LoggingConfig
                            linuxPath=""/opt/hergbot/appname/logs/""
                            windowsPath=""./logs/""
                            fileName=""AlternateTestLog""
                        >
                            <LoggingType key=""DEBUG"" enabled=""False"">Debug</LoggingType>
                            <LoggingType key=""ERROR"" enabled=""True"">Error</LoggingType>
                            <LoggingType key=""EXCEPTION"" enabled=""True"">Exception</LoggingType>
                            <LoggingType key=""INFO"" enabled=""True"">Information</LoggingType>
                            <LoggingType key=""WARNING"" enabled=""True"">Warning</LoggingType>
                        </LoggingConfig>";
            }
        }

        /// <summary>
        /// A logging configuration missing the fileName attribute in the LoggingConfig element
        /// </summary>
        public string MissingAttributeConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <LoggingConfig
                            linuxPath=""/opt/hergbot/appname/logs/""
                            windowsPath=""./logs/""
                        >
                            <LoggingType key=""DEBUG"" enabled=""True"">Debug</LoggingType>
                            <LoggingType key=""ERROR"" enabled=""True"">Error</LoggingType>
                            <LoggingType key=""EXCEPTION"" enabled=""True"">Exception</LoggingType>
                            <LoggingType key=""INFO"" enabled=""True"">Information</LoggingType>
                            <LoggingType key=""WARNING"" enabled=""True"">Warning</LoggingType>
                        </LoggingConfig>";
            }
        }

        /// <summary>
        /// A logging configuration missing the root LoggingConfig element
        /// </summary>
        public string MissingRootConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>";
            }
        }

        /// <summary>
        /// A logging configuration with the wrong root element name
        /// </summary>
        public string MissingElementConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <WrongRoot></WrongRoot>";
            }
        }

        /// <summary>
        /// A logging configuration with a new logging type outside the defaults
        /// </summary>
        public string UnknownLoggingTypeConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <LoggingConfig
                            linuxPath=""/opt/hergbot/appname/logs/""
                            windowsPath=""./logs/""
                            fileName=""PrimaryTestLog""
                        >
                            <LoggingType key=""DEBUG"" enabled=""True"">Debug</LoggingType>
                            <LoggingType key=""ERROR"" enabled=""True"">Error</LoggingType>
                            <LoggingType key=""EXCEPTION"" enabled=""True"">Exception</LoggingType>
                            <LoggingType key=""INFO"" enabled=""True"">Information</LoggingType>
                            <LoggingType key=""WARNING"" enabled=""True"">Warning</LoggingType>
                            <LoggingType key=""UNKNOWN"" enabled=""True"">Unknown</LoggingType>
                        </LoggingConfig>";
            }
        }

        /// <summary>
        /// A logging configuration missing the Debug default logging type
        /// </summary>
        public string MissingDebugLoggingTypeConfig
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <LoggingConfig
                            linuxPath=""/opt/hergbot/appname/logs/""
                            windowsPath=""./logs/""
                            fileName=""PrimaryTestLog""
                        >
                            <LoggingType key=""ERROR"" enabled=""True"">Error</LoggingType>
                            <LoggingType key=""EXCEPTION"" enabled=""True"">Exception</LoggingType>
                            <LoggingType key=""INFO"" enabled=""True"">Information</LoggingType>
                            <LoggingType key=""WARNING"" enabled=""True"">Warning</LoggingType>
                        </LoggingConfig>";
            }
        }
    }
}
