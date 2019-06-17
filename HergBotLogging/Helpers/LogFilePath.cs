/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;

using HergBot.Utilities.DateTimeUtilities;

namespace HergBot.Logging.Helpers
{
    /// <summary>
    /// Controls the logic of constructing and updating the path for log files
    /// </summary>
    public class LogFilePath
    {
        /// <summary>
        /// The format string for the date
        /// </summary>
        protected const string DATE_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// Object to provide us with DateTime objects
        /// </summary>
        private IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// The directory portion of the log path
        /// </summary>
        private string _directory;

        /// <summary>
        /// The file name portion of the log path
        /// </summary>
        private string _baseFileName;

        /// <summary>
        /// The file extension portion of the log path
        /// </summary>
        private string _fileExtension;

        /// <summary>
        /// The date the current log file is for
        /// </summary>
        private DateTime _currentLogFileDate;

        /// <summary>
        /// The fully constructed path for the log file
        /// </summary>
        public string FullFilePath { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="baseFileName"></param>
        /// <param name="fileExtension"></param>
        public LogFilePath(string directory, string baseFileName, string fileExtension)
        {
            _dateTimeProvider = new LocalDateTimeProvider();
            _directory = directory;
            _baseFileName = baseFileName;
            _fileExtension = fileExtension;
            _currentLogFileDate = _dateTimeProvider.Now.Date;
            ConstructFullFileName();
        }

        /// <summary>
        /// Constructor to allow changing the DateTime provider from local time
        /// </summary>
        /// <param name="dateTimeProvider">The DateTime provider to use</param>
        /// <param name="directory">The directory to log to</param>
        /// <param name="baseFileName">The logs file name</param>
        /// <param name="fileExtension">The logs file extension</param>
        public LogFilePath(IDateTimeProvider dateTimeProvider, string directory, string baseFileName, string fileExtension)
        {
            _dateTimeProvider = dateTimeProvider;
            _directory = directory;
            _baseFileName = baseFileName;
            _fileExtension = fileExtension;
            _currentLogFileDate = _dateTimeProvider.Now.Date;
            ConstructFullFileName();
        }

        /// <summary>
        /// Checks if the log file is still for the current time and updates the full path accordingly
        /// </summary>
        public void CheckDate()
        {
            DateTime currentDate = _dateTimeProvider.Now.Date;
            if (_currentLogFileDate < currentDate)
            {
                _currentLogFileDate = currentDate;
                ConstructFullFileName();
            }
        }

        /// <summary>
        /// Constructs the full file path
        /// </summary>
        private void ConstructFullFileName()
        {
            FullFilePath = $"{_directory}{_baseFileName}_{_currentLogFileDate.ToString(DATE_FORMAT)}.{_fileExtension}";
        }
    }
}
