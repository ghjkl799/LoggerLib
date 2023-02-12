using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib.Writers
{
    public interface IWriter : IDisposable
    {
        void Write(string message, LogLevel level);
    }
}
