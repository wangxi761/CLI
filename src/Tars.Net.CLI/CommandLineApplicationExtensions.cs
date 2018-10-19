using Microsoft.Extensions.CommandLineUtils;
using System.Reflection;

namespace Tars.Net.CLI
{
    public static class CommandLineApplicationExtensions
    {
        public static CommandOption HelpOption(this CommandLineApplication app)
        {
            return app.HelpOption("-?|-h|--help");
        }

        public static void VersionOptionFromAssemblyAttributes(this CommandLineApplication app, Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var versionAttribute = attribute == null
                ? assembly.GetName().Version.ToString()
                : attribute.InformationalVersion;

            app.VersionOption("--version", versionAttribute);
        }
    }
}