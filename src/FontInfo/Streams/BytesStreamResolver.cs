using System.IO;

namespace FontInfo.Streams
{
    internal class BytesStreamResolver : IFontStreamResolver
    {
        private readonly byte[] bytes;

        public BytesStreamResolver(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public Stream GetStream()
        {
            return new MemoryStream(bytes);
        }
    }
}
