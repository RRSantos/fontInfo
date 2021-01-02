using static FontParser.Constants.Numbers;
using FontParser.Extension;
using FontParser.StringExtractor;
using System.Collections.Generic;
using System.IO;
using System;
using FontParser.Records;

namespace FontParser
{
    public class FontDetails
    {

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

        private void initInteralFields(BinaryReader binaryReader, List<TableRecord> tables)
        {
            TableRecord nameTable = TableRecord.GetNamesTable(tables);
            List<NameRecord> nameRecords = getNameRecords(binaryReader, nameTable);

            List<NameRecord> filteredRecords = deduplicateRecords(nameRecords);

            foreach (NameRecord record in filteredRecords)
            {
                string data = extractStringFromNameRecord(binaryReader, record, nameTable.Offset);
                switch (record.NameID)
                {
                    case NameID.CompatibleFullName:
                        CompatibleFullName = data;
                        break;
                    case NameID.Copyright:
                        Copyright = data;
                        break;
                    case NameID.DarkBackgroundPalette:
                        DarkBackgroundPalette = data;
                        break;
                    case NameID.Description:
                        Description = data;
                        break;
                    case NameID.Designer:
                        Designer = data;
                        break;
                    case NameID.Family:
                        Family = data;
                        break;
                    case NameID.FullName:
                        FullName = data;
                        break;
                    case NameID.LicenseDescription:
                        LicenseDescription = data;
                        break;
                    case NameID.LicenseURL:
                        LicenseURL = data;
                        break;
                    case NameID.LightBackgroundPalette:
                        LightBackgroundPalette = data;
                        break;
                    case NameID.Manufacturer:
                        Manufacturer = data;
                        break;
                    case NameID.PostScriptCID:
                        PostScriptCID = data;
                        break;
                    case NameID.PostScriptName:
                        PostScriptName = data;
                        break;
                    case NameID.PostScriptNamePrefix:
                        PostScriptNamePrefix = data;
                        break;
                    case NameID.SampleText:
                        SampleText = data;
                        break;
                    case NameID.Subfamily:
                        Subfamily = data;
                        break;
                    case NameID.Trademark:
                        Trademark = data;
                        break;
                    case NameID.TypographicFamily:
                        TypographicFamily = data;
                        break;
                    case NameID.TypographicSubfamily:
                        TypographicSubfamily = data;
                        break;
                    case NameID.UniqueID:
                        UniqueID = data;
                        break;
                    case NameID.URLDesigner:
                        URLDesigner = data;
                        break;
                    case NameID.URLVendor:
                        URLVendor = data;
                        break;
                    case NameID.Version:
                        Version = data;
                        break;
                    case NameID.WWSFamily:
                        WWSFamily = data;
                        break;
                    case NameID.WWSSubfamily:
                        WWSSubfamily = data;
                        break;
                    default:
                        break;
                }
            }
        }

        private string extractStringFromNameRecord(BinaryReader binaryReader, NameRecord nameRecord, uint tableOffset)
        {
            uint stringOffset = tableOffset + nameRecord.StringOffset;

            binaryReader.BaseStream.Seek(stringOffset, SeekOrigin.Begin);
            byte[] nameByte = binaryReader.ReadBytes(nameRecord.Length);

            IStringExtractor stringExtractor = StringExtractorFactory.CreateExtractor(nameRecord.PlatformID, nameRecord.EncodingID);
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
                NameRecord newRecord = new NameRecord(
                    binaryReader.ReadUInt16BE(),
                    binaryReader.ReadUInt16BE(),
                    binaryReader.ReadUInt16BE(),
                    binaryReader.ReadUInt16BE(),
                    binaryReader.ReadUInt16BE(),
                    (ushort)(binaryReader.ReadUInt16BE() + storageOffset));

                nameRecords.Add(newRecord);
            }

            return nameRecords;
        }

        public string Copyright { get; private set; } 
        public string Family { get; private set; }
        public string Subfamily { get; private set; }
        public string UniqueID { get; private set; }
        public string FullName { get; private set; }
        public string Version { get; private set; }
        public string PostScriptName { get; private set; } 
        public string Trademark { get; private set; }
        public string Manufacturer { get; private set; }
        public string Designer { get; private set; }
        public string Description { get; private set; }
        public string URLVendor { get; private set; }
        public string URLDesigner { get; private set; }
        public string LicenseDescription { get; private set; }
        public string LicenseURL { get; private set; }
        public string TypographicFamily { get; private set; }
        public string TypographicSubfamily { get; private set; }
        public string CompatibleFullName { get; private set; }
        public string SampleText { get; private set; } = string.Empty;
        public string PostScriptCID { get; private set; }
        public string WWSFamily { get; private set; }
        public string WWSSubfamily { get; private set; }
        public string LightBackgroundPalette { get; private set; }
        public string DarkBackgroundPalette { get; private set; }
        public string PostScriptNamePrefix { get; private set; }

        internal FontDetails(BinaryReader binaryReader, List<TableRecord> tables)
        {
            initInteralFields(binaryReader, tables);
        }

        
    }
}
