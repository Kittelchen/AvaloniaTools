using CodeGenerator.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        CommonModules.AddCore(services);
    })
    .Build();

var gen = host.Services.GetRequiredService<Generator>();

if (gen.Initialize(@".\config.json"))
{
    gen.Execute();
}