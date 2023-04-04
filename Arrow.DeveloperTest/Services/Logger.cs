using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Services
{
    public class Logger : ILogger
    {

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(Exception exception)
        {
            Error("", exception);
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine(message + "\n" + "An error occured with Exception: " + exception.Message);

        }

        public void Info(string message)
        {
            Console.WriteLine("Info message: " + message);
        }

        public void Warn(string message)
        {
            Console.WriteLine("Warning message: " + message);
        }
    }
}
