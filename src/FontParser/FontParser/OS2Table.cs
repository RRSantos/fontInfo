using FontParser.Extension;
using System.IO;


namespace FontParser
{
    internal class OS2Table
    {
        private const uint BIT_7_MASK = 0b0000_0000_0100_0000;

        private bool shouldUseTypoMetrics()
        {
            if (Version < 4)
                return false;
            return (FsSelection & BIT_7_MASK) > 0;
        }
        public ushort Version { get; private set; }
        public ushort FsSelection { get; private set; }

        public short TypoAscender { get; private set; }
        public short TypoDescender { get; private set; }
        public short TypoLineGap { get; private set; }

        public ushort WinAscent { get; private set; }
        public ushort WinDescent { get; private set; }

        public bool ShouldUseTypoMetrics { get { return shouldUseTypoMetrics(); } }

        public OS2Table(ushort version, ushort fsSelection, short typoAscender, short typoDescender, short typoLineGap, ushort winAscent, ushort winDescent)
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
            short typoAscender = binaryReader.ReadInt16BE();
            short typoDescender = binaryReader.ReadInt16BE();
            short typoLineGap = binaryReader.ReadInt16BE();
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
