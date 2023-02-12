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
        [Fact]
        public void CanFormatString()
        {
            var logger = new TestLogger();
            logger.FixTime = DateTime.Parse("2020-01-01 11:12:13");
            var message = "mymessage";
            var expected = $"11:12:13 [info] {message}"+Environment.NewLine;
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);

            logger.Log(message, LogLevel.INFO);

            sb.ToString().Should().Be(expected);
        }
    }
}