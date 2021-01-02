using FontParser.Extension;
using FontParser.Records;
using FontParser.StringExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static FontParser.Constants.Numbers;

namespace FontParser.Tables
{
    internal class NamingTable
    {

        private void initInteralFields(BinaryReader binaryReader, TableRecord namingTableRecord)
        {   
            List<NameRecord> nameRecords = getNameRecords(binaryReader, namingTableRecord);
            NameRecords =  deduplicateRecords(nameRecords);
        }

        private string extractStringFromNameRecord(BinaryReader binaryReader, 
            uint tableOffset, 
            ushort stringOffset, 
            ushort stringLength,
            ushort platformID,
            ushort encodingID)
        {
            uint totalOffset = tableOffset + stringOffset;

            binaryReader.BaseStream.Seek(totalOffset, SeekOrigin.Begin);
            byte[] nameByte = binaryReader.ReadBytes(stringLength);

            IStringExtractor stringExtractor = StringExtractorFactory.CreateExtractor(platformID, encodingID);
            return stringExtractor.Extract(nameByte);
        }

        private List<NameRecord> getNameRecords(BinaryReader binaryReader, TableRecord nameTable)
        {
            binaryReader.BaseStream.Seek(nameTable.Offset, SeekOrigin.Begin);
            binaryReader.Skip(2); //version
            ushort nameRecordCount = binaryReader.ReadUInt16BE();
            ushort storageOffset = binaryReader.ReadUInt16BE();

            List<NameRecord> nameRecords = new List<NameRecord>();

            for (int i = 0; i < nameRecordCount; i++)
            {
                ushort platformID = binaryReader.ReadUInt16BE();
                ushort encodingID = binaryReader.ReadUInt16BE();
                ushort languageID = binaryReader.ReadUInt16BE();
                ushort nameID = binaryReader.ReadUInt16BE();
                ushort length = binaryReader.ReadUInt16BE();
                ushort stringOffset = (ushort)(binaryReader.ReadUInt16BE() + storageOffset);
                long actualPosition = binaryReader.BaseStream.Position;
                string data = extractStringFromNameRecord(binaryReader, 
                    nameTable.Offset,  
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

        internal NamingTable (BinaryReader binaryReader, TableRecord namingTableRecord)
        {
            initInteralFields(binaryReader, namingTableRecord);
        }
    }
}
