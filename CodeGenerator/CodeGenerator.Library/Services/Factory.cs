using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Library;

public class GeneratorFactory
{
    private readonly IServiceProvider _sp;

    public GeneratorFactory(IServiceProvider sp)
    {
        _sp = sp;
    }

    public IGenerator? Create(string dbType)
    {
        return dbType.ToLower() switch
        {
            "sqlite" => _sp.GetRequiredService<SQLiteGenerator>(),
            _        => null
        };
    }
}