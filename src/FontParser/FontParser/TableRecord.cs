using FontParser.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FontParserTests")]
namespace FontParser
{
    internal class TableRecord
    {

        private static void validateSfntVersion(uint sfntVersion)
        {
            if ((sfntVersion != Constants.Numbers.SFNTVersion.DEFAULT) &&
                (sfntVersion != Constants.Numbers.SFNTVersion.OTTO) &&
                (sfntVersion != Constants.Numbers.SFNTVersion.TRUE) &&
                (sfntVersion != Constants.Numbers.SFNTVersion.TYP1))
            {
                throw new ArgumentException(string.Format(Constants.Errors.SFNT.UNKNOW_VERSION, sfntVersion));
            }
        }

        private void validateTagByteContent(byte[] tagByte)
        {
            if (tagByte.Length != 4)
            {
                throw new ArgumentException(Constants.Errors.TableRecord.INVALID_TAG_SIZE);
            }

            for (int i = 0; i < tagByte.Length; i++)
            {
                if (!isInUnicodeBasicLatinCharRange(tagByte[i]))
                {
                    throw new ArgumentOutOfRangeException(Constants.Errors.TableRecord.INVALID_CHAR_RANGE);
                }
            }
        }

        private bool isInUnicodeBasicLatinCharRange(byte b)
        {
            //range of values of Unicode Basic Latin characters in UTF-8 encoding
            return (b >= 0x20) && (b <= 0x7E);
        }

        private string convertToTagName(byte[] tagByte)
        {
            validateTagByteContent(tagByte);
            return Encoding.UTF8.GetString(tagByte);
        }

        public string Tag { get; private set; }
        public uint CheckSum { get; private set; }
        public uint Offset { get; private set; }
        public uint Length { get; private set; }

        public TableRecord(byte[] tagByte, uint checksum, uint offset, uint lenght)
        {
            Tag = convertToTagName(tagByte);
            CheckSum = checksum;
            Offset = offset;
            Length = lenght;
        }

        public static TableRecord GetOS2Table(List<TableRecord> allTables)
        {
            TableRecord os2TableRecord = allTables.Find(t => t.Tag == Constants.Strings.Tables.OS2);

            return os2TableRecord;
        }

        public static TableRecord GetNamesTable(List<TableRecord> allTables)
        {   
            TableRecord namesTableRecord = allTables.Find(t => t.Tag == Constants.Strings.Tables.NAME);

            return namesTableRecord;
        }

        public static TableRecord GetHeadTable(List<TableRecord> allTables)
        {
            TableRecord namesTableRecord = allTables.Find(t => t.Tag == Constants.Strings.Tables.HEAD);

            return namesTableRecord;
        }

        public static List<TableRecord> GetAllTables(BinaryReader binaryReader)
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

    }
}
