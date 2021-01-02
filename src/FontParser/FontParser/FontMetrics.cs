using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FontParser
{
    public class FontMetrics
    {

        private void initInteralFields(OS2Table os2Table)
        {
            //TableRecord os2TableRecord = TableRecord.GetOS2Table(tables);
            //OS2Table os2Table = OS2Table.Create(binaryReader, os2TableRecord);
            //TableRecord headTableRecord = TableRecord.GetHeadTable(tables);

            Ascender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoAscender : os2Table.WinAscent;
            Descender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoDescender : os2Table.WinDescent;
            Height = Ascender + Descender;
            LineSpacing = (ushort)(Height + os2Table.TypoLineGap);
            
        }

        public uint UnitsPerEm { get; private set; }

        public uint Ascender { get; private set; }

        public uint Descender { get; private set; }
        public uint Height { get; private set; }

        public uint LineSpacing { get; private set; }

        internal FontMetrics(OS2Table os2Table)
        {
            initInteralFields(os2Table);
        }
        
    }
}
