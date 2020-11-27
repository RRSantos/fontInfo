using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FontParser
{
    public class FontMetrics
    {

        private void initInteralFields(BinaryReader binaryReader, List<TableRecord> tables)
        {
            TableRecord os2TableRecord = tables.Find(t => t.Tag == Constants.Strings.Tables.OS2);
            OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord);


            Ascender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoAscender : os2Table.WinAscent;
            Descender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoDescender : os2Table.WinDescent;
            LineSpacing = (ushort)(Ascender - Descender + os2Table.TypoLineGap);

            //Baseline;
            //Height;
            //BlackBoxHeight;
            //BlackBoxWidth;


        }

        public uint Baseline { get; private set; }
        public uint Ascender { get; private set; }

        public uint Descender { get; private set; }
        public uint Height { get; private set; }

        public uint LineSpacing { get; private set; }

        public uint BlackBoxHeight { get; private set; }

        public uint BlackBoxWidth{ get; private set; }

        internal FontMetrics(BinaryReader binaryReader, List<TableRecord> tables)
        {
            initInteralFields(binaryReader, tables);
        }
        
    }
}
