using LoggerLib;

namespace LoggerLibTests
{
    public class LoggerTests
    {
        [Fact]
        public void CanSendMessage()
        {
            var logger = new Logger();
            var message = "mymessage";

            logger.Log(message);
        }
    }
}