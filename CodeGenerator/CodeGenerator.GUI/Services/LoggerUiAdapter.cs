using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using CodeGenerator.Library.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace CodeGenerator.GUI.Services;

public class LogMessage
{
    public string Text { get; }
    public IBrush Color { get; }

    public LogMessage(string text, IBrush color)
    {
        Text = text;
        Color = color;
    }
}
public class LoggerUiAdapter : ILogger
{
    private readonly ILogger _dllLogger; 

    public ObservableCollection<LogMessage> Messages { get; } = new();

    public LoggerUiAdapter(ILogger dllLogger)
    {
        _dllLogger = dllLogger;
    }

    private void Post(string prefix, string message, IBrush color)
    {
        var timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
        var fullMessage = $"{timestamp} [{prefix}] {message}";

        Dispatcher.UIThread.Post(() =>
        {
            Messages.Add(new LogMessage(fullMessage, color));
        });
    }

    public void Debug(string message)
    {
        _dllLogger.Debug(message);
        Post("DBG", message, Brushes.Gray);
    }

    public void Info(string message)
    {
        _dllLogger.Info(message);
        Post("INF", message, Brushes.White);
    }

    public void Warning(string message)
    {
        _dllLogger.Warning(message);
        Post("WRN", message, Brushes.Yellow);
    }

    public void Error(string message)
    {
        _dllLogger.Error(message);
        Post("ERR", message, Brushes.Red);
    }

    public void Success(string message)
    {
        _dllLogger.Success(message);
        Post("SUC", message, Brushes.Green);
    }
}