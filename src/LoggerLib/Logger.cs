using LoggerLib;
public class Logger
{
    public const string FORMAT = "{0} [{1}] {2}";

    public Logger()
    {

    }

    public void Log(string message, LogLevel level = LogLevel.INFO)
    {
        Console.WriteLine(Format(message, level, DateTime.Now));
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
