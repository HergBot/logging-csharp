/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.Threading;

using HergBot.Logging.Configuration;

namespace HergBot.Logging
{
    /// <summary>
    /// A base class that reads and handles the logging configuration
    /// </summary>
    public abstract class BaseLogger
    {
        /// <summary>
        /// The format string for a date with time
        /// </summary>
        protected const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Empty method name constant for the parameter default
        /// </summary>
        protected const string EMPTY_METHOD_NAME = "";

        /// <summary>
        /// The configuration used for logging
        /// </summary>
        protected LoggingConfiguration Configuration;

        /// <summary>
        /// A formatted timestamp string
        /// </summary>
        protected string Timestamp { get { return DateTime.Now.ToString(DATE_TIME_FORMAT); } }

        /// <summary>
        /// The current threads name
        /// </summary>
        protected string ThreadName { get { return Thread.CurrentThread.Name;  } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configFilePath">Path to the logging configuration file</param>
        public BaseLogger(string configFilePath)
        {
            Configuration = new LoggingConfiguration();
            
            if (configFilePath != null)
            {
                Configuration = LoggingConfiguration.LoadFromFile(configFilePath);
            }
        }
    }
}
