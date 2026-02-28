
using System;

namespace CodeGenerator.Library;

public class Logger
{
    private readonly bool _useColors;
    
    public Logger(bool useColors = true)
    {
        _useColors = useColors;
    }

    public void Log(string message, string prefix, 
        ConsoleColor? foreColor = null, 
        ConsoleColor? backgroundColor = null, 
        ConsoleColor? foreColorBox = null)
    {
        string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";

        if (_useColors && foreColor.HasValue && backgroundColor.HasValue && foreColorBox.HasValue)
        {
            Console.Write(timestamp + " ");
            Console.BackgroundColor = backgroundColor.Value;
            Console.ForegroundColor = foreColorBox.Value;
            Console.Write("[" + prefix + "]");
            Console.ResetColor();
            Console.ForegroundColor = foreColor.Value;
            Console.Write(" " + message);
            Console.ResetColor();
            Console.WriteLine();
        }
        else
        {
            Console.Write(timestamp + " ");
            Console.Write("[" + prefix + "]");
            Console.Write(" " + message);
            Console.WriteLine();
        }
    }

    public void Info(string message) => Log(message, Constants.Info, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);
    public void Error(string message) => Log(message, Constants.Error, ConsoleColor.Red,   ConsoleColor.Red, ConsoleColor.White);
    public void Warning(string message) => Log(message, Constants.Warning, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Black);
    public void Debug(string message) => Log(message, Constants.Debug, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Black);    
    public void Success(string message) => Log(message, Constants.Success, ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Black);
    
    
}