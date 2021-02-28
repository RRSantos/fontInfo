using FontInfo.Tables;
using FontInfo.Records;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FontInfo.Reader;

namespace FontInfo
{
    
    public class Font
    {  
        private readonly string filename;

        private async Task loadDataAsync()
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = await TableRecord.GetAllTablesAsync(binaryReader);

                    TableRecord namingTableRecord = TableRecord.GetNamesTable(tables);
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);

                    NamingTable namingTable = await NamingTable.CreateAsync(binaryReader, namingTableRecord);
                    OS2Table os2Table = await OS2Table.CreateAsync(binaryReader, os2TableRecord);
                    HeadTable headTable = await HeadTable.CreateAsync(binaryReader, headTableRecord);

                    Details = new FontDetails(namingTable, headTable);
                    Metrics = new FontMetrics(os2Table, headTable);

                }
            }
        }

        public static async Task<Font> CreateAsync(string fileName)
        {
            Font font = new Font(fileName);
            await font.loadDataAsync();
            return font;
        }
        

        public FontMetrics Metrics { get; private set; }

        public FontDetails Details { get; private set; }


        private Font(string fileName)
        {
            this.filename = fileName;
        }
        
    }
}
