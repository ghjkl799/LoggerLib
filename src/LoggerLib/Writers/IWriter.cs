namespace LoggerLib.Writers;

public interface IWriter : IDisposable
{
    Task Write(string message, LogLevel level);
}
