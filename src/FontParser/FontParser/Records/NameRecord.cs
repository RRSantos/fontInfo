using System;
using System.Collections.Generic;
using System.Text;

namespace FontParser.Records
{
    internal class NameRecord
    {
        public ushort PlatformID { get; private set; }
        public ushort EncodingID { get; private set; }
        public ushort LanguageID { get; private set; }
        public ushort NameID { get; private set; }
        public ushort Length { get; private set; }
        public ushort StringOffset { get; private set; }

        public NameRecord(ushort platformID, ushort encodingID, ushort languageID, ushort nameID, ushort length, ushort stringOffset)
        {
            PlatformID = platformID;
            EncodingID = encodingID;
            LanguageID = languageID;
            NameID = nameID;
            Length = length;
            StringOffset = stringOffset;    
        }

        

    }
}
