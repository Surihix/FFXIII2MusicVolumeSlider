using FFXIII2MusicVolumeSlider.WhiteBinClasses;
using FFXIII2MusicVolumeSlider.WhiteBinClasses.SupportClasses;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFXIII2MusicVolumeSlider.VolumeClasses
{
    internal class PatchPrep
    {
        public static void PackedMode(string filelistscrFileVar, string albaPathVar, string langCodeVar, string whitescrFileVar, int sliderValueVar)
        {
            UnpackTypeC.UnpackFilelistPaths(CmnEnums.GameCodes.ff132, filelistscrFileVar);

            var filelistscrPathsFile = albaPathVar + "alba_data\\sys\\filelist_scr" + langCodeVar + ".win32.txt";

            uint totalFileCount = (uint)File.ReadAllLines(filelistscrPathsFile).Count();
            totalFileCount -= 1;

            var albaMusicDir = "";
            string[] scdListToUse = { };
            switch (langCodeVar)
            {
                case "u":
                    albaMusicDir = "sound\\pack\\8000\\usa";
                    scdListToUse = SCDArrays.XIII2musicArray_us;
                    break;
                case "c":
                    albaMusicDir = "sound\\pack\\8000";
                    scdListToUse = SCDArrays.XIII2musicArray_jp;
                    break;
            }

            using (var scrPathsReader = new StreamReader(filelistscrPathsFile))
            {
                using (var scrBin = new FileStream(whitescrFileVar, FileMode.Open, FileAccess.Write))
                {
                    using (var scrBinWriter = new BinaryWriter(scrBin))
                    {

                        for (int m = 0; m < totalFileCount; m++)
                        {
                            string[] parsedFileLine = scrPathsReader.ReadLine().Split(':');
                            var fPos = Convert.ToUInt32(parsedFileLine[0], 16) * 2048;
                            var fPath = parsedFileLine[3];

                            var fDir = Path.GetDirectoryName(fPath);

                            if (fDir.Contains(albaMusicDir) || fDir.Contains("sound\\pack\\8578") || fDir.Contains("sound\\pack\\8593"))
                            {
                                var fname = Path.GetFileName(fPath);

                                AdjustVolume.SCD(scdListToUse, scrBinWriter, fPos + 168, fname, sliderValueVar);
                            }
                        }
                    }
                }
            }

            if (!File.Exists(albaPathVar + "alba_data\\sys\\FFXIII2MusicVolumeSlider.exe")
                && File.Exists(albaPathVar + "alba_data\\sys\\ffxiiicrypt.exe"))
            {
                CmnMethods.IfFileExistsDel(albaPathVar + "alba_data\\sys\\ffxiiicrypt.exe");
            }

            CmnMethods.IfFileExistsDel(albaPathVar + "alba_data\\sys\\filelist_scr" + langCodeVar + ".win32.txt");

            PatchSucess(sliderValueVar);
        }


        public static void NovaMode(string unpackedMusicDir1Var, string unpackedMusicDir2Var, string unpackedMusicDir3Var, string[] scdListToUse, int sliderValueVar)
        {
            string[] musicDir = Directory.GetFiles(unpackedMusicDir1Var, "*.scd", SearchOption.TopDirectoryOnly);
            string[] musicDir2 = Directory.GetFiles(unpackedMusicDir2Var, "*.scd", SearchOption.TopDirectoryOnly);
            string[] musicDir3 = Directory.GetFiles(unpackedMusicDir3Var, "*.scd", SearchOption.TopDirectoryOnly);

            if (musicDir.Length.Equals(0) || musicDir2.Length.Equals(0) || musicDir3.Length.Equals(0))
            {
                CmnMethods.AppMsgBox("One or more unpacked music folders are empty.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
                return;
            }

            PatchEachFile(musicDir, scdListToUse, sliderValueVar);
            PatchEachFile(musicDir2, scdListToUse, sliderValueVar);
            PatchEachFile(musicDir3, scdListToUse, sliderValueVar);

            PatchSucess(sliderValueVar);
        }


        static void PatchEachFile(string[] musicDirVar, string[] scdListToUse, int sliderValueVar)
        {
            foreach (var musicFile in musicDirVar)
            {
                var musicFileName = new FileInfo(musicFile).Name;

                using (var scdFile = new FileStream(musicFile, FileMode.Open, FileAccess.Write))
                {
                    using (var scdWriter = new BinaryWriter(scdFile))
                    {
                        AdjustVolume.SCD(scdListToUse, scdWriter, 168, musicFileName, sliderValueVar);
                    }
                }
            }
        }


        public static void PatchSucess(int sliderValueVar)
        {
            CmnMethods.AppMsgBox("Music volume is set to level " + sliderValueVar + ".", "Success", MessageBoxIcon.Information);
        }
    }
}