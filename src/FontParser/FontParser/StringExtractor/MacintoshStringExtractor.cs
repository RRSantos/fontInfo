using System;
using System.Collections.Generic;
using System.Text;

namespace FontInfo.StringExtractor
{
    class MacintoshStringExtractor : IStringExtractor
    {
        public string Extract(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
