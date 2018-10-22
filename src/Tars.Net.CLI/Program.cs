using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tars.Net.CLI
{
    public class Program
    {
        private readonly IServiceProvider provider;

        public Program()
        {
            var services = new ServiceCollection();
            services.AddSingleton(CreateApplication());
            services.AddSingleton<ICommand, CodecsCommand>();
            provider = services.BuildServiceProvider();
        }

        private static int Main(string[] args)
        {
            return new Program().Run(args);
        }

        private int Run(string[] args)
        {
            try
            {
                var app = provider.GetRequiredService<CommandLineApplication>();
                foreach (var command in provider.GetServices<ICommand>())
                {
                    app.Command(command.Name, command.Execute);
                }
                return app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Tars.net tool error : {e.Message}");
                return 1;
            }
        }

        private CommandLineApplication CreateApplication()
        {
            var app = new CommandLineApplication
            {
                Name = "dotnet tarsnet",
                FullName = "Tars.Net Command Line Tool",
                Description = "Tars.Net Command Line Tool."
            };

            app.HelpOption();
            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });
            return app;
        }
    }
}