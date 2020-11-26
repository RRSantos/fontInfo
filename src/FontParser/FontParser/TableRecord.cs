using System;
using System.Text;

namespace FontParser
{
    internal class TableRecord
    {
        private void validateTagByteContent(byte[] tagByte)
        {
            if (tagByte.Length != 4)
            {
                throw new ArgumentException(Constant.Error.TableRecord.INVALID_TAG_SIZE);
            }

            for (int i = 0; i < tagByte.Length; i++)
            {
                if (!isInUnicodeBasicLatinCharRange(tagByte[i]))
                {
                    throw new ArgumentOutOfRangeException(Constant.Error.TableRecord.INVALID_CHAR_RANGE);
                }
            }
        }

        private bool isInUnicodeBasicLatinCharRange(byte b)
        {
            //range of values of Unicode Basic Latin characters in UTF-8 encoding
            return (b >= 0x20) && (b <= 0x7E);
        }

        private string convertToTagName(byte[] tagByte)
        {
            validateTagByteContent(tagByte);
            return Encoding.UTF8.GetString(tagByte);
        }

        public string Tag { get; private set; }
        public uint CheckSum { get; private set; }
        public uint Offset { get; private set; }
        public uint Length { get; private set; }

        public TableRecord(byte[] tagByte, uint checksum, uint offset, uint lenght)
        {
            Tag = convertToTagName(tagByte);
            CheckSum = checksum;
            Offset = offset;
            Length = lenght;
        }

    }
}
