using System.Text;

namespace LoggerLib.Writers;

//adapter for System.IO.StreamWriter
public class StreamWriter : IWriter
{
    private bool disposedValue;

    private System.IO.StreamWriter SW { get; }

    public StreamWriter(Stream stream, Encoding encoding)
    {
        SW = new System.IO.StreamWriter(stream, encoding);
    }

    public Task Write(string message, LogLevel level)
    {
        return SW.WriteLineAsync(message);
    }

    public void Flush()
    {
        SW.Flush();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Flush();
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
