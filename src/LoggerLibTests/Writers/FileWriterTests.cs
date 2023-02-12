using FluentAssertions;
using LoggerLib.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibTests.Writers
{
    public class FileWriterTests : IDisposable
    {
        public FileWriterTests()
        {
            TryDeleteTempFile();
        }

        [Fact]
        public void ShouldWriteMessageToFile()
        {
            var path = Path.GetTempPath();
            var writer = new FileWriter(path);
            var message = "a message for file writer";
            var expected = $"{message}{Environment.NewLine}";


            writer.Write(message, LoggerLib.LogLevel.INFO);


            writer.Dispose();
            File.ReadAllText(GetTempFilePath()).Should().Be(expected);
        }

        [Fact]
        public void ShouldWriteTwoMessagesToFile()
        {
            var path = Path.GetTempPath();
            var writer = new FileWriter(path);
            var message1 = "a message for file writer";
            var message2 = "a second message for file writer";
            var expected = $"{message1}{Environment.NewLine}{message2}{Environment.NewLine}";


            writer.Write(message1, LoggerLib.LogLevel.INFO);
            writer.Write(message2, LoggerLib.LogLevel.INFO);


            writer.Dispose();
            File.ReadAllText(GetTempFilePath()).Should().Be(expected);
        }

        private string GetTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), "log.txt");
        }

        private void TryDeleteTempFile()
        {
            if (File.Exists(GetTempFilePath()))
            {
                File.Delete(GetTempFilePath());
            }
        }

        public void Dispose()
        {
            TryDeleteTempFile();
        }
    }
}
