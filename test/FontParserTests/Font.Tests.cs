using System;
using Xunit;
using FontParser;

namespace FontParserTests
{
    public class FontTests
    {
        [Fact]
        public void ShouldReadWindowsFont()
        {
            Font font = new Font(@"D:\Temp\Roboto\Roboto-Regular.ttf");
            
            Assert.Equal("Roboto", font.FullName);

        }

        [Fact]
        public void ShouldReadMacintoshFont()
        {
            Font font = new Font(@"D:\Temp\trueno-font\TruenoBlack-mBYV.otf");

            Assert.Equal("Trueno Black", font.FullName);

        }
    }
}
