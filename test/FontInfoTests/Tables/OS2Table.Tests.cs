using Xunit;
using System.Collections.Generic;
using System.IO;
using FontInfo.Tables;
using FontInfo.Records;
using FontInfo.Reader;
using System.Threading.Tasks;

namespace FontInfoTests.Tables
{
    public class OS2TableTests
    {
        
        

        [Fact]
        public async Task ShouldLoadOS2TableValuesForTTFAsync()
        {
            using (FileStream fs = new FileStream(Constants.TTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = await TableRecord.GetAllTablesAsync(binaryReader);
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    OS2Table os2Table = await OS2Table.CreateAsync(binaryReader, os2TableRecord);

                    Assert.Equal(64, os2Table.FsSelection);
                    Assert.Equal(1082, os2Table.Height);
                    Assert.Equal(1536, os2Table.TypoAscender);
                    Assert.Equal(-512, os2Table.TypoDescender);
                    Assert.Equal(102, os2Table.TypoLineGap);
                    Assert.Equal(1946, os2Table.WinAscent);
                    Assert.Equal(512, os2Table.WinDescent);

                }

            }

        }

        [Fact]
        public async Task ShouldLoadOS2TableValuesForOTFAsync()
        {
            using (FileStream fs = new FileStream(Constants.OTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = await TableRecord.GetAllTablesAsync(binaryReader);
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    OS2Table os2Table = await OS2Table.CreateAsync(binaryReader, os2TableRecord);

                    Assert.Equal(0, os2Table.FsSelection);
                    Assert.Equal(532, os2Table.Height);
                    Assert.Equal(968, os2Table.TypoAscender);
                    Assert.Equal(-251, os2Table.TypoDescender);
                    Assert.Equal(0, os2Table.TypoLineGap);
                    Assert.Equal(1006, os2Table.WinAscent);
                    Assert.Equal(194, os2Table.WinDescent);

                }

            }

        }
    }
}
