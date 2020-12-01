using Xunit;
using FontParser;

namespace FontParserTests
{
    public class FontTests
    {
        [Fact]
        public void ShouldReadWindowsFont()
        {
            Font font = new Font(@"fonts/Roboto-Regular.ttf");
            
            Assert.Equal("Roboto", font.Details.FullName);

        }

        [Fact]
        public void ShouldReadMacintoshFont()
        {
            Font font = new Font(@"fonts/Trueno-wml2.otf");

            Assert.Equal("Trueno", font.Details.FullName);
        }
        
    }
}
