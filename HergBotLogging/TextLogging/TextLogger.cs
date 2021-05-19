/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

namespace HergBot.Logging.TextLogging
{
    /// <summary>
    /// Logs preformatted plain text messages to a file
    /// </summary>
    public class TextLogger : FileLogger
    {
        /// <summary>
        /// The extension to use for the text logs
        /// </summary>
        private const string TEXT_LOG_EXTENSION = "log";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configFile">The path to the logging configuration file</param>
        public TextLogger(string configFile) : base(configFile, new TextLogMessageGenerator(), TEXT_LOG_EXTENSION)
        {

        }
    }
}
