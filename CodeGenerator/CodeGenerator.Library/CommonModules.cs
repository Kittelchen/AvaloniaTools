using CodeGenerator.Library.Generators;
using CodeGenerator.Library.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Library;

public static class CommonModules
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var appConfig = AppConfig.Load();

        services.AddTransient<Generator>();
        
        services.AddSingleton<IConfig>(appConfig);
        
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<IDbService, DbService>();
        
        services.AddSingleton<SqLiteGenerator>();
        services.AddSingleton<GeneratorFactory>();
        services.AddSingleton<EntityGenerator>();
        
        return services;
    }
}