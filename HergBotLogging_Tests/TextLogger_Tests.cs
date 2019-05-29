using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HergBotLogging;
using NUnit.Framework;

namespace HergBotLogging_Tests
{
    public class TextLogger_Tests
    {
        private const string DEFAULT_CONFIG_PATH = "./Config/Default.config";

        private const string DEFAULT_LOG_FILE_NAME = "DefaultTestLog";

        private const string ALTERNATE_CONFIG_PATH = "./Config/Alternate.config";

        private const string ALTERNATE_LOG_FILE_NAME = "AlternateTestLog";

        private const string TEST_LOG_DIRECTORY = "./Logs/";

        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new TextLogger(DEFAULT_CONFIG_PATH);
        }

        [Test]
        public void LoadConfiguration_Null()
        {
            ILogger _testLogger = new TextLogger(null);
            
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
