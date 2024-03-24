using FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses;
using System;
using System.IO;

namespace FFXIII2MusicVolumeSlider.WhiteBinTools.CryptoClasses
{
    internal class CryptFilelist
    {
        public static void ProcessFilelist(string inFile)
        {
            using (var inFileReader = new BinaryReader(File.Open(inFile, FileMode.Open, FileAccess.Read)))
            {
                inFileReader.BaseStream.Position = 16;
                var cryptBodySizeVal = inFileReader.ReadBytes(4);
                Array.Reverse(cryptBodySizeVal);

                var cryptBodySize = BitConverter.ToUInt32(cryptBodySizeVal, 0);
                cryptBodySize += 8;

                var fileLength = (uint)inFileReader.BaseStream.Length;
                fileLength -= cryptBodySize;
                fileLength -= 32;

                uint remainderBytes = 0;

                if (fileLength > 0)
                {
                    remainderBytes = fileLength;
                }

                uint readPos = 32;
                uint writePos = 32;

                inFileReader.BaseStream.Position = 0;
                var baseSeedArray = inFileReader.ReadBytes(16);
                var seedArray8Bytes = (ulong)((baseSeedArray[9] << 24) | (baseSeedArray[12] << 16) | (baseSeedArray[2] << 8) | (baseSeedArray[0]));
                var seedArray = BitConverter.GetBytes(seedArray8Bytes);

                var xorTable = Generator.GenerateXORtable(seedArray, false);

                CmnMethods.IfFileExistsDel(inFile + ".dec");

                using (var decryptedStreamBinWriter = new BinaryWriter(File.Open(inFile + ".dec", FileMode.Append, FileAccess.Write)))
                {
                    inFileReader.BaseStream.Position = 0;
                    inFileReader.BaseStream.ExCopyTo(decryptedStreamBinWriter.BaseStream, 0, writePos);

                    var blockCount = cryptBodySize / 8;
                    Decryption.DecryptBlocks(xorTable, blockCount, readPos, writePos, inFileReader, decryptedStreamBinWriter);

                    inFileReader.BaseStream.Position = decryptedStreamBinWriter.BaseStream.Length;
                    inFileReader.BaseStream.ExCopyTo(decryptedStreamBinWriter.BaseStream, decryptedStreamBinWriter.BaseStream.Length, remainderBytes);
                }

                inFileReader.Dispose();

                CreateFinalFile(inFile, inFile + ".dec");
            }
        }


        static void CreateFinalFile(string ogFile, string processedFile)
        {
            var ogFileName = Path.GetFileName(ogFile);
            var ogFileDir = Path.GetDirectoryName(ogFile);
            var newFile = Path.Combine(ogFileDir, ogFileName);

            File.Delete(ogFile);
            File.Move(processedFile, newFile);
        }
    }
}