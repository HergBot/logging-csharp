using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HergBotLogging;
using NUnit.Framework;
using Moq;

namespace HergBotLogging_Tests
{
    public class TextLogger_Tests
    {
        private const string DEFAULT_LOG_FILE_NAME = "DefaultTestLog";

        private const string ALTERNATE_LOG_FILE_NAME = "AlternateTestLog";

        private const string TEST_LOG_DIRECTORY = "./Logs/";

        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void LoadConfiguration_Null()
        {
            
        }

        [Test]
        public void LoadConfiguration_Default()
        {

        }

        [Test]
        public void LoadConfiguration_Alternate()
        {

        }

        [Test]
        public void LoadConfiguration_MissingConfigFile()
        {

        }

        [Test]
        public void LoadConfiguration_EmptyFile()
        {

        }

        [Test]
        public void LoadConfiguration_MissingAttribute()
        {

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
