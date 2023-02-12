using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibTests.Writers
{
    public class StreamWriterTests
    {
        [Fact]
        public void ShouldWriteMessageToStream()
        {
            using var ms = new MemoryStream();
            var writer = new LoggerLib.Writers.StreamWriter(ms, Encoding.Default);
            var message = "test message on stream";
            var expected = $"{message}{Environment.NewLine}";


            writer.Write(message, LoggerLib.LogLevel.INFO);
            writer.Flush();


            Encoding.Default.GetString(ms.ToArray()).Should().Be(expected);
        }
    }
}
