using System;
using System.IO;
using System.Reflection;
using Common.Extensions;
using log4net;
using log4net.Config;

namespace CodeGenerator.Library
{
    public class Logger : ILogger
    {
        private static readonly ILog _log;

        static Logger()
        {
            string dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configPath = Path.Combine(dllFolder, "log4net.config");

            var repo = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(repo, new FileInfo(configPath));

            _log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(Logger));
        }

        private void LogWithColors(string prefix, string message, ConsoleColor boxColor, ConsoleColor textColor)
        {
            string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";

            // Save console colors
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            // Print timestamp
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(timestamp + " ");

            // Print box
            Console.BackgroundColor = boxColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"[{prefix}]");

            // Print message
            Console.BackgroundColor = oldBg;
            Console.ForegroundColor = textColor;
            Console.WriteLine(" " + message);

            // Reset colors
            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;

            // Always log to file via log4net
           if (prefix.IsEQ("DBG"))  _log.Debug($"[{prefix}] {message}");
           if (prefix.IsEQ("INF"))  _log.Info($"[{prefix}] {message}");
           if (prefix.IsEQ("ERR"))  _log.Error($"[{prefix}] {message}");
           if (prefix.IsEQ("WRN"))  _log.Warn($"[{prefix}] {message}");
           if (prefix.IsEQ("SUC"))  _log.Info($"[{prefix}] {message}");
        }

        public void Debug(string message) => LogWithColors("DBG", message, ConsoleColor.DarkGray, ConsoleColor.Gray);
        public void Info(string message) => LogWithColors("INF", message, ConsoleColor.White, ConsoleColor.White);
        public void Warning(string message) => LogWithColors("WRN", message, ConsoleColor.Yellow, ConsoleColor.Yellow);
        public void Error(string message) => LogWithColors("ERR", message, ConsoleColor.Red, ConsoleColor.Red);
        public void Success(string message) => LogWithColors("SUC", message, ConsoleColor.Green, ConsoleColor.Green);
    }
}