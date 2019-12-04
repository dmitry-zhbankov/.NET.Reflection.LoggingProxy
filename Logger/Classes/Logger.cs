using System;
using System.Collections.Generic;

namespace Logger.Classes
{
    public class Logger : ILogger
    {
        private readonly ILogger _logger;
        bool disposed = false;

        public Logger()
        {
            _logger = new ConsoleLogger();
        }

        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            Console.WriteLine("BaseLogger disposer");
            if (disposed)
                return;
            if (disposing)
            {
            }
            _logger.Dispose();
            disposed = true;
        }

        ~Logger()
        {
            Console.WriteLine("BaseLogger destructor");
            Dispose(false);
        }
    }
}
