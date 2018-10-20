using System.IO;
using Tars.Net.CLI;
using Xunit;

namespace UT
{
    public class CodecsCommandTest
    {
        [Fact]
        public void TestFindAllTarsFileWhenExistsDirectory()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "ff");
            var tarsFiles = new CodecsCommand().FindAllTarsFile(dir);
            Assert.Single(tarsFiles);
            Assert.EndsWith("a.tars", tarsFiles[0]);

            new CodecsCommand().Generate(tarsFiles, Directory.GetCurrentDirectory());
        }

        [Fact]
        public void TestFindAllTarsFileWhenExistsFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "ff", "test", "a.tars");
            var tarsFiles = new CodecsCommand().FindAllTarsFile(file);
            Assert.Single(tarsFiles);
            Assert.EndsWith("a.tars", tarsFiles[0]);
        }


        [Fact]
        public void TestFindAllTarsFileWhenNoExistsDirectory()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "ffd");
            var tarsFiles = new CodecsCommand().FindAllTarsFile(dir);
            Assert.Empty(tarsFiles);
        }

        [Fact]
        public void TestFindAllTarsFileWhenNoExistsFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "ff", "a.t");
            var tarsFiles = new CodecsCommand().FindAllTarsFile(file);
            Assert.Empty(tarsFiles);
        }
    }
}
