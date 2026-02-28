using System.Text.Json;

namespace CodeGenerator.Library;

public class AppConfig
{
    public string LogDirectory { get; set; } = "log";
    
    public static AppConfig Load(string path)
    {
        if (!File.Exists(path))
            return new AppConfig(); // defaults if no config

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
    }
}