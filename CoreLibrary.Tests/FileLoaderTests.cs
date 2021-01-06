using CoreLibrary.FileServices;
using System;
using Xunit;

namespace CoreLibrary.Tests
{
    public class FileLoaderTests
    {
        [Fact]
        public void FileLoaderTest()
        {
            FileLoader fileLoader = new FileLoader();
            var container = fileLoader.LoadFile(@"");
            
            Assert.Equal(3, container.ObjectNames.Count);
        }
    }
}
