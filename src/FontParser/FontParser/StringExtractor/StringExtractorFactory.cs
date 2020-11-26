using FontParser.Constant.NameRecord;
using System;

namespace FontParser.StringExtractor
{
    internal static class StringExtractorFactory
    {
        public static IStringExtractor CreateExtractor(ushort platformID, ushort encodingID)
        {
            if (platformID == Constant.NameRecord.PlatformID.Windows)
            {
                if ((encodingID != EncodingID.Windows.Symbol) &&
                    (encodingID != EncodingID.Windows.UnicodeBMP) &&
                    (encodingID != EncodingID.Windows.UnicodeFull))
                {
                    throw new ArgumentException($"Windows platform (platformID={platformID}): Invalid encodingID: {encodingID}.");
                }
                return new WindowsStringExtractor();
            }

            return null;
        }
    }
}
