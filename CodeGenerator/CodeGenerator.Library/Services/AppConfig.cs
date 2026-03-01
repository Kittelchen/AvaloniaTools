using Newtonsoft.Json;

namespace CodeGenerator.Library;

public class AppConfig : IConfig
{
    public string LogDirectory { get; set; } = "log";
    public string ConnectionString { get; set; } = string.Empty;
    public string GeneratorOutputPath { get; set; } = string.Empty;
    public string DbType { get; set; } = string.Empty;
    
    public static AppConfig Load()
    {
        string exeFolder = AppContext.BaseDirectory;
        string configPath = Path.Combine(exeFolder, "config.json");
        
        if (!File.Exists(configPath))
            return new AppConfig(); // defaults if no config

        try
        {
            string json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<AppConfig>(json) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Config Error] Failed to load {configPath}: {ex.Message}");
            return new AppConfig();
        }
    }
}