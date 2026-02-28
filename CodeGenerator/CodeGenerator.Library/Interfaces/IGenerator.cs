namespace CodeGenerator.Library;

public interface IGenerator
{
    void Initialize(AppConfig config, Logger logger);
    bool Execute();
}