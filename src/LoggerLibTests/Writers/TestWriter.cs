using LoggerLib.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibTests.Writers
{
    internal class TestWriter : IWriter
    {
        public StringBuilder Sb { get; } = new StringBuilder();

        public string Message => Sb.ToString();

        public void Write(string message)
        {
            Sb.AppendLine(message);
        }
    }
}
