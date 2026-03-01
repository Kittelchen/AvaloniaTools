namespace CodeGenerator.Library.Core;

public static class Constants
{
    // Constants for Logger prefixes
    
    public const string Debug = "DBG";
    public const string Error = "ERR";
    public const string Info = "INF";
    public const string Warning = "WRN";
    public const string Success = "SUC";

    public const string OpenBracket = "[";
    public const string CloseBracket = "]";
    
    public const string DefaultLogFolder = "./log/";

    public const string GetSqLiteTables =
        "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
}