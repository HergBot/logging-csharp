/*
* PROJECT: HergBot Logging Tests
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;

using NUnit.Framework;

using HergBot.Logging.TextLogging;

namespace HergBot.Logging_Tests
{
    public class TextLogMessageGenerator_Tests
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private const string TEST_THREAD_NAME = "TestThread";

        private const string TEST_METHOD_NAME = "TestMethod";

        private const string TEST_TYPE = "TestType";

        private const string TEST_MESSAGE = "Test Message";

        private TextLogMessageGenerator _generator;

        [SetUp]
        public void SetUp()
        {
            _generator = new TextLogMessageGenerator();
        }

        [Test]
        public void GenerateLogMessage_Format()
        {
            string timestamp = DateTime.Now.Date.ToString(DATE_TIME_FORMAT);
            string expected = $"[{timestamp}] <{TEST_THREAD_NAME}> @{TEST_METHOD_NAME} {TEST_TYPE}: {TEST_MESSAGE}";
            string actual = _generator.GenerateLogMessage(
                timestamp,
                TEST_THREAD_NAME,
                TEST_METHOD_NAME,
                TEST_TYPE,
                TEST_MESSAGE
            );
            Assert.AreEqual(expected, actual);
        }
    }
}
