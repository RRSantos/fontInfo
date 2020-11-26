using FontParser.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FontParser.Constant.NameRecord;
using FontParser.StringExtractor;

namespace FontParser
{
    
    public class Font
    {

        private List<NameRecord> getNameRecords(BinaryReader binaryReader, TableRecord nameTable)
        {
            binaryReader.BaseStream.Seek(nameTable.Offset, SeekOrigin.Begin);
            ushort version = binaryReader.ReadUInt16BE();
            ushort nameRecordCount = binaryReader.ReadUInt16BE();
            ushort storageOffset = binaryReader.ReadUInt16BE();
            
            List<NameRecord> nameRecords = new List<NameRecord>();
            
            for (int i = 0; i < nameRecordCount; i++)
            {
                NameRecord newRecord =  new NameRecord(
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
        private List<TableRecord> getTables(BinaryReader binaryReader)
        {
            uint sfntVersion = binaryReader.ReadUInt32BE();
            validateSfntVersion(sfntVersion);
            uint tableCount = binaryReader.ReadUInt16BE();

            binaryReader.Skip(6);

            List<TableRecord> tables = new List<TableRecord>();

            for (int i = 0; i < tableCount; i++)
            {
                TableRecord record = new TableRecord(
                    binaryReader.ReadBytes(4),
                    binaryReader.ReadUInt32BE(),
                    binaryReader.ReadUInt32BE(),
                    binaryReader.ReadUInt32BE());

                tables.Add(record);
            }

            return tables;
        }
        private void validateSfntVersion(uint sfntVersion)
        {
            if ((sfntVersion != 0x00010000) && // default signature
                (sfntVersion != 0x4F54544F) && // 'OTTO'
                (sfntVersion != 0x74727565) && // 'true'
                (sfntVersion != 0x74797031))   // 'typ1'
            {
                throw new ArgumentException(string.Format(Constant.Error.SFNT.UNKNOW_VERSION, sfntVersion));
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

        private void fillInternalPropsRelatedToNameTable(BinaryReader binaryReader, List<TableRecord> tables)
        {
            TableRecord nameTable = tables.Find(x => x.Tag == "name");
            List<NameRecord> nameRecords = getNameRecords(binaryReader, nameTable);

            foreach (NameRecord record in nameRecords)
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
        public string CompatibleFullName  { get; private set; }
        public string SampleText { get; private set; }
        public string PostScriptCID { get; private set; }
        public string WWSFamily { get; private set; }
        public string WWSSubfamily { get; private set; }
        public string LightBackgroundPalette { get; private set; }
        public string DarkBackgroundPalette { get; private set; }
        public string PostScriptNamePrefix { get; private set; }



        public Font(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    List<TableRecord> tables = getTables(binaryReader);

                    fillInternalPropsRelatedToNameTable(binaryReader, tables);                    

                }
                    
            }
        }
        
    }
}
