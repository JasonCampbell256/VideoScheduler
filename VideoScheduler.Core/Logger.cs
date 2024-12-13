using System;
using System.IO;

namespace VideoScheduler
{
    public class Logger
    {
        private const string logFilePath = @"Logs\log.txt";

        private static void CreateLogDirectoryIfNeeded()
        {
            Directory.CreateDirectory("Logs");
        }

        public static void LogException(Exception e)
        {
            CreateLogDirectoryIfNeeded();
            string logText = $"[{DateTime.Now}] Unhandled Exception: {e.ToString()}\n";
            AppendTextToLogFile(logText);
        }

        public static void LogMessage(string message)
        {
            Directory.CreateDirectory("Logs");  // Ensure the directory exists
            string logText = $"[{DateTime.Now}]: {message}\n";
            AppendTextToLogFile(logText);
        }

        private static void AppendTextToLogFile(string logText)
        {
            File.AppendAllText(logFilePath, logText);
        }
    }
}
