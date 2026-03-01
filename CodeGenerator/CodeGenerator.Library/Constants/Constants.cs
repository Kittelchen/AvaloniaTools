namespace CodeGenerator.Library;

public class Constants
{
    // Constants for Logger prefixes
    
    public static readonly string Debug = "DBG";
    public static readonly string Error = "ERR";
    public static readonly string Info = "INF";
    public static readonly string Warning = "WRN";
    public static readonly string Success = "SUC";

    public static readonly string OpenBracket = "[";
    public static readonly string CloseBracket = "]";
    
    public static readonly string DefaultLogFolder = "./log/";

    public static readonly string GetSQLiteTables =
        "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
}