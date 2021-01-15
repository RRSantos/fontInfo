using System.Text;

namespace FontInfo.StringExtractor
{
    internal class UnicodeStringExtractor : IStringExtractor
    {
        public string Extract(byte[] data)
        {
            return Encoding.BigEndianUnicode.GetString(data);
        }
    }
}
