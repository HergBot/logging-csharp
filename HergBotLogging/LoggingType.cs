using System;
using System.Collections.Generic;
using System.Text;

namespace HergBotLogging
{
    public class LoggingType
    {
        public static string DEBUG_KEY = "DEBUG";

        public static string ERROR_KEY = "ERROR";

        public static string EXCEPTION_KEY = "EXCEPTION";

        public static string INFO_KEY = "INFO";

        public static string WARNING_KEY = "WARNING";

        public bool Enabled { get; }

        public string Label { get; }

        public LoggingType(bool enabled, string label)
        {
            Enabled = enabled;
            Label = label;
        }
    }
}
