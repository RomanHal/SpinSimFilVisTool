using System;
using Xunit;

namespace Parser.Tests
{
    public class TextProcessingHelperTests
    {
        MifParser.Utility.TextProcessingHelper textHelper = new MifParser.Utility.TextProcessingHelper();
        [Fact]
        public void SearchClosingTest()
        {
           string[] lines = new string[] { "{{", "{{}", "}}","}" };
           int index= textHelper.SearchClosingBraces(lines, '{', 1);
            Assert.Equal(2,index);
        }

        [Fact]
        public void GetCodeToClosingBrace()
        {
            string[] lines = new string[] { "{{", "{{}", "}", "}}" };
            string[] test = textHelper.GetCodeToClosingBrace(lines, 1, '{');
            Assert.Equal(new string[] { "{{}","}" },test);
        }
    }
}
