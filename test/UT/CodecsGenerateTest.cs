using Antlr4.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using System.IO;
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
            var f = GetTestFile(file);
            using (var stream = File.OpenRead(f))
            {
                var lexer = new GrammarLexer(new AntlrInputStream(stream));
                var parser = new GrammarParser(new CommonTokenStream(lexer));
                var visitor = new TarsGrammarVisitor(f);
                AdhocWorkspace cw = new AdhocWorkspace();
                var formattedNode = Formatter.Format(visitor.Visit(parser.tarsDefinition()), cw);
                return formattedNode.ToFullString();
            }
        }

        [Fact]
        public void WhenOnlyNamespace()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;

namespace OnlyNamespace
{
}

namespace OnlyNamespace.Test
{
}";
            var result = Generate("OnlyNamespace.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyClass()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;

namespace OnlyNamespace
{
    [TarsStruct]
    public class C
    {
    }
}";
            var result = Generate("OnlyClass.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyField()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;

namespace OnlyNamespace
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
        public byte[] sBuffer { get; set; }
        [TarsStructProperty(9)]
        public Dictionary<string, string> context { get; set; }
    }
}";
            var result = Generate("OnlyField.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyEnum()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;

namespace OnlyNamespace
{
    public enum EMTaskCommand
    {
        EM_CMD_START = -1,
        EM_CMD_STOP = 0,
        EM_CMD_PATCH,
        EM_CMD_UNINSTALL
    }
}";
            var result = Generate("OnlyEnum.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyInterface()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;

namespace OnlyNamespace
{
    [Rpc]
    public interface Patch
    {
        Task<int> listFileInfo(string path, List<FileInfo> vf);
        Task<int> download(string file, int pos, byte[] vb);
        Task<int> preparePatchFile(string app, string serverName, string outpatchFile = ""test.cs"");
    }
}";
            var result = Generate("OnlyInterface.tars");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenOnlyInclude()
        {
            var expected = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tars.Net.Attributes;
using OnlyNamespace;

namespace Test
{
}";
            var result = Generate("OnlyInclude.tars");
            Assert.Equal(expected, result);
        }
    }
}