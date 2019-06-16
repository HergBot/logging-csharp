/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

namespace HergBotLogging.Configuration
{
    /// <summary>
    /// Contains the information about a specific type of log message
    /// </summary>
    public class LoggingType
    {
        /// <summary>
        /// Key used for the Debug logging type
        /// </summary>
        public static string DEBUG_KEY = "DEBUG";

        /// <summary>
        /// Key used for the Error logging type
        /// </summary>
        public static string ERROR_KEY = "ERROR";

        /// <summary>
        /// Key used for the Exception logging type
        /// </summary>
        public static string EXCEPTION_KEY = "EXCEPTION";

        /// <summary>
        /// Key used for the Info logging type
        /// </summary>
        public static string INFO_KEY = "INFO";

        /// <summary>
        /// Key used for the Warning logging type
        /// </summary>
        public static string WARNING_KEY = "WARNING";

        /// <summary>
        /// If the logging type is enabled or not
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// The label used when logging this type
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Constructs a basic logging type
        /// </summary>
        /// <param name="enabled">Whether the logging type is enabled or not</param>
        /// <param name="label">The label to use for the logging type</param>
        public LoggingType(bool enabled, string label)
        {
            Enabled = enabled;
            Label = label;
        }
    }
}
