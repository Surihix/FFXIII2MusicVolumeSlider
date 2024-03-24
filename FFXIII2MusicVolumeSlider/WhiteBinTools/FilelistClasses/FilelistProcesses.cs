using FFXIII2MusicVolumeSlider.WhiteBinTools.CryptoClasses;
using System;
using System.IO;
using static FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses.CmnEnums;

namespace FFXIII2MusicVolumeSlider.WhiteBinTools.FilelistClasses
{
    public class FilelistProcesses
    {
        public static void PrepareFilelistVars(FilelistVariables filelistVariables, string filelistFile)
        {
            filelistVariables.MainFilelistFile = filelistFile;

            var inFilelistFilePath = Path.GetFullPath(filelistVariables.MainFilelistFile);
            filelistVariables.MainFilelistDirectory = Path.GetDirectoryName(inFilelistFilePath);
            filelistVariables.TmpDcryptFilelistFile = Path.Combine(filelistVariables.MainFilelistDirectory, "filelist_tmp.bin");
        }


        public static void DecryptProcess(GameCodes gameCodeVar, FilelistVariables filelistVariables)
        {
            // Check if the filelist is encrypted
            // or not
            if (gameCodeVar.Equals(GameCodes.ff132))
            {
                filelistVariables.IsEncrypted = CheckIfEncrypted(filelistVariables.MainFilelistFile);
            }

            // Check if the filelist is in decrypted
            // state and the length of the cryptBody
            // for divisibility.
            // If the file was decrypted then skip
            // decrypting it.
            // If the filelist is encrypted then
            // decrypt the filelist file by first
            // creating a temp copy of the filelist.            
            if (filelistVariables.IsEncrypted)
            {
                var wasDecrypted = false;

                using (var encCheckReader = new BinaryReader(File.Open(filelistVariables.MainFilelistFile, FileMode.Open, FileAccess.Read)))
                {
                    encCheckReader.BaseStream.Position = 16;
                    var cryptBodySizeVal = encCheckReader.ReadBytes(4);
                    Array.Reverse(cryptBodySizeVal);

                    var cryptBodySize = BitConverter.ToUInt32(cryptBodySizeVal, 0);
                    encCheckReader.BaseStream.Position = 32 + cryptBodySize - 8;

                    if (encCheckReader.ReadUInt32() == cryptBodySize)
                    {
                        wasDecrypted = true;
                    }
                }

                switch (wasDecrypted)
                {
                    case true:
                        CmnMethods.IfFileExistsDel(filelistVariables.TmpDcryptFilelistFile);
                        File.Copy(filelistVariables.MainFilelistFile, filelistVariables.TmpDcryptFilelistFile);

                        filelistVariables.MainFilelistFile = filelistVariables.TmpDcryptFilelistFile;
                        break;

                    case false:
                        CmnMethods.IfFileExistsDel(filelistVariables.TmpDcryptFilelistFile);
                        File.Copy(filelistVariables.MainFilelistFile, filelistVariables.TmpDcryptFilelistFile);

                        CryptFilelist.ProcessFilelist(filelistVariables.TmpDcryptFilelistFile);
                        Console.WriteLine("Finished decrypting filelist file\n");

                        filelistVariables.MainFilelistFile = filelistVariables.TmpDcryptFilelistFile;
                        break;
                }
            }
        }


        public static bool CheckIfEncrypted(string filelistFileVar)
        {
            var isEncrypted = false;
            using (var encStream = new FileStream(filelistFileVar, FileMode.Open, FileAccess.Read))
            {
                using (var encStreamReader = new BinaryReader(encStream))
                {
                    encStreamReader.BaseStream.Position = 20;
                    var encHeaderNumber = encStreamReader.ReadUInt32();

                    if (encHeaderNumber == 501232760)
                    {
                        isEncrypted = true;
                    }
                }
            }

            return isEncrypted;
        }


        public static uint GetFilesInChunkCount(string chunkToRead)
        {
            var filesInChunkCount = (uint)0;
            using (var fileCountReader = new StreamReader(chunkToRead))
            {
                while (!fileCountReader.EndOfStream)
                {
                    var currentNullChar = fileCountReader.Read();
                    if (currentNullChar == 0)
                    {
                        filesInChunkCount++;
                    }
                }
            }

            return filesInChunkCount;
        }
    }
}