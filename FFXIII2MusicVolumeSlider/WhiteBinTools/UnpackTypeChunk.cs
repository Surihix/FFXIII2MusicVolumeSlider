using FFXIII2MusicVolumeSlider.WhiteBinTools.FilelistClasses;
using FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses;
using System.IO;
using static FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses.CmnEnums;

namespace FFXIII2MusicVolumeSlider.WhiteBinTools
{
    public class UnpackTypeChunk
    {
        public static void UnpackFilelistChunks(GameCodes gameCode, string filelistFile)
        {
            var filelistVariables = new FilelistVariables();

            FilelistProcesses.PrepareFilelistVars(filelistVariables, filelistFile);

            var filelistOutName = Path.GetFileName(filelistFile);
            filelistVariables.DefaultChunksExtDir = Path.Combine(filelistVariables.MainFilelistDirectory, "_chunks");
            filelistVariables.ChunkFile = Path.Combine(filelistVariables.DefaultChunksExtDir, "chunk_");
            var outChunkFile = Path.Combine(filelistVariables.MainFilelistDirectory, filelistOutName + ".txt");


            filelistVariables.DefaultChunksExtDir.IfDirExistsDel();
            Directory.CreateDirectory(filelistVariables.DefaultChunksExtDir);

            CmnMethods.IfFileExistsDel(outChunkFile);


            FilelistProcesses.DecryptProcess(gameCode, filelistVariables);

            using (var filelistStream = new FileStream(filelistVariables.MainFilelistFile, FileMode.Open, FileAccess.Read))
            {
                using (var filelistReader = new BinaryReader(filelistStream))
                {
                    FilelistChunksPrep.GetFilelistOffsets(filelistReader, filelistVariables);
                    FilelistChunksPrep.UnpackChunks(filelistStream, filelistVariables.ChunkFile, filelistVariables);
                }
            }

            if (filelistVariables.IsEncrypted)
            {
                CmnMethods.IfFileExistsDel(filelistVariables.TmpDcryptFilelistFile);
                filelistVariables.MainFilelistFile = filelistFile;
            }


            // Write all file paths strings
            // to a text file
            filelistVariables.ChunkFNameCount = 0;
            for (int cf = 0; cf < filelistVariables.TotalChunks; cf++)
            {
                var filesInChunkCount = FilelistProcesses.GetFilesInChunkCount(filelistVariables.ChunkFile + filelistVariables.ChunkFNameCount);

                // Open a chunk file for reading
                using (var currentChunkStream = new FileStream(filelistVariables.ChunkFile + filelistVariables.ChunkFNameCount, FileMode.Open, FileAccess.Read))
                {
                    using (var chunkStringReader = new BinaryReader(currentChunkStream))
                    {

                        using (var outChunkStream = new FileStream(outChunkFile, FileMode.Append, FileAccess.Write))
                        {
                            using (var outChunkWriter = new StreamWriter(outChunkStream))
                            {

                                var chunkStringReaderPos = (uint)0;
                                for (int f = 0; f < filesInChunkCount; f++)
                                {
                                    var convertedString = chunkStringReader.BinaryToString(chunkStringReaderPos);

                                    outChunkWriter.WriteLine(convertedString);

                                    chunkStringReaderPos = (uint)chunkStringReader.BaseStream.Position;
                                }
                            }
                        }
                    }
                }

                filelistVariables.ChunkFNameCount++;
            }

            Directory.Delete(filelistVariables.DefaultChunksExtDir, true);
        }
    }
}