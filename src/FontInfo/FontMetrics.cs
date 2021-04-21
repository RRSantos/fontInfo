using FontInfo.Tables;

namespace FontInfo
{
    public class FontMetrics
    {

        private void initInteralFields(OS2Table os2Table, HeadTable headTable)
        {
            Ascender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoAscender : os2Table.WinAscent;
            Descender = os2Table.ShouldUseTypoMetrics ? (ushort)os2Table.TypoDescender : os2Table.WinDescent;
            Height = Ascender + Descender;
            LineSpacing = (ushort)(Height + os2Table.TypoLineGap);
            UnitsPerEm = headTable.UnitsPerEm;
        }

        public uint UnitsPerEm { get; private set; }

        public uint Ascender { get; private set; }

        public uint Descender { get; private set; }
        public uint Height { get; private set; }

        public uint LineSpacing { get; private set; }

        internal FontMetrics(OS2Table os2Table, HeadTable headTable)
        {
            initInteralFields(os2Table, headTable);
        }

    }
}
