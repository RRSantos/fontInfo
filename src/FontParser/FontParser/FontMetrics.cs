using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FontParser
{
    public class FontMetrics
    {
        public uint Baseline { get; private set; }
        public uint Ascender { get; private set; }

        public uint Descender { get; private set; }
        public uint Height { get; private set; }

        public uint BlackBoxHeight { get; private set; }

        public uint BlackBoxWidth{ get; private set; }

        internal FontMetrics(BinaryReader binaryReader, List<TableRecord> tables)
        {

        }
        
    }
}
