using FontInfo.Reader;
using FontInfo.Records;
using FontInfo.StringExtractor;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using static FontInfo.Constants.Numbers;

namespace FontInfo.Tables
{
    internal class NamingTable
    {
        public static async Task<NamingTable> CreateAsync(AsyncBinaryReader binaryReader, TableRecord namingTableRecord)
        {
            NamingTable table = new NamingTable(binaryReader, namingTableRecord);
            await table.loadDataAsync();
            return table;
            
        }
        private readonly AsyncBinaryReader binaryReader;
        private readonly TableRecord namingTableRecord;
        
        private async Task loadDataAsync()
        {   
            List<NameRecord> nameRecords = await getNameRecordsAsync();
            NameRecords =  deduplicateRecords(nameRecords);
        }

        private async Task<string> extractStringFromNameRecordAsync(AsyncBinaryReader binaryReader, 
            uint tableOffset, 
            ushort stringOffset, 
            ushort stringLength,
            ushort platformID,
            ushort encodingID)
        {
            uint totalOffset = tableOffset + stringOffset;

            binaryReader.BaseStream.Seek(totalOffset, SeekOrigin.Begin);
            byte[] nameByte = await binaryReader.ReadBytesAsync(stringLength);

            IStringExtractor stringExtractor = StringExtractorFactory.CreateExtractor(platformID, encodingID);
            return stringExtractor.Extract(nameByte);
        }

        private async Task<List<NameRecord>> getNameRecordsAsync()
        {
            binaryReader.BaseStream.Seek(namingTableRecord.Offset, SeekOrigin.Begin);
            await binaryReader.SkipAsync(2); //version
            ushort nameRecordCount = await binaryReader.ReadUInt16BEAsync();
            ushort storageOffset = await binaryReader.ReadUInt16BEAsync();

            List<NameRecord> nameRecords = new List<NameRecord>();

            for (int i = 0; i < nameRecordCount; i++)
            {
                ushort platformID = await binaryReader.ReadUInt16BEAsync();
                ushort encodingID = await binaryReader.ReadUInt16BEAsync();
                ushort languageID = await binaryReader.ReadUInt16BEAsync();
                ushort nameID = await binaryReader.ReadUInt16BEAsync();
                ushort length = await binaryReader.ReadUInt16BEAsync();
                ushort stringOffset = (ushort)(await binaryReader.ReadUInt16BEAsync() + storageOffset);
                long actualPosition = binaryReader.BaseStream.Position;
                string data = await extractStringFromNameRecordAsync(binaryReader, 
                    namingTableRecord.Offset,  
                    stringOffset,
                    length,
                    platformID,
                    encodingID);

                NameRecord newRecord = new NameRecord(
                    platformID,
                    encodingID,
                    languageID,
                    nameID,
                    length,
                    stringOffset,
                    data);

                nameRecords.Add(newRecord);
                binaryReader.BaseStream.Seek(actualPosition, SeekOrigin.Begin);

            }

            return nameRecords;
        }

        private List<NameRecord> deduplicateRecords(List<NameRecord> allRecords)
        {
            List<NameRecord> records = getWindowsEnglishRecords(allRecords);

            if (records.Count == 0)
            {
                records = getMacintoshEnglishRecords(allRecords);

                if (records.Count == 0)
                {
                    records = allRecords;
                }
            }

            return records;
        }

        private List<NameRecord> getWindowsEnglishRecords(List<NameRecord> allRecords)
        {
            return allRecords.FindAll(
                r => r.PlatformID == Constants.Numbers.PlatformID.Windows &&
                r.LanguageID == LanguageID.Windows.English
                );
        }

        private List<NameRecord> getMacintoshEnglishRecords(List<NameRecord> allRecords)
        {
            return allRecords.FindAll(
                r => r.PlatformID == Constants.Numbers.PlatformID.Macintosh &&
                r.LanguageID == LanguageID.Macintosh.English
                );
        }

        public IReadOnlyList<NameRecord> NameRecords { get; private set; }

        internal NamingTable (AsyncBinaryReader binaryReader, TableRecord namingTableRecord)
        {
            this.binaryReader = binaryReader;
            this.namingTableRecord = namingTableRecord;
        }
    }
}
