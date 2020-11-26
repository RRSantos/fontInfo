using System;
using System.Collections.Generic;
using System.Text;

namespace FontParser.Constant.NameRecord
{
    internal static class EncodingID
    {
        internal static class Unicode
        {
            public const ushort Unicode1_0 = 0;
            public const ushort Unicode1_1 = 1;
            public const ushort ISO_IEC_10646 = 2;
            public const ushort Unicode2_0_BMP = 3;
            public const ushort Unicode2_0 = 4;
        }

        internal static class Windows
        {
            public const ushort Symbol = 0;
            public const ushort UnicodeBMP = 1;
            public const ushort UnicodeFull = 10;
        }
    }
}
