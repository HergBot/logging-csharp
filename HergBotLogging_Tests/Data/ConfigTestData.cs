using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HergBotLogging_Tests.Data
{
    public class ConfigTestData
    {
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

        // Missing fileName attribute
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
    }
}
