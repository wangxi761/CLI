using Antlr4.Runtime;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tars.Net.CLI.Grammar;

namespace Tars.Net.CLI
{
    public class CodecsCommand : ICommand
    {
        public string Name => "codecs";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate tars file to csharp file";
            command.HelpOption();

            var sourceArgument = command.Argument("Source", "Tars file or Tars file directory");
            var destinationArgument = command.Argument("Destination directory", "CSharp file destination Directory");

            command.OnExecute(() =>
            {
                var files = FindAllTarsFile(sourceArgument.Value);
                if (files.Length == 0)
                {
                    Console.WriteLine($"No found any tars file from {sourceArgument.Value}");
                    return 1;
                }

                if (!Directory.Exists(destinationArgument.Value))
                {
                    Directory.CreateDirectory(destinationArgument.Value);
                }

                Generate(files, destinationArgument.Value);

                return 0;
            });
        }

        public void Generate(string[] files, string value)
        {
            using (var stream = File.OpenRead(files[0]))
            {
                var lexer = new GrammarLexer(new AntlrInputStream(stream));
                var parser = new GrammarParser(new CommonTokenStream(lexer));
                var visitor = new TarsGrammarVisitor();
                var a = visitor.Visit(parser.tarsDefinitions());
            }
        }

        public string[] FindAllTarsFile(string value)
        {
            if (Directory.Exists(value))
            {
                return FindAllTarsFileFromDirectory(value).ToArray();
            }
            else if (IsTarsFile(value) && File.Exists(value))
            {
                return new string[] { value };
            }
            else
            {
                return new string[0];
            }
        }

        private bool IsTarsFile(string file)
        {
            return file.EndsWith(".tars", StringComparison.OrdinalIgnoreCase);
        }

        private IEnumerable<string> FindAllTarsFileFromDirectory(string value)
        {
            return Directory.EnumerateFiles(value)
                .Union(Directory.EnumerateDirectories(value)
                    .SelectMany(FindAllTarsFileFromDirectory))
                .Where(IsTarsFile);
        }
    }
}