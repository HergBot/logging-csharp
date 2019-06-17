/*
* PROJECT: HergBot Logging Tests
* PROGRAMMER: Justin
* FIRST VERSION: 16/06/2019
*/

using System;
using System.IO;
using System.Xml;

using NUnit.Framework;

using HergBot.Logging.Configuration;
using HergBot.Logging_Tests.Data;

namespace HergBot.Logging_Tests
{
    public class LoggingConfiguration_Tests
    {
        private const string DEFAULT_LOG_FILE_NAME = "HergBotLog";

        private const string PRIMARY_LOG_FILE_NAME = "PrimaryTestLog";

        private const string ALTERNATE_LOG_FILE_NAME = "AlternateTestLog";

        private const string TEST_LOG_DIRECTORY = "./logs/";

        private const string DEBUG_LABEL = "Debug";

        private const string ERROR_LABEL = "Error";

        private const string EXCEPTION_LABEL = "Exception";

        private const string INFO_LABEL = "Information";

        private const string WARNING_LABEL = "Warning";

        private const string UNKNOWN_KEY = "UNKNOWN";

        private const string UNKNOWN_LABEL = "Unknown";

        private const string MISSING_CONFIG_FILE = "./FileDoesNot.Exist";

        private const string MISSING_CONFIG_LABEL = "???";

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
            Assert.AreEqual(DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_PrimaryFromString()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.PrimaryConfig);
            Assert.AreEqual(TEST_LOG_DIRECTORY, _configuration.LogDirectory);
            Assert.AreEqual(PRIMARY_LOG_FILE_NAME, _configuration.BaseFileName);
            Assert.IsTrue(_configuration.IsDebugEnabled);
            Assert.AreEqual(DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_AlternateFromString()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.AlternativeConfig);
            Assert.AreEqual(TEST_LOG_DIRECTORY, _configuration.LogDirectory);
            Assert.AreEqual(ALTERNATE_LOG_FILE_NAME, _configuration.BaseFileName);
            Assert.IsFalse(_configuration.IsDebugEnabled);
            Assert.AreEqual(DEBUG_LABEL, _configuration.DebugLabel);
            Assert.IsTrue(_configuration.IsErrorEnabled);
            Assert.AreEqual(ERROR_LABEL, _configuration.ErrorLabel);
            Assert.IsTrue(_configuration.IsExceptionEnabled);
            Assert.AreEqual(EXCEPTION_LABEL, _configuration.ExceptionLabel);
            Assert.IsTrue(_configuration.IsInfoEnabled);
            Assert.AreEqual(INFO_LABEL, _configuration.InfoLabel);
            Assert.IsTrue(_configuration.IsWarningEnabled);
            Assert.AreEqual(WARNING_LABEL, _configuration.WarningLabel);
        }

        [Test]
        public void LoadConfiguration_MissingRoot()
        {
            Assert.Throws<XmlException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString(_testData.MissingRootConfig);
            });
        }

        [Test]
        public void LoadConfiguration_MissingAttribute()
        {
            Assert.Throws<FormatException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString(_testData.MissingAttributeConfig);
            });
        }

        [Test]
        public void LoadConfiguration_MissingElement()
        {
            Assert.Throws<FormatException>(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString(_testData.MissingElementConfig);
            });
        }

        [Test]
        public void LoadConfiguration_UnknownLoggingType()
        {
            Assert.DoesNotThrow(() =>
            {
                _configuration = LoggingConfiguration.LoadFromString(_testData.UnknownLoggingTypeConfig);
            });
        }

        [Test]
        public void IsTypeEnabled_MissingDefaultType()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.MissingDebugLoggingTypeConfig);
            Assert.IsFalse(_configuration.IsDebugEnabled);
        }

        [Test]
        public void IsTypeEnabled_UnknownLoggingType()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.UnknownLoggingTypeConfig);
            Assert.IsTrue(_configuration.IsDebugEnabled);
        }

        [Test]
        public void GetTypeLabel_MissingDefaultType()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.MissingDebugLoggingTypeConfig);
            Assert.AreEqual(MISSING_CONFIG_LABEL, _configuration.DebugLabel);
        }

        [Test]
        public void GetTypeLabel_UnknownLoggingType()
        {
            _configuration = LoggingConfiguration.LoadFromString(_testData.UnknownLoggingTypeConfig);
            Assert.AreEqual(UNKNOWN_LABEL, _configuration.GetTypeLabel(UNKNOWN_KEY));
        }

        [Test]
        public void AddLoggingType_New()
        {
            _configuration = new LoggingConfiguration();
            LoggingType newType = new LoggingType(true, "SomeLabel");
            Assert.IsTrue(_configuration.AddLoggingType("SomeKey", newType));
        }

        [Test]
        public void AddLoggingType_AlreadyExists()
        {
            _configuration = new LoggingConfiguration();
            LoggingType newType = new LoggingType(true, "SomeLabel");
            _configuration.AddLoggingType("SomeKey", newType);
            Assert.IsFalse(_configuration.AddLoggingType("SomeKey", newType));
        }
    }
}
