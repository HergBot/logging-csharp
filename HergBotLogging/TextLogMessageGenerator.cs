namespace HergBotLogging
{
    public class TextLogMessageGenerator : ILogMessageGenerator
    {
        public string GenerateLogMessage(string timestamp, string threadName, string methodName, string type, string message)
        {
            return $"[{timestamp}] <{threadName}> @{methodName} {type}: {message}";
        }
    }
}
