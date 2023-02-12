using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib.Writers
{
    public class FileWriter : IWriter
    {
        private bool disposedValue;

        public string Path { get; }
        private System.IO.StreamWriter SW { get; }

        /// <summary>
        /// The destination directory must exists and has to be writeble.
        /// </summary>
        /// <param name="path">Path of the folder to write logfiles to</param>
        public FileWriter(string path)
        {
            Path = path;
            SW = new(System.IO.Path.Combine(path, "log.txt"), append: true);
        }

        public void Write(string message, LogLevel level)
        {
            SW.WriteLine(message);
        }

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
}
