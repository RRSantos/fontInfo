using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FontInfo.Streams
{
    internal interface IFontStreamResolver
    {
        Stream GetStream();
    }
}
