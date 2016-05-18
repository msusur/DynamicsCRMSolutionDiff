using System;

namespace DiffTool.Services
{
    public class LogService
    {
        public void Info(string message)
        {
            SetColor(ConsoleColor.Green);
            Write(message);
        }

        public void Message(string message)
        {
            SetColor(ConsoleColor.White);
            Write(message);
        }

        public void Error(string message, Exception exception = null)
        {
            SetColor(ConsoleColor.Red);
            Write(message);
            if (exception != null)
            {
                Write($"Error details:\r\n{exception.StackTrace}");
            }
        }

        public void Warn(string message)
        {
            SetColor(ConsoleColor.Yellow);
            Write(message);
        }

        private void Write(string message)
        {
            Console.WriteLine($"** {message}");
            Console.WriteLine();
        }

        private void SetColor(ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
        }

        public void Append(string message)
        {
            Console.Write(message);
        }
    }
}
