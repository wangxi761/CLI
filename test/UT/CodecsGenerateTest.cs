using Antlr4.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.IO;
using Tars.Net.CLI;
using Tars.Net.CLI.Grammar;
using Xunit;

namespace UT
{
    public class CodecsGenerateTest
    {
        private string GetTestFile(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Codecs", fileName);
        }

        private string Generate(string file)
        {
            using (var stream = File.OpenRead(GetTestFile(file)))
            {
                var lexer = new GrammarLexer(new AntlrInputStream(stream));
                var parser = new GrammarParser(new CommonTokenStream(lexer));
                var visitor = new TarsGrammarVisitor();
                AdhocWorkspace cw = new AdhocWorkspace();
                var formattedNode = Formatter.Format(visitor.Visit(parser.tarsDefinitions()), cw);
                return formattedNode.ToFullString();
            }
        }

        [Fact]
        public void WhenOnlyNamespace()
        {
            var expected = @"namespace OnlyNamespace
{
}

namespace OnlyNamespace.Test
{
}".ReplaceLine();
            var result = Generate("OnlyNamespace.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyClass()
        {
            var expected = @"namespace OnlyNamespace
{
    [TarsStruct]
    public class C
    {
    }
}".ReplaceLine();
            var result = Generate("OnlyClass.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyField()
        {
            var expected = @"namespace OnlyNamespace
{
    [TarsStruct]
    public class C
    {
        [TarsStructProperty(1)]
        public short iVersion { get; set; }
        [TarsStructProperty(2)]
        public byte? cPacketType { get; set; } = 0;
        [TarsStructProperty(5)]
        public string sServantName { get; set; } = """";
        [TarsStructProperty(7)]
        public List<byte> sBuffer { get; set; }
        [TarsStructProperty(9)]
        public Dictionary<string, string> context { get; set; }
    }
}".ReplaceLine();
            var result = Generate("OnlyField.tars");
            Assert.Equal(expected, result);
        }
    }

    public static class TestExtensions
    {
        public static string ReplaceLine(this string str)
        {
            return str.Replace("\r\n", System.Environment.NewLine);
        }
    }
}