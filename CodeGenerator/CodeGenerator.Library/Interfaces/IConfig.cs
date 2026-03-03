namespace CodeGenerator.Library.Interfaces;

public interface IConfig
{
    string LogDirectory { get; }
    string ConnectionString { get; }
    string GeneratorOutputPath { get; }
    string DbType { get; }
    string Namespace { get; }
}