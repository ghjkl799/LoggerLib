using FluentAssertions;
using LoggerLib;
using LoggerLib.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibTests.Writers
{
    public class ConsoleWriterTests
    {
        [Fact]
        public void CanWriteShortMessage()
        {
            var writer = CreateTestConsoleWriter();


            writer.Write("a short message", LogLevel.INFO);
        }

        [Fact]
        public void CanWriteMaxLengthMessage()
        {
            var writer = CreateTestConsoleWriter();

            writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH), LogLevel.INFO);
        }

        [Fact]
        public void ShouldThrowExceptionForLongMessage()
        {
            var writer = CreateTestConsoleWriter();


            var act = () => writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH + 1), LogLevel.INFO);


            act.Should().Throw<MessageTooLongException>();
        }

        [Theory]
        [InlineData(LogLevel.INFO, ConsoleColor.Green)]
        [InlineData(LogLevel.DEBUG, ConsoleColor.Gray)]
        [InlineData(LogLevel.ERROR, ConsoleColor.Red)]
        public void LogLevelToColor(LogLevel level, ConsoleColor color)
        {
            ConsoleWriter.LevelToColor(level).Should().Be(color);
        }

        private ConsoleWriter CreateTestConsoleWriter()
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);
            return new ConsoleWriter();
        }
    }
}
