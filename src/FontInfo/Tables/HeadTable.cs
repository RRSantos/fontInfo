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

        public static async Task<HeadTable> CreateAsync(AsyncBinaryReader binaryReader, TableRecord headTableRecord)
        {
            binaryReader.BaseStream.Seek(headTableRecord.Offset, SeekOrigin.Begin);

            ushort majorVersion = await binaryReader.ReadUInt16BEAsync();
            ushort minorVersion = await binaryReader.ReadUInt16BEAsync();
            double revision = await binaryReader.ReadInt32FixedBEAsync();
            await binaryReader.SkipAsync(10);
            ushort unitsPerEm = await binaryReader.ReadUInt16BEAsync();

            HeadTable headTable = new HeadTable(majorVersion, minorVersion, revision, unitsPerEm);
            
            return headTable;
        }

    }
}
