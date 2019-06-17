/*
* PROJECT: HergBot Logging Tests
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;

using Moq;
using NUnit.Framework;

using HergBot.Logging.Helpers;
using HergBot.Utilities.DateTimeUtilities;

namespace HergBot.Logging_Tests
{
    public class LogFilePath_Tests
    {
        private const string TEST_DIRECTORY = "./logs/";

        private const string TEST_FILE_NAME = "Test";

        private const string TEST_LOG_EXTENSION = "log";

        private const string DATE_FORMAT = "yyyy-MM-dd";

        private Mock<IDateTimeProvider> _testDateTimeProvider;

        private DateTime _currentDate;

        private DateTime _nextDayDate;

        private LogFilePath _logPath;

        [SetUp]
        public void SetUp()
        {
            _currentDate = DateTime.Now.Date;
            _nextDayDate = _currentDate.AddDays(1);

            // Mock out the Date Time Provider
            _testDateTimeProvider = new Mock<IDateTimeProvider>();
            _testDateTimeProvider.Setup(x => x.Now).Returns(_currentDate);

            _logPath = new LogFilePath(_testDateTimeProvider.Object, TEST_DIRECTORY, TEST_FILE_NAME, TEST_LOG_EXTENSION);
        }

        [Test]
        public void FullFilePath_Format()
        {
            string expectedFormat = $"{TEST_DIRECTORY}{TEST_FILE_NAME}_{_currentDate.ToString(DATE_FORMAT)}.{TEST_LOG_EXTENSION}";
            Assert.AreEqual(expectedFormat, _logPath.FullFilePath);
        }

        [Test]
        public void FullFilePath_Rollover()
        {
            // Create a date time provider that will start off on the current day and simulate a rollover to the next day
            Mock<IDateTimeProvider> rolloverProvider;
            rolloverProvider = new Mock<IDateTimeProvider>();
            rolloverProvider.SetupSequence(x => x.Now)
                .Returns(_currentDate.Date)
                .Returns(_nextDayDate.Date);

            _logPath = new LogFilePath(rolloverProvider.Object, TEST_DIRECTORY, TEST_FILE_NAME, TEST_LOG_EXTENSION);
            _logPath.CheckDate();

            string expectedFormat = $"{TEST_DIRECTORY}{TEST_FILE_NAME}_{_nextDayDate.ToString(DATE_FORMAT)}.{TEST_LOG_EXTENSION}";
            Assert.AreEqual(expectedFormat, _logPath.FullFilePath);
        }
    }
}
