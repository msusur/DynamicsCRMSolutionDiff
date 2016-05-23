using System;
using System.Text;

namespace DiffTool.Services
{
    public class LogService
    {
        private readonly StringBuilder _logBuilder = new StringBuilder();
        private readonly IFileHelper _fileHelper;

        public LogService(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public LogService()
            : this(new FileHelper())
        {

        }

        public void Info(string message)
        {
            SetColor(ConsoleColor.Green);
            Write(message);
        }

        public void Message(string message, ConsoleColor color = ConsoleColor.White)
        {
            SetColor(color);
            Console.WriteLine(message);
            LogOnly(message);
        }

        public void Error(string message, Exception exception = null)
        {
            SetColor(ConsoleColor.Red);
            Write($"ERROR: {message}");
            if (exception != null)
            {
                LogOnly($"Error details:\r\n{exception.StackTrace}");
            }
        }

        public void Warn(string message)
        {
            SetColor(ConsoleColor.Yellow);
            Write($"WARNING: {message}");
        }

        private void LogOnly(string message)
        {
            _logBuilder.AppendLine(message);
        }

        private void Write(string message)
        {
            message = $"@{DateTime.Now.ToShortTimeString()}-{message}";
            Console.WriteLine(message);
            Console.WriteLine();
            LogOnly(message);
        }

        private void SetColor(ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
        }

        public void Flush(string logFilePath)
        {
            if (string.IsNullOrEmpty(logFilePath))
            {
                return;
            }
            _fileHelper.SaveToFile(logFilePath, _logBuilder.ToString());
        }
    }
}
