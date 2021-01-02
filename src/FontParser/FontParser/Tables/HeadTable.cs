using FontParser.Extension;
using FontParser.Records;
using System.IO;

namespace FontParser.Tables
{
    internal class HeadTable
    {
        public ushort MajorVersion { get; private set; }
        public ushort MinorVersion { get; private set; }

        public double FontRevision { get; private set; }

        public ushort UnitsPerEm { get; private set; }

        protected HeadTable(ushort majorVersion, ushort minorVersion, double revision,  ushort unitsPerEm)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            FontRevision = revision;
            UnitsPerEm = unitsPerEm;
        }

        public static HeadTable Create(BinaryReader binaryReader, TableRecord headTableRecord)
        {
            binaryReader.BaseStream.Seek(headTableRecord.Offset, SeekOrigin.Begin);

            ushort majorVersion = binaryReader.ReadUInt16BE();
            ushort minorVersion = binaryReader.ReadUInt16BE();
            double revision = binaryReader.ReadInt32FixedBE();
            binaryReader.Skip(10);
            ushort unitsPerEm = binaryReader.ReadUInt16BE();

            HeadTable headTable = new HeadTable(majorVersion, minorVersion, revision, unitsPerEm);
            
            return headTable;
        }

    }
}
