/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 04/08/2017
*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

using HergBotLogging.Configuration;

namespace HergBotLogging
{
    /// <summary>
    /// A base class that reads and handles the logging configuration
    /// </summary>
    public abstract class BaseLogger
    {
        /// <summary>
        /// The format string for just the date
        /// </summary>
        protected const string DATE_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// The format string for a date with time
        /// </summary>
        protected const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Empty method name constant for the parameter default
        /// </summary>
        protected const string EMPTY_METHOD_NAME = "";

        protected LoggingConfiguration Configuration;

        protected string Timestamp { get { return DateTime.Now.ToString(DATE_TIME_FORMAT); } }

        protected string ThreadName { get { return Thread.CurrentThread.Name;  } }

        /// <summary>
        /// Constructor
        /// </summary>
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
