/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

namespace HergBot.Logging.TextLogging
{
    /// <summary>
    /// Formats the messages for plain text logging
    /// </summary>
    public class TextLogMessageGenerator : ILogMessageGenerator
    {
        /// <summary>
        /// Generates a formatted message for plain text logging
        /// </summary>
        /// <param name="timestamp">The logs timestamp</param>
        /// <param name="threadName">The name of the thread the message is logged from</param>
        /// <param name="methodName">The name of the method logging the message</param>
        /// <param name="type">The label for the type of log message</param>
        /// <param name="message">The log message</param>
        /// <returns>A formatted string for logging</returns>
        public string GenerateLogMessage(string timestamp, string threadName, string methodName, string type, string message)
        {
            return $"[{timestamp}] <{threadName}> @{methodName} {type}: {message}";
        }
    }
}
