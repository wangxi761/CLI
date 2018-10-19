using Microsoft.Extensions.CommandLineUtils;

namespace Tars.Net.CLI
{
    public interface ICommand
    {
        string Name { get; }

        void Execute(CommandLineApplication command);
    }
}