﻿using Xunit;
using System.Collections.Generic;
using System.IO;
using FontInfo.Tables;
using FontInfo.Records;
using FontInfo.Reader;

namespace FontInfoTests.Tables
{
    public class OS2TableTests
    {
        
        

        [Fact]
        public void ShouldLoadOS2TableValuesForTTF()
        {
            using (FileStream fs = new FileStream(Constants.TTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = TableRecord.GetAllTables(binaryReader).Result;
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord).Result;

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
        public void ShouldLoadOS2TableValuesForOTF()
        {
            using (FileStream fs = new FileStream(Constants.OTFFontFilename, FileMode.Open, FileAccess.Read))
            {
                using (AsyncBinaryReader binaryReader = new AsyncBinaryReader(fs))
                {
                    List<TableRecord> tables = TableRecord.GetAllTables(binaryReader).Result;
                    TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
                    OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord).Result;

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
