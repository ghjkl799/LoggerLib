using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
