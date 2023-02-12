namespace LoggerLib
{
    public class Logger
    {
        public Logger()
        {

        }

        public void Log(string mymessage, LogLevel level = LogLevel.INFO)
        {
            Console.WriteLine(mymessage);
        }
    }
}