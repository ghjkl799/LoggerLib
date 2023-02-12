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
            var logger = new TestLogger();
            logger.FixTime = DateTime.Parse(time);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);

            logger.Log(message, level);

            sb.ToString().Should().Be(expected);
        }
    }
}