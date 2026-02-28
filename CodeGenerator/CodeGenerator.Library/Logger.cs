
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
        ConsoleColor? ForeColor = null, 
        ConsoleColor? BackgroundColor = null, 
        ConsoleColor? ForeColorBox = null)
    {
        string timestamp = $"[{DateTime.Now:HH:mm:ss}]";

        if (_useColors && ForeColor.HasValue && BackgroundColor.HasValue && ForeColorBox.HasValue)
        {
            Console.Write(timestamp + " ");
            Console.BackgroundColor = BackgroundColor.Value;
            Console.ForegroundColor = ForeColorBox.Value;
            Console.Write("[" + prefix + "]");
            Console.ResetColor();
            Console.ForegroundColor = ForeColor.Value;
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

    public void Info(string message) => Log(message, "INF", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);
    public void Error(string message) => Log(message, "ERR", ConsoleColor.Red,   ConsoleColor.Red, ConsoleColor.White);
    public void Warning(string message) => Log(message, "WRN", ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Black);
    public void Debug(string message) => Log(message, "DBG", ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Black);    
    public void Success(string message) => Log(message, "SUC", ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Black);
    
    
}