using FluentAssertions;
using LoggerLib;
using System.Text;

namespace LoggerLibTests
{
    public class LoggerTests
    {
        [Fact]
        public void CanSendMessageToConsole()
        {
            var logger = new Logger();
            var message = "mymessage";
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);

            logger.Log(message);

            sb.ToString().Should().Be(message+Environment.NewLine);
        }
    }
}