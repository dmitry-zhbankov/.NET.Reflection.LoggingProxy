using System;

namespace Logger.Classes
{
    public class ConsoleLogger : ILogger
    {
        private readonly ILogger _logger;
        private bool disposed = false;

        public ConsoleLogger()
        {
        }

        public ConsoleLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Error(string message)
        {
            Console.WriteLine($"Log {DateTime.Now:s} Error: {message}");
            _logger?.Error(message);
        }

        public void Error(Exception ex)
        {
            Console.WriteLine($"Log {DateTime.Now:s} Error exception: {ex?.Message}");
            _logger?.Error(ex);
        }

        public void Info(string message)
        {
            Console.WriteLine($"Log {DateTime.Now:s} Info: {message}");
            _logger?.Info(message);
        }

        public void Warning(string message)
        {
            Console.WriteLine($"Log {DateTime.Now:s} Warning: {message}");
            _logger?.Warning(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("DbLogger disposer");
            if (disposed)
                return;
            if (disposing)
            {
            }
            _logger?.Dispose();
        }

        ~ConsoleLogger()
        {
            Dispose(false);
        }
    }
}
