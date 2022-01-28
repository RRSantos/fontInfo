using System.IO;

namespace FontInfo.Streams
{
    internal class FileNameStreamResolver : IFontStreamResolver
    {
        private readonly string fileName;

        public FileNameStreamResolver(string fileName)
        {
            this.fileName = fileName;
        }

        public Stream GetStream()
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }
    }
}
