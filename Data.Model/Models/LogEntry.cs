namespace Data.Model.Models;

public class LogEntry
{
    public int Id { get; set; }
    public string Timestamp { get; set; } = "";
    public string Prefix { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "";
}