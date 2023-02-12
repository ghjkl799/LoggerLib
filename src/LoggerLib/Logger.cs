using LoggerLib;
using LoggerLib.Writers;

public class Logger
{
    public const string FORMAT = "{0} [{1}] {2}";
    private IWriter Writer { get; }

    public Logger(IWriter writer)
    {
        Writer = writer;
    }

    public void LogInformation(string message) => Log(message, LogLevel.INFO);
    public void LogDebug(string message) => Log(message, LogLevel.DEBUG);
    public void LogError(string message) => Log(message, LogLevel.ERROR);


    public void Log(string message, LogLevel level = LogLevel.INFO)
    {
        Writer.Write(Format(message, level, DateTime.Now), level);
    }

    protected virtual string Format(string message, LogLevel level, DateTime time)
    {
        return string.Format(FORMAT, time.ToString("HH:mm:ss"), LevelString(level), message);
    }

    private string LevelString(LogLevel level) => level switch
    {
        LogLevel.INFO => "info",
        LogLevel.DEBUG => "debug",
        LogLevel.ERROR => "error",
        _ => level.ToString(),
    };
}
