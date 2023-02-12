using LoggerLib;
using LoggerLib.Writers;
using System.Text;

namespace LoggerLibTests.Writers
{
    internal class TestWriter : IWriter
    {
        public StringBuilder Sb { get; } = new StringBuilder();

        public string Message => Sb.ToString();

        public void Dispose()
        {
            //left blank
        }

        public Task Write(string message, LogLevel level)
        {
            Sb.AppendLine(message);
            return Task.CompletedTask;
        }
    }
}
