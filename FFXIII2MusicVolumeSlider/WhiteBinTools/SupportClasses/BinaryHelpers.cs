using System;
using System.IO;
using System.Text;
using static FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses.CmnEnums;

namespace FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses
{
    public static class BinaryHelpers
    {
        public static string BinaryToString(this BinaryReader readerName, uint readerPos)
        {
            readerName.BaseStream.Position = readerPos;
            var parsedString = new StringBuilder();
            char getParsedString;
            while ((getParsedString = readerName.ReadChar()) != default)
            {
                parsedString.Append(getParsedString);
            }

            return parsedString.ToString();
        }


        public static void ExWriteBytesUInt32(this BinaryWriter writerName, uint writerPos, uint adjustVal, Endianness endianness)
        {
            writerName.BaseStream.Position = writerPos;
            var adjustValBytes = new byte[4];

            switch (endianness)
            {
                case Endianness.LittleEndian:
                    adjustValBytes = BitConverter.GetBytes(adjustVal);
                    break;

                case Endianness.BigEndian:
                    adjustValBytes = BitConverter.GetBytes(adjustVal);
                    Array.Reverse(adjustValBytes, 0, adjustValBytes.Length);
                    break;
            }

            writerName.Write(adjustValBytes);
        }

    }
}