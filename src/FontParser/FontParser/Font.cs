using FontParser.Extension;
using System;
using System.Collections.Generic;
using System.IO;

namespace FontParser
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
                    Details = new FontDetails(binaryReader, tables);

                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord);
                    Metrics = new FontMetrics(os2Table);

                }
                    
            }
        }
        
    }
}
