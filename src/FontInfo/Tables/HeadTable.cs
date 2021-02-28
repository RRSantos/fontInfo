using FontInfo.Reader;
using FontInfo.Records;
using System.IO;
using System.Threading.Tasks;

namespace FontInfo.Tables
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

        public static async Task<HeadTable> Create(AsyncBinaryReader binaryReader, TableRecord headTableRecord)
        {
            binaryReader.BaseStream.Seek(headTableRecord.Offset, SeekOrigin.Begin);

            ushort majorVersion = await binaryReader.ReadUInt16BE();
            ushort minorVersion = await binaryReader.ReadUInt16BE();
            double revision = await binaryReader.ReadInt32FixedBE();
            await binaryReader.Skip(10);
            ushort unitsPerEm = await binaryReader.ReadUInt16BE();

            HeadTable headTable = new HeadTable(majorVersion, minorVersion, revision, unitsPerEm);
            
            return headTable;
        }

    }
}
