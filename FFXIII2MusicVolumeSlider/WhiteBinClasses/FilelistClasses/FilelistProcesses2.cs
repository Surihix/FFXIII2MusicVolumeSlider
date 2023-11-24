using FFXIII2MusicVolumeSlider.WhiteBinClasses.SupportClasses;
using System;
using System.Diagnostics;
using System.IO;

namespace FFXIII2MusicVolumeSlider.WhiteBinClasses.FilelistClasses
{
    public partial class FilelistProcesses
    {
        public static void PrepareFilelistVars(FilelistProcesses filelistVariables, string filelistFileVar)
        {
            filelistVariables.MainFilelistFile = filelistFileVar;

            var inFilelistFilePath = Path.GetFullPath(filelistVariables.MainFilelistFile);
            filelistVariables.MainFilelistDirectory = Path.GetDirectoryName(inFilelistFilePath);
            filelistVariables.TmpDcryptFilelistFile = Path.Combine(filelistVariables.MainFilelistDirectory, "filelist_tmp.bin");
        }


        public static void DecryptProcess(CmnEnums.GameCodes gameCodeVar, FilelistProcesses filelistVariables)
        {
            // Check for encryption header in the filelist file,
            // if the game code is set to ff13-1
            if (gameCodeVar.Equals(CmnEnums.GameCodes.ff131))
            {
                filelistVariables.IsEncrypted = CheckIfEncrypted(filelistVariables.MainFilelistFile);

                if (filelistVariables.IsEncrypted.Equals(true))
                {
                    if (Directory.Exists(filelistVariables.DefaultChunksExtDir))
                    {
                        Directory.Delete(filelistVariables.DefaultChunksExtDir, true);
                    }

                    Console.WriteLine("Error: Detected encrypted filelist file. set the game code to '-ff132' for handling this type of filelist");
                    IOhelpers.ErrorExit("");
                }
            }


            // If the ffxiiicrypt tool does not exist in app directory, then
            // throw a error and exit
            if (gameCodeVar.Equals(CmnEnums.GameCodes.ff132))
            {
                filelistVariables.IsEncrypted = CheckIfEncrypted(filelistVariables.MainFilelistFile);

                if (filelistVariables.IsEncrypted.Equals(true))
                {
                    if (!File.Exists("ffxiiicrypt.exe"))
                    {
                        Console.WriteLine("Error: Unable to locate ffxiiicrypt tool in the main app folder to decrypt the filelist file");

                        if (Directory.Exists(filelistVariables.DefaultChunksExtDir))
                        {
                            Directory.Delete(filelistVariables.DefaultChunksExtDir, true);
                        }

                        IOhelpers.ErrorExit("");
                    }
                }
            }


            // If the filelist is encrypted then decrypt the filelist file
            // by first creating a temp copy of the filelist 
            if (filelistVariables.IsEncrypted.Equals(true))
            {
                filelistVariables.TmpDcryptFilelistFile.IfFileExistsDel();
                File.Copy(filelistVariables.MainFilelistFile, filelistVariables.TmpDcryptFilelistFile);

                var cryptFilelistCode = " filelist";
                FFXiiiCryptTool(" -d ", "\"" + filelistVariables.TmpDcryptFilelistFile + "\"", ref cryptFilelistCode);

                filelistVariables.MainFilelistFile = filelistVariables.TmpDcryptFilelistFile;
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


        static void FFXiiiCryptTool(string actionSwitch, string filelistName, ref string actionType)
        {
            using (Process xiiiCrypt = new Process())
            {
                xiiiCrypt.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                xiiiCrypt.StartInfo.FileName = "ffxiiicrypt.exe";
                xiiiCrypt.StartInfo.Arguments = actionSwitch + filelistName + actionType;
                xiiiCrypt.StartInfo.UseShellExecute = true;
                xiiiCrypt.Start();
                xiiiCrypt.WaitForExit();
            }
        }
    }
}