using static FontInfo.Constants.Numbers;
using System;

namespace FontInfo.StringExtractor
{
    internal static class StringExtractorFactory
    {
        public static IStringExtractor CreateExtractor(ushort platformID, ushort encodingID)
        {
            if (platformID == Constants.Numbers.PlatformID.Windows)
            {
                if ((encodingID != EncodingID.Windows.Symbol) &&
                    (encodingID != EncodingID.Windows.UnicodeBMP) &&
                    (encodingID != EncodingID.Windows.UnicodeFull))
                {
                    throw new ArgumentException(
                        string.Format(
                            Constants.Errors.StringExtractorFactory.INVALID_ENCODIG_FOR_PLATFORM,
                            "Windows",
                            platformID,
                            encodingID));

                }

                return new WindowsStringExtractor();
            }
            else if (platformID == Constants.Numbers.PlatformID.Unicode)
            {
                if ((encodingID != EncodingID.Unicode.Unicode2_0) &&
                    (encodingID != EncodingID.Unicode.Unicode2_0_BMP))
                {
                    throw new ArgumentException(
                        string.Format(
                            Constants.Errors.StringExtractorFactory.INVALID_ENCODIG_FOR_PLATFORM,
                            "Unicode",
                            platformID,
                            encodingID));
                }

                return new UnicodeStringExtractor();
            }
            else if (platformID == Constants.Numbers.PlatformID.Macintosh)
            {   
                return new MacintoshStringExtractor();
            }


            throw new ArgumentException(
                string.Format(
                    Constants.Errors.StringExtractorFactory.INVALID_PLATFORM_ID,
                    platformID
                )
            );
        }
    }
}
