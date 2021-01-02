using FontParser.Records;
using FontParser.Tables;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace FontParserTests.Tables
{

    public class HeadTableTests
    {

        [Fact]
        public void ShouldLoadHeadTableValuesForTTF()
        {
            using (FileStream fs = new FileStream(Constants.TTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    List<TableRecord> tables = TableRecord.GetAllTables(binaryReader);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);
                    HeadTable headTable = HeadTable.Create(binaryReader, headTableRecord);

                    Assert.Equal(1, headTable.MajorVersion);
                    Assert.Equal(0, headTable.MinorVersion);
                    Assert.Equal(2.137, headTable.FontRevision);
                    Assert.Equal(2048, headTable.UnitsPerEm);
                    

                }

            }
        }

        [Fact]
        public void ShouldLoadHeadTableValuesForOTF()
        {
            using (FileStream fs = new FileStream(Constants.OTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    List<TableRecord> tables = TableRecord.GetAllTables(binaryReader);
                    TableRecord headTableRecord = TableRecord.GetHeadTable(tables);
                    HeadTable headTable = HeadTable.Create(binaryReader, headTableRecord);

                    Assert.Equal(1, headTable.MajorVersion);
                    Assert.Equal(0, headTable.MinorVersion);
                    Assert.Equal(1, headTable.FontRevision);
                    Assert.Equal(1000, headTable.UnitsPerEm);


                }

            }
        }
    }
}
