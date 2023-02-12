namespace LoggerLib.Writers;

public class FileWriter : IWriter
{
    private bool disposedValue;
    private int RollIndex = 1;
    private System.IO.StreamWriter SW { get; set; }

    public const int ROLL_FILE_SIZE_BYTES = 5000;

    public string Path { get; }

    public string LogFilePath(int? rollIndex = null)
    {
        return System.IO.Path.Combine(Path, rollIndex.HasValue ? $"log.{rollIndex}.txt" : "log.txt");
    }

    /// <summary>
    /// The destination directory must exists and has to be writeble.
    /// </summary>
    /// <param name="path">Path of the folder to write logfiles to</param>
    public FileWriter(string path)
    {
        Path = path;
        SW = CreateWriter();
    }

    private System.IO.StreamWriter CreateWriter()
    {
        return new(LogFilePath(), append: true);
    }

    public async Task Write(string message, LogLevel level)
    {
        await SW.WriteLineAsync(message).ConfigureAwait(false);
        await SW.FlushAsync().ConfigureAwait(false);
        await RollIfNeeded().ConfigureAwait(false);
    }


    private async Task RollIfNeeded()
    {
        //possible issue with recreating the filewrite and overwriting existing archived files
        //refactor to a strategy if things get more complex
        if (SW.BaseStream.Position >= ROLL_FILE_SIZE_BYTES)
        {
            await SW.DisposeAsync().ConfigureAwait(false);
            File.Move(LogFilePath(), LogFilePath(RollIndex), true);
            SW = CreateWriter();
            RollIndex++;
        }
    }

    private

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                SW.Dispose();
            }
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
