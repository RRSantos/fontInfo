using FontParser.Extension;
using System;
using System.Collections.Generic;
using System.IO;

namespace FontParser
{
    
    public class Font
    {

        
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
            if ((sfntVersion != Constants.Numbers.SFNTVersion.DEFAULT) && 
                (sfntVersion != Constants.Numbers.SFNTVersion.OTTO) &&
                (sfntVersion != Constants.Numbers.SFNTVersion.TRUE) &&
                (sfntVersion != Constants.Numbers.SFNTVersion.TYP1))
            {
                throw new ArgumentException(string.Format(Constants.Errors.SFNT.UNKNOW_VERSION, sfntVersion));
            }
        }        

        public FontMetrics Metrics { get; private set; }

        public FontDetails Details { get; private set; }



        public Font(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    List<TableRecord> tables = getTables(binaryReader);

                    Details = new FontDetails(binaryReader, tables);
                    Metrics = new FontMetrics(binaryReader, tables);

                }
                    
            }
        }
        
    }
}
