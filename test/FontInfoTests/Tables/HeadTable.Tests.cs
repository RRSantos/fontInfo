using FontInfo.Reader;
using FontInfo.Records;
using FontInfo.Tables;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System.Threading.Tasks;

namespace FontInfoTests.Tables
{

    public class HeadTableTests
    {

        [Fact]
        public async Task ShouldLoadHeadTableValuesForTTFAsync()
        {
            using (FileStream fs = new FileStream(Constants.TTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = await TableRecord.GetAllTablesAsync(binaryReader);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);
                    HeadTable headTable = await HeadTable.CreateAsync(binaryReader, headTableRecord);

                    Assert.Equal(1, headTable.MajorVersion);
                    Assert.Equal(0, headTable.MinorVersion);
                    Assert.Equal(2.137, headTable.FontRevision);
                    Assert.Equal(2048, headTable.UnitsPerEm);
                    

                }

            }
        }

        [Fact]
        public async Task ShouldLoadHeadTableValuesForOTFAsync()
        {
            using (FileStream fs = new FileStream(Constants.OTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = await TableRecord.GetAllTablesAsync(binaryReader);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);
                    HeadTable headTable = await HeadTable.CreateAsync(binaryReader, headTableRecord);

                    Assert.Equal(1, headTable.MajorVersion);
                    Assert.Equal(0, headTable.MinorVersion);
                    Assert.Equal(1, headTable.FontRevision);
                    Assert.Equal(1000, headTable.UnitsPerEm);


                }

            }
        }
    }
}
