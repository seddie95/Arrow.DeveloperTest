using System;

namespace Arrow.DeveloperTest.Services
{
    public interface ILogger
    {
        void Error(Exception exception);
        void Error(string message, Exception exception);
        void Info(string message);
        void Log(string message);
        void Warn(string message);
    }
}