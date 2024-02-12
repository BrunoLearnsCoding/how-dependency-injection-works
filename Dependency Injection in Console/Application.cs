using Microsoft.Extensions.Logging;

namespace Dependency_Injection_in_Console;

public class Application
{
    private readonly ILogger _logger;
    public Application(ILogger<Application> logger)
    {
        _logger = logger;
    }
    public void StartUp() {
        _logger.LogCritical("Application started...");
        _logger.LogTrace("The Logic is executing...");
        _logger.LogError("Error!");
        _logger.LogInformation("Just logging");

        _logger.LogDebug("logging a debug");
    }
}
