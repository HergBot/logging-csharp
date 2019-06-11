using System;
using System.Collections.Generic;
using System.Text;

namespace HergBotLogging
{
    public interface ILogMessageGenerator
    {
        string GenerateLogMessage(string timestamp, string threadName, string methodName, string type, string message);
    }
}
