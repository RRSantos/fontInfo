using System;
using System.Collections.Generic;
using System.Text;

namespace FontInfo.Constants
{
    internal static class Numbers
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

        internal static class NameID
        {
            public const ushort Copyright = 0;
            public const ushort Family = 1;
            public const ushort Subfamily = 2;
            public const ushort UniqueID = 3;
            public const ushort FullName = 4;
            public const ushort Version = 5;
            public const ushort PostScriptName = 6;
            public const ushort Trademark = 7;
            public const ushort Manufacturer = 8;
            public const ushort Designer = 9;
            public const ushort Description = 10;
            public const ushort URLVendor = 11;
            public const ushort URLDesigner = 12;
            public const ushort LicenseDescription = 13;
            public const ushort LicenseURL = 14;
            public const ushort TypographicFamily = 16;
            public const ushort TypographicSubfamily = 17;
            public const ushort CompatibleFullName = 18;
            public const ushort SampleText = 19;
            public const ushort PostScriptCID = 20;
            public const ushort WWSFamily = 21;
            public const ushort WWSSubfamily = 22;
            public const ushort LightBackgroundPalette = 23;
            public const ushort DarkBackgroundPalette = 24;
            public const ushort PostScriptNamePrefix = 25;
        }

        internal static class PlatformID
        {
            public const ushort Unicode = 0;
            public const ushort Macintosh = 1;
            public const ushort Windows = 3;
        }


        internal static class SFNTVersion
        {
            public const uint DEFAULT = 0x00010000;
            public const uint OTTO = 0x4F54544F;
            public const uint TRUE = 0x74727565;
            public const uint TYP1 = 0x74797031;
        }

        internal static class LanguageID
        {
            internal static class Windows
            {
                public const ushort English = 1033;
            }

            internal static class Macintosh
            {
                public const ushort English = 0;
            }
        }

        


    }
}
