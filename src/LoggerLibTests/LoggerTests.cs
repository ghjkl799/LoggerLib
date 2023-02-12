using FluentAssertions;
using LoggerLib;
using LoggerLib.Writers;
using LoggerLibTests.Writers;
using System.Text;

namespace LoggerLibTests
{
    public class LoggerTests
    {
        [Theory]
        [InlineData("the message", LogLevel.INFO, "2020-01-01 00:00:01", "00:00:01 [info] the message\r\n")]
        [InlineData("a debug message", LogLevel.DEBUG, "2020-01-01 00:00:02", "00:00:02 [debug] a debug message\r\n")]
        [InlineData("an error", LogLevel.ERROR, "2020-01-01 13:14:15", "13:14:15 [error] an error\r\n")]
        public void CanFormatString(string message, LogLevel level, string time, string expected)
        {
            var (logger, sb) = CreateTestLogger();
            logger.FixTime = DateTime.Parse(time);


            logger.Log(message, level);


            sb.ToString().Should().Be(expected);
        }

        [Fact]
        public void CanWriteTwoMessages()
        {
            var (logger, sb) = CreateTestLogger();
            logger.FixTime = DateTime.Parse("2020-01-01 11:12:13");
            var expected = "11:12:13 [info] infomessage\r\n11:12:13 [error] errormessage\r\n";


            logger.Log("infomessage", LogLevel.INFO);
            logger.Log("errormessage", LogLevel.ERROR);


            sb.ToString().Should().Be(expected);
        }

        [Fact]
        public void LogDebugShouldbeDebug()
        {
            var (logger, sb) = CreateTestLogger();
            var time = "12:00:11";
            var message = "a debug message";
            var expected = $"{time} [debug] {message}{Environment.NewLine}";
            logger.FixTime = DateTime.Parse(time);


            logger.LogDebug(message);


            sb.ToString().Should().Be(expected);
        }

        [Fact]
        public void LogInfoShouldbeInfo()
        {
            var (logger, sb) = CreateTestLogger();
            var time = "12:00:11";
            var message = "an info message";
            var expected = $"{time} [info] {message}{Environment.NewLine}";
            logger.FixTime = DateTime.Parse(time);


            logger.LogInformation(message);


            sb.ToString().Should().Be(expected);
        }

        [Fact]
        public void LogErrorShouldbeInfo()
        {
            var (logger, sb) = CreateTestLogger();
            var time = "12:00:11";
            var message = "an error message";
            var expected = $"{time} [error] {message}{Environment.NewLine}";
            logger.FixTime = DateTime.Parse(time);


            logger.LogError(message);


            sb.ToString().Should().Be(expected);
        }

        private static (TestLogger logger, StringBuilder sb) CreateTestLogger()
        {
            var writer = new TestWriter();
            var logger = new TestLogger(writer);
            var sb = new StringBuilder();
            return (logger, writer.Sb);
        }
    }

    //provides seam for tests to have a fixed datetime
    public class TestLogger : Logger
    {
        public TestLogger(IWriter writer) : base(writer) { }

        public DateTime FixTime { get; set; } = DateTime.Now;

        override protected string Format(string message, LogLevel level, DateTime time)
        {
            return base.Format(message, level, FixTime);
        }
    }

}