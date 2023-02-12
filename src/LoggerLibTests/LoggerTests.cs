using FluentAssertions;
using LoggerLib;
using System.Text;

namespace LoggerLibTests
{
    public class TestLogger : Logger
    {
        public DateTime FixTime { get; set; } = DateTime.Now;

        override protected string Format(string message, LogLevel level, DateTime time)
        {
            return base.Format(message, level, FixTime);
        }
    }

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

        public void CanWriteTwoMessages()
        {
            var (logger, sb) = CreateTestLogger();
            logger.FixTime = DateTime.Parse("2020-01-01 11:12:13");
            var expected = "11:12:13 [info] infomessage\r\n11:12:13 [error] errormessage\r\n";


            logger.Log("infomessage", LogLevel.INFO);
            logger.Log("errormessage", LogLevel.ERROR);


            sb.ToString().Should().Be(expected);
        }

        private static (TestLogger, StringBuilder) CreateTestLogger()
        {
            var logger = new TestLogger();
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);
            return (logger, sb);
        }
    }
}