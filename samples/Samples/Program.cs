using LoggerLib;
//basic initialisation

using (var logger = LoggerBuilder.Create().Build())
{
    logger.LogInformation("Hello world message");
}


//file logging
//directory must exists!
using (var fileLogger = LoggerBuilder.Create()
    .UseFileLogging("d:/temp/logs")
    .Build())
{
    fileLogger.LogError("An error occuresd");
}

//stream logging
using var ms = new MemoryStream();
using (var memoryLogger = LoggerBuilder.Create()
    .UseStreamLogging(ms)
    .Build())
{

    memoryLogger.LogInformation("Hello");
    memoryLogger.LogInformation("World");
}

Console.WriteLine("reading from stream:");
Console.WriteLine(System.Text.Encoding.Default.GetString(ms.ToArray()));

Console.ReadLine();
