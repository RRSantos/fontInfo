using FontParser.Extension;
using System.IO;


namespace FontParser
{
    internal class OS2Table
    {
        private const uint BIT_7_MASK = 0b0000_0000_0100_0000;

        private bool shouldUseTypoMetrics()
        {
            return (FsSelection & BIT_7_MASK) > 0;
        }
        public ushort Version { get; private set; }
        public ushort FsSelection { get; private set; }

        public ushort TypoAscender { get; private set; }
        public ushort TypoDescender { get; private set; }
        public ushort TypoLineGap { get; private set; }

        public ushort WinAscent { get; private set; }
        public ushort WinDescent { get; private set; }

        public bool ShouldUseTypoMetrics { get { return shouldUseTypoMetrics(); } }

        public OS2Table(ushort version, ushort fsSelection, ushort typoAscender, ushort typoDescender, ushort typoLineGap, ushort winAscent, ushort winDescent)
        {
            Version = version;
            FsSelection = fsSelection;
            TypoAscender = typoAscender;
            TypoDescender = typoDescender;
            TypoLineGap = typoLineGap;
            WinAscent = winAscent;
            WinDescent = winDescent;
        }

        public static OS2Table Create(BinaryReader binaryReader, TableRecord os2Table)
        {

            binaryReader.BaseStream.Seek(os2Table.Offset, SeekOrigin.Begin);
            ushort version = binaryReader.ReadUInt16BE();
            binaryReader.Skip(60);
            ushort fsSelection = binaryReader.ReadUInt16BE();
            binaryReader.Skip(4);
            ushort typoAscender = binaryReader.ReadUInt16BE();
            ushort typoDescender = binaryReader.ReadUInt16BE();
            ushort typoLineGap = binaryReader.ReadUInt16BE();
            ushort winAscent = binaryReader.ReadUInt16BE();
            ushort winDescent = binaryReader.ReadUInt16BE();

            return new OS2Table(
                version,
                fsSelection,
                typoAscender,
                typoDescender,
                typoLineGap,
                winAscent,
                winDescent);
        }
    }
}
