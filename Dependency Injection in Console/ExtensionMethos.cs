using Microsoft.Extensions.Configuration;

namespace Dependency_Injection_in_Console;

public static class ExtensionMethods 
{
    public static IConfigurationBuilder AddCustom(this IConfigurationBuilder builder) {
        builder.AddInMemoryCollection(new Dictionary<string, string?>() {
            ["Sport"] = "Vollyball"
        });
        
        return builder;
    }
}