using System.Text;

namespace FontParser.StringExtractor
{
    internal class WindowsStringExtractor : IStringExtractor
    {
        public string Extract(byte[] data)
        {
            return Encoding.BigEndianUnicode.GetString(data);
        }
    }
}
