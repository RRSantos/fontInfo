using System;
using System.IO;
using System.Threading.Tasks;

namespace FontInfo.Reader
{
    internal class AsyncBinaryReader : IDisposable
    {
        public Stream BaseStream { get; private set; }

        private ushort toUInt16BE(byte[] bytes)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return BitConverter.ToUInt16(bytes, 0);
            }
            return (ushort)((bytes[0] << 8) | (bytes[1]));
        }
        private short toInt16BE(byte[] bytes)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return BitConverter.ToInt16(bytes, 0);
            }
            return (short)((bytes[0] << 8) | (bytes[1]));
        }
        private uint toUInt32BE(byte[] bytes)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return BitConverter.ToUInt32(bytes, 0);
            }
            return (uint)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | (bytes[3]));
        }
        public AsyncBinaryReader(Stream baseStream)
        {
            BaseStream = baseStream;
        }

        public void Dispose()
        {
            BaseStream.Dispose();
        }

        public void Close()
        {
            BaseStream.Close();
        }

        public async Task SkipAsync(int count)
        {
            byte[] trash = new byte[count];
            await BaseStream.ReadAsync(trash, 0, count).ConfigureAwait(false);
        }

        public async Task<byte[]> ReadBytesAsync(int count)
        {
            byte[] data = new byte[count];
            await BaseStream.ReadAsync(data, 0, count).ConfigureAwait(false);
            return data;
        }

        public async Task<ushort> ReadUInt16BEAsync()
        {
            byte[] data = new byte[2];
            await BaseStream.ReadAsync(data, 0, data.Length).ConfigureAwait(false);

            ushort result = toUInt16BE(data);
            return result;
        }

        public async Task<short> ReadInt16BEAsync()
        {
            byte[] data = new byte[2];
            await BaseStream.ReadAsync(data, 0, data.Length).ConfigureAwait(false);

            short result = toInt16BE(data);
            return result;
        }
        public async Task<uint> ReadUInt32BEAsync()
        {
            byte[] data = new byte[4];
            await BaseStream.ReadAsync(data, 0, data.Length).ConfigureAwait(false);

            uint result = toUInt32BE(data);
            return result;
        }

        public async Task<double> ReadInt32FixedBEAsync()
        {
            short hi = await this.ReadInt16BEAsync().ConfigureAwait(false);
            ushort lo = await this.ReadUInt16BEAsync().ConfigureAwait(false);

            double result = Math.Round(((hi) + ((double)lo / ushort.MaxValue)) * 10000) / 10000;
            return result;
        }

    }
}
