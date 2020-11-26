using System;
using System.Collections.Generic;
using System.Text;

namespace FontParser.StringExtractor
{
    class MacintoshStringExtractor : IStringExtractor
    {
        public string Extract(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
