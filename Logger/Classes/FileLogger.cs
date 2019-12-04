using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Logger
{
    public class FileLogger : ILogger
    {
        private static StreamWriter streamWriter;
        private bool disposed;
        private static FileLogger instance;
        private ILogger _logger;

        private FileLogger()
        {
            var fileName = $"Log {DateTime.Now:yyyy'-'MM'-'dd'T'HH'-'mm'-'ss}";
            FileStream fileStream = File.Open(fileName, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
        }

        private FileLogger(ILogger logger) : this()
        {
            _logger = logger;
        }

        public static FileLogger GetInstance(ILogger logger = null)
        {
            if (instance != null)
            {
                if (logger == null)
                {
                    instance._logger = null;
                }
                return instance;
            }
            instance = new FileLogger(logger);
            return instance;
        }

        public void Error(string message)
        {
            streamWriter.WriteLine($"Error: {message}");
            streamWriter.Flush();
            _logger?.Error(message);
        }

        public void Error(Exception ex)
        {
            streamWriter.WriteLine($"Error exception: {ex?.Message}");
            streamWriter.Flush();
            _logger?.Error(ex);
        }

        public void Info(string message)
        {
            streamWriter.WriteLine($"Info: {message}");
            streamWriter.Flush();
            _logger?.Info(message);
        }

        public void Warning(string message)
        {
            streamWriter.WriteLine($"Warning: {message}");
            streamWriter.Flush();
            _logger?.Warning(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            Console.WriteLine("FileLogger disposer");
            if (disposed)
                return;
            if (disposing)
            {
            }
            streamWriter?.Close();
            streamWriter?.Dispose();
            _logger?.Dispose();
            disposed = true;
        }

        ~FileLogger()
        {
            Console.WriteLine("FileLogger destructor");
            Dispose(false);
        }
    }
}
