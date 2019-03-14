using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupDocsPoC.Logger
{
    class Logger : ILogger
    {
        private const string ErrorLogNameTemplate = "error-conv_time-{0}.log";
        private const string LogFileNameTemplate = "log-conv_time-{0}.csv";

        private readonly string InfoLog;
        private readonly string ErrorLog;

        private static void SetLogsPath(string logFolderPath, string time, out string logFilePath, out string errorLogPath)
        {
            var logFileName = string.Format(LogFileNameTemplate, time);
            logFilePath = Path.Combine(logFolderPath, logFileName);

            var errorFileName = string.Format(ErrorLogNameTemplate, time);
            errorLogPath = Path.Combine(logFolderPath, errorFileName);
        }
        
        private void AddHeaders()
        {
            var logInfoHeader = "Time, File, Conversion, Render, Resize, Total, Status";
            File.AppendAllLines(InfoLog, new[] { logInfoHeader });
        }

        public Logger(string logFolderPath, string startTime)
        {
            SetLogsPath(logFolderPath, startTime, out string logPath, out string errorLogPath);
            InfoLog = logPath;
            ErrorLog = errorLogPath;
            AddHeaders();
        }
        public void LogError(string message)
        {
            AddToLog(ErrorLog, message);
        }

        public void LogInfo(string message)
        {
            AddToLog(InfoLog, message);
        }

        private void AddToLog(string logPath, string message)
        {
            var entry = $"{DateTime.Now}, {message}";
            File.AppendAllLines(logPath, new[] { entry });
        }
    }
}
