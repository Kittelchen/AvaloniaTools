using CodeGenerator.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => { CommonModules.AddCore(services); })
                .Build();

            var gen = host.Services.GetRequiredService<Generator>();

            if (gen.Initialize(@".\config.json"))
            {
                gen.Execute();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}