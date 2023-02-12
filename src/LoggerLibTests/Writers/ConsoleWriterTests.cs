using FluentAssertions;
using LoggerLib;
using LoggerLib.Writers;
using System.Text;

namespace LoggerLibTests.Writers
{
    public class ConsoleWriterTests
    {
        [Fact]
        public async Task CanWriteShortMessage()
        {
            var (writer, sb) = CreateTestConsoleWriter();
            var message = "a short message";
            var expected = $"{message}{Environment.NewLine}";


            await writer.Write(message, LogLevel.INFO);


            sb.ToString().Should().Be(expected);
        }

        [Fact]
        public async Task CanWriteMaxLengthMessage()
        {
            var (writer, sb) = CreateTestConsoleWriter();

            await writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH), LogLevel.INFO);
        }

        [Fact]
        public async Task ShouldThrowExceptionForLongMessage()
        {
            var (writer, sb) = CreateTestConsoleWriter();


            var act = async () => await writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH + 1), LogLevel.INFO);


            await act.Should().ThrowAsync<MessageTooLongException>();
        }

        [Theory]
        [InlineData(LogLevel.INFO, ConsoleColor.Green)]
        [InlineData(LogLevel.DEBUG, ConsoleColor.Gray)]
        [InlineData(LogLevel.ERROR, ConsoleColor.Red)]
        public void LogLevelToColor(LogLevel level, ConsoleColor color)
        {
            ConsoleWriter.LevelToColor(level).Should().Be(color);
        }

        private (ConsoleWriter writer, StringBuilder sb) CreateTestConsoleWriter()
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);
            return (new ConsoleWriter(), sb);
        }
    }
}
