﻿using FontInfo.Reader;
using FontInfo.Records;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("FontInfoTests")]
namespace FontInfo.Tables
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

        public static async Task<OS2Table> Create(AsyncBinaryReader binaryReader, TableRecord os2Table)
        {

            binaryReader.BaseStream.Seek(os2Table.Offset, SeekOrigin.Begin);
            ushort version = await binaryReader.ReadUInt16BE();
            await binaryReader.Skip(60);
            ushort fsSelection = await binaryReader.ReadUInt16BE();
            await binaryReader.Skip(4);
            short typoAscender = await binaryReader.ReadInt16BE();
            short typoDescender = await binaryReader.ReadInt16BE();
            short typoLineGap = await binaryReader.ReadInt16BE();
            ushort winAscent = await binaryReader.ReadUInt16BE();
            ushort winDescent = await binaryReader.ReadUInt16BE();

            short height = 0;
            if (version >=2)
            {
                await binaryReader.Skip(8);
                height = await binaryReader.ReadInt16BE();
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
