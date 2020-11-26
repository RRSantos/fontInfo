using System;
using Xunit;
using FontParser;

namespace FontParserTests
{
    public class FontTests
    {
        [Fact]
        public void ShouldReadSfntTag()
        {
            Font font = new Font(@"D:\Temp\Roboto\Roboto-Regular.ttf");
            
            Assert.Equal("Roboto", font.FullName);

        }
    }
}
