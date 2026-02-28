using Newtonsoft.Json;

namespace CodeGenerator.Library;

public class AppConfig
{
    public string LogDirectory { get; set; } = "log";
    
    public static AppConfig Load(string path)
    {
        if (!File.Exists(path))
            return new AppConfig(); // defaults if no config

        try
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<AppConfig>(json) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Config Error] Failed to load {path}: {ex.Message}");
            return new AppConfig();
        }
    }
}