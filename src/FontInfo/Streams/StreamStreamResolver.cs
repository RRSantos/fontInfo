using System.IO;

namespace FontInfo.Streams
{
    internal class StreamStreamResolver : IFontStreamResolver
    {
        private readonly Stream stream;

        public StreamStreamResolver(Stream stream)
        {
            this.stream = stream;
        }

        public Stream GetStream()
        {
            return stream;
        }
    }
}
