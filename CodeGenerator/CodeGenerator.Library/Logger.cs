
using System;

namespace CodeGenerator.Library;

public class Logger
{
    private readonly bool _useColors;
    private readonly string _logFilePath;
    
    public Logger(bool useColors = true, string? logDirectory = null)
    {
        _useColors = useColors;

        string dir = string.IsNullOrEmpty(logDirectory) ? Constants.DefaultLogFolder : logDirectory;

        Directory.CreateDirectory(dir);
        _logFilePath = Path.Combine(dir, $"{DateTime.Now:yyyy-MM-dd}_CodeGenerator.log");
        
    }

    public void Log(string message, string prefix, 
        ConsoleColor? foreColor = null, 
        ConsoleColor? backgroundColor = null, 
        ConsoleColor? foreColorBox = null)
    {
        string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
        string output = $"{timestamp} {Constants.Open_Bracket}{prefix}{Constants.Close_Bracket} {message}";

        if (_useColors && foreColor.HasValue && backgroundColor.HasValue && foreColorBox.HasValue)
        {
            Console.Write(timestamp + " ");
            Console.BackgroundColor = backgroundColor.Value;
            Console.ForegroundColor = foreColorBox.Value;
            Console.Write(Constants.Open_Bracket + prefix + Constants.Close_Bracket);
            Console.ResetColor();
            Console.ForegroundColor = foreColor.Value;
            Console.Write(" " + message);
            Console.ResetColor();
            Console.WriteLine();
        }
        else
        {
            Console.Write(timestamp + " ");
            Console.Write(Constants.Open_Bracket + prefix + Constants.Close_Bracket);
            Console.Write(" " + message);
            Console.WriteLine();
        }
        
        try
        {
            File.AppendAllText(_logFilePath, output + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Logger Error] Could not write to file: {ex.Message}");
        }
    }

    public void Info(string message) => Log(message, Constants.Info, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);
    public void Error(string message) => Log(message, Constants.Error, ConsoleColor.Red,   ConsoleColor.Red, ConsoleColor.White);
    public void Warning(string message) => Log(message, Constants.Warning, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Black);
    public void Debug(string message) => Log(message, Constants.Debug, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Black);    
    public void Success(string message) => Log(message, Constants.Success, ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Black);
    
    
}