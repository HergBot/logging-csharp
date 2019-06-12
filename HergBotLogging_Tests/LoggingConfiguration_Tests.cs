using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HergBotLogging;
using NUnit.Framework;
using Moq;
using System.IO;
using HergBotLogging_Tests.Data;

namespace HergBotLogging_Tests
{
    public class LoggingConfiguration_Tests
    {
        private const string DEFAULT_LOG_FILE_NAME = "HergBotLog";

        private const string PRIMARY_LOG_FILE_NAME = "PrimaryTestLog";

        private const string ALTERNATE_LOG_FILE_NAME = "AlternateTestLog";

        private const string TEST_LOG_DIRECTORY = "./logs/";

        private const string DEFAULT_DEBUG_LABEL = "Debug";

        private const string DEFAULT_ERROR_LABEL = "Error";

        private const string DEFAULT_EXCEPTION_LABEL = "Exception";

        private const string DEFAULT_INFO_LABEL = "Info";

        private const string DEFAULT_WARNING_LABEL = "Warning";

        private const string LOADED_DEBUG_LABEL = "Debug";

        private const string LOADED_ERROR_LABEL = "Error";

        private const string LOADED_EXCEPTION_LABEL = "Exception";

        private const string LOADED_INFO_LABEL = "Information";

        private const string LOADED_WARNING_LABEL = "Warning";

        private const string MISSING_CONFIG_FILE = "./FileDoesNot.Exist";

        private LoggingConfiguration _configuration;

        private ConfigTestData _testData;

        [SetUp]
        public void SetUp()
        {
            _testData = new ConfigTestData();
        }

        [Test]
        public void LoadConfiguration_NullFromFile()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromFile(null);
            });
        }

        [Test]
        public void LoadConfiguration_NullFromString()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString(null);
            });
        }

        [Test]
        public void LoadConfiguration_MissingConfigFile()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromFile(MISSING_CONFIG_FILE);
            });
        }

        [Test]
        public void LoadConfiguration_EmptyString()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString("");
            });
        }

        [Test]
        public void LoadConfiguration_Default()
        {
            _configuration = new LoggingConfiguration();
            Assert.AreEqual(TEST_LOG_DIRECTORY, _configuration.LogDirectory);
            Assert.AreEqual(DEFAULT_LOG_FILE_NAME, _configuration.BaseFileName);
            Assert.IsTrue(_configuration.IsDebugEnabled);
            Assert.AreEqual(DEFAULT_DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(DEFAULT_ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(DEFAULT_EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(DEFAULT_INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(DEFAULT_WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_PrimaryFromString()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.PrimaryConfig);
            Assert.AreEqual(TEST_LOG_DIRECTORY, _configuration.LogDirectory);
            Assert.AreEqual(PRIMARY_LOG_FILE_NAME, _configuration.BaseFileName);
            Assert.IsTrue(_configuration.IsDebugEnabled);
            Assert.AreEqual(LOADED_DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(LOADED_ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(LOADED_EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(LOADED_INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(LOADED_WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_AlternateFromString()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.AlternativeConfig);
            Assert.AreEqual(TEST_LOG_DIRECTORY, _configuration.LogDirectory);
            Assert.AreEqual(ALTERNATE_LOG_FILE_NAME, _configuration.BaseFileName);
            Assert.IsFalse(_configuration.IsDebugEnabled);
            Assert.AreEqual(LOADED_DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(LOADED_ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(LOADED_EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(LOADED_INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(LOADED_WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_MissingAttribute()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.MissingAttributeConfig);
        }

        [Test]
        public void LoadConfiguration_MissingElement()
        {

        }

        [Test]
        public void LoadConfiguration_UnknownLoggingType()
        {

        }
    }
}
