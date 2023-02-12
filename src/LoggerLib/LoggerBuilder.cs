using LoggerLib.Writers;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LoggerLib
{
    public class LoggerBuilder
    {
        private IWriter? Writer;

        public static LoggerBuilder Create() => new();

        public LoggerBuilder UseConsoleLogging()
        {
            Writer = new ConsoleWriter();
            return this;
        }

        public LoggerBuilder UseFileLogging(string logDirectory)
        {
            Writer = new FileWriter(logDirectory);
            return this;
        }

        public LoggerBuilder UseStreamLogging(Stream stream, Encoding encoding)
        {
            Writer = new LoggerLib.Writers.StreamWriter(stream, encoding);
            return this;
        }

        public LoggerBuilder UseStreamLogging(Stream stream)
        {
            Writer = new LoggerLib.Writers.StreamWriter(stream, Encoding.Default);
            return this;
        }

        public Logger Build()
        {
            if(Writer == null)
            {
                UseConsoleLogging();
            }
            return new Logger(Writer!);
        }
    }
}
