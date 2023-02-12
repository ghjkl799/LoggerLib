namespace LoggerLib.Writers;
public class ConsoleWriter : IWriter
{
    public const int MAX_MESSAGE_LENGTH = 1000;

    public async Task Write(string message, LogLevel level)
    {
        if (message != null && message.Length > MAX_MESSAGE_LENGTH)
        {
            throw new MessageTooLongException($"Message is too long, actual length: {message.Length}, max length: {MAX_MESSAGE_LENGTH}.");
        }
        SetColor(level);
        await Console.Out.WriteLineAsync(message).ConfigureAwait(false);
    }

    public static ConsoleColor LevelToColor(LogLevel level) => level switch
    {
        LogLevel.ERROR => ConsoleColor.Red,
        LogLevel.DEBUG => ConsoleColor.Gray,
        LogLevel.INFO => ConsoleColor.Green,
        _ => ConsoleColor.Green,
    };

    protected void SetColor(LogLevel level)
    {
        Console.ForegroundColor = LevelToColor(level);
    }

    public void Dispose()
    {
        //left blank
    }
}
