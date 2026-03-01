namespace CodeGenerator.Library;

public interface ILogger
{
    public void Log(string message, string prefix,
        ConsoleColor? foreColor = null,
        ConsoleColor? backgroundColor = null,
        ConsoleColor? foreColorBox = null);
    void Info(string message);
    void Warning(string message);
    void Error(string message);
    void Debug(string message);
    void Success(string message);
}