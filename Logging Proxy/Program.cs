using Logger;
using System;

namespace Logging_Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new LoggingProxy<ILogger>();
            ILogger logger = new Logger.Classes.Logger();
            logger = proxy.CreateInstance(logger);

            logger.Error("error message");
            logger.Error(new Exception("exception message"));
            logger.Info("info message");
            logger.Warning("warning message");
        }
    }
}
