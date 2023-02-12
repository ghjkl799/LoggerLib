using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib
{
    public class LoggerException : Exception
    {
        public LoggerException()
        {
        }

        public LoggerException(string? message) : base(message)
        {
        }

        public LoggerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LoggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class ConsoleWriterException : LoggerException
    {
        public ConsoleWriterException()
        {
        }

        public ConsoleWriterException(string? message) : base(message)
        {
        }

        public ConsoleWriterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ConsoleWriterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class MessageTooLongException : ConsoleWriterException
    {
        public MessageTooLongException()
        {
        }

        public MessageTooLongException(string? message) : base(message)
        {
        }

        public MessageTooLongException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MessageTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
