using FontParser.Extension;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FontParserTests")]
namespace FontParser
{
    
    internal  class OS2Table
    {
        private const ushort BIT_7_MASK = 0b0000_0000_0100_0000;

        private readonly ushort version;

        private bool shouldUseTypoMetrics()
        {
            if (version < 4)
                return false;
            return (FsSelection & BIT_7_MASK) > 0;
        }        

        
        public ushort FsSelection { get; private set; }

        public short TypoAscender { get; private set; }
        public short TypoDescender { get; private set; }
        public short TypoLineGap { get; private set; }

        public ushort WinAscent { get; private set; }
        public ushort WinDescent { get; private set; }

        public short Height { get; private set; }

        public bool ShouldUseTypoMetrics { get { return shouldUseTypoMetrics(); } }

        public OS2Table(ushort version, ushort fsSelection, short typoAscender, short typoDescender, short typoLineGap, ushort winAscent, ushort winDescent, short height)
        {
            this.version = version;
            FsSelection = fsSelection;
            TypoAscender = typoAscender;
            TypoDescender = typoDescender;
            TypoLineGap = typoLineGap;
            WinAscent = winAscent;
            WinDescent = winDescent;
            Height = height;
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

            short height = 0;
            if (version >=2)
            {
                binaryReader.Skip(8);
                height = binaryReader.ReadInt16BE();
            }

            return new OS2Table(
                version,
                fsSelection,
                typoAscender,
                typoDescender,
                typoLineGap,
                winAscent,
                winDescent, 
                height);
        }
    }
}
