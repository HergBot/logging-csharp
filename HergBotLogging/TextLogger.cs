using System;
using System.Collections.Generic;
using System.Text;

namespace HergBotLogging
{
    public class TextLogger : FileLogger
    {
        private const string TEXT_LOG_EXTENSION = ".log";

        public TextLogger(string configFile) : base(configFile, new TextLogMessageGenerator(), TEXT_LOG_EXTENSION)
        {

        }
    }
}
