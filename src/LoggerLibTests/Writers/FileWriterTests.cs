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
        public async Task ShouldWriteMessageToFile()
        {
            var path = Path.GetTempPath();
            var writer = new FileWriter(path);
            var message = "a message for file writer";
            var expected = $"{message}{Environment.NewLine}";


            await writer.Write(message, LoggerLib.LogLevel.INFO);


            writer.Dispose();
            File.ReadAllText(GetTempFilePath()).Should().Be(expected);
        }

        [Fact]
        public async Task ShouldWriteTwoMessagesToFile()
        {
            var path = Path.GetTempPath();
            var writer = new FileWriter(path);
            var message1 = "a message for file writer";
            var message2 = "a second message for file writer";
            var expected = $"{message1}{Environment.NewLine}{message2}{Environment.NewLine}";


            await writer.Write(message1, LoggerLib.LogLevel.INFO);
            await writer.Write(message2, LoggerLib.LogLevel.INFO);


            writer.Dispose();
            File.ReadAllText(GetTempFilePath()).Should().Be(expected);
        }

        [Fact]
        public async Task ShouldRollFilesOverMaxSize()
        {
            var path = Path.GetTempPath();
            var writer = new FileWriter(path);
            var message1 = new string('x', 5001);
            var message2 = new string('y', 5001);
            var message3 = "message2";


            await writer.Write(message1, LoggerLib.LogLevel.INFO);
            await writer.Write(message2, LoggerLib.LogLevel.INFO);
            await writer.Write(message3, LoggerLib.LogLevel.INFO);


            writer.Dispose();

            File.ReadAllText(GetTempFilePath()).Should().Be(message3 + Environment.NewLine);
            File.ReadAllText(GetTempFilePath("log.1.txt")).Should().Be(message1 + Environment.NewLine);
            File.ReadAllText(GetTempFilePath("log.2.txt")).Should().Be(message2 + Environment.NewLine);
            TryDeleteTempFile("log.1.txt");
            TryDeleteTempFile("log.2.txt");
        }

        private string GetTempFilePath(string filename = "log.txt")
        {
            return Path.Combine(Path.GetTempPath(), filename);
        }

        private void TryDeleteTempFile(string filename = "log.txt")
        {
            if (File.Exists(GetTempFilePath(filename)))
            {
                File.Delete(GetTempFilePath(filename));
            }
        }

        public void Dispose()
        {
            TryDeleteTempFile();
        }
    }
}
