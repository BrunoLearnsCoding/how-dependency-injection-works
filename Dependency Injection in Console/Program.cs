using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace Dependency_Injection_in_Console;

class Program
{
    static void Main(string[] args)
    {
        using var services = CreateServices(args);

        Application app = services.GetRequiredService<Application>();

        app.StartUp();
    }

    private static ServiceProvider CreateServices(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                // ["LogLevel:Default"] = "Error",
                // ["Logging:LogLevel:Console:Default"] = "Trace"
            })
            .AddEnvironmentVariables()
            .AddJsonFile(@"appsettings.json")
            .AddUserSecrets("5a730dc7-4fdd-420d-a27e-016e8b212fa6")
            .AddCommandLine(args)
            .AddCustom()
            .Build();
        // System.Console.WriteLine(configuration.GetSection("LogLevel").GetSection("Default"));
        var items = new List<string>() {"username", "sport", "environment", "loglevel", "logging:loglevel:default", "loglevel:default"};

        foreach (var i in configuration.AsEnumerable())
        {
            if (items.Contains(i.Key.ToLower()))
            {
                System.Console.WriteLine($"{i.Key} : {i.Value}");
            }
        }
        var serviceProvider = new ServiceCollection()
            .AddSingleton<Application>()
            .AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddDebug();
                builder.AddConsole();               
                builder.AddConfiguration(configuration.GetSection("Logging"));
                // if (OperatingSystem.IsWindows())
                // {
                //     options.AddEventLog();
                // }
                // options.SetMinimumLevel(LogLevel.Debug);
                // options.AddDebug();
            })
            .BuildServiceProvider();
        return serviceProvider;
    }
}
