/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

namespace HergBot.Logging
{
    /// <summary>
    /// An interface to follow for implementing HergBot Log Message Generator classes
    /// </summary>
    public interface ILogMessageGenerator
    {
        string GenerateLogMessage(string timestamp, string threadName, string methodName, string type, string message);
    }
}
