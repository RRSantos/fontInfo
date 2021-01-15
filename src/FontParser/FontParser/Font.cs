using FontInfo.Extension;
using FontInfo.Tables;
using FontInfo.Records;
using System;
using System.Collections.Generic;
using System.IO;
//using static FontParser.Constants.Errors;

namespace FontInfo
{
    
    public class Font
    {   

        public FontMetrics Metrics { get; private set; }

        public FontDetails Details { get; private set; }


        public Font(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    List<TableRecord> tables = TableRecord.GetAllTables(binaryReader);

                    TableRecord namingTableRecord = TableRecord.GetNamesTable(tables);
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);

                    NamingTable namingTable = new NamingTable(binaryReader, namingTableRecord);
                    OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord);
                    HeadTable headTable = HeadTable.Create(binaryReader, headTableRecord);

                    Details = new FontDetails(namingTable, headTable);
                    Metrics = new FontMetrics(os2Table, headTable);

                }
                    
            }
        }
        
    }
}
