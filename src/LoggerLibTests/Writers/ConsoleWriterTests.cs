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

            writer.Write("a short message");
        }

        [Fact]
        public void CanWriteMaxLengthMessage()
        {
            var writer = CreateTestConsoleWriter();

            writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH));
        }

        [Fact]
        public void ShouldThrowExceptionForLongMessage()
        {
            var writer = CreateTestConsoleWriter();

            var act = () => writer.Write(new string('x', ConsoleWriter.MAX_MESSAGE_LENGTH + 1));

            act.Should().Throw<MessageTooLongException>();
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
