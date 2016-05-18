using System;
using System.Diagnostics.Eventing;
using System.Drawing;

namespace DiffTool.Services
{
    public class LogService
    {
        public void Info(string message)
        {
            SetColor(ConsoleColor.Blue);

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
            Console.WriteLine(message);
        }

        private void SetColor(ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
        }
    }
}
