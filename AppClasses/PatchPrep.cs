using FFXIII2MusicVolumeSlider.WhiteBinClasses;
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
            UnpackBin.FilePaths(filelistscrFileVar);

            var filelistscrPathsFile = albaPathVar + "alba_data\\sys\\filelist_scr" + langCodeVar + ".win32.txt";

            uint totalFileCount = 0;
            totalFileCount = (uint)File.ReadAllLines(filelistscrPathsFile).Count();
            totalFileCount--;

            var albaMusicDir = "";
            switch (langCodeVar)
            {
                case "u":
                    albaMusicDir = "sound\\pack\\8000\\usa";
                    break;
                case "c":
                    albaMusicDir = "sound\\pack\\8000";
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

                                AdjustVolume.SCD(langCodeVar, scrBinWriter, fPos + 168, fname, sliderValueVar);
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


        public static void NovaMode(string unpackedMusicDir1Var, string unpackedMusicDir2Var, string unpackedMusicDir3Var, string langCodeVar, int sliderValueVar)
        {
            string[] musicDir = Directory.GetFiles(unpackedMusicDir1Var, "*.scd", SearchOption.AllDirectories);
            string[] musicDir2 = Directory.GetFiles(unpackedMusicDir2Var, "*.scd", SearchOption.AllDirectories);
            string[] musicDir3 = Directory.GetFiles(unpackedMusicDir3Var, "*.scd", SearchOption.AllDirectories);

            if (musicDir.Length.Equals(0) || musicDir2.Length.Equals(0) || musicDir3.Length.Equals(0))
            {
                CmnMethods.AppMsgBox("Unpacked music folder is empty.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
                return;
            }

            PatchEachFile(musicDir, langCodeVar, sliderValueVar);
            PatchEachFile(musicDir2, langCodeVar, sliderValueVar);
            PatchEachFile(musicDir3, langCodeVar, sliderValueVar);

            PatchSucess(sliderValueVar);
        }

        static void PatchEachFile(string[] musicDirVar, string langCodeVar, int sliderValueVar)
        {
            foreach (var musicFile in musicDirVar)
            {
                var musicFileName = new FileInfo(musicFile).Name;

                using (var scdFile = new FileStream(musicFile, FileMode.Open, FileAccess.Write))
                {
                    using (var scdWriter = new BinaryWriter(scdFile))
                    {
                        AdjustVolume.SCD(langCodeVar, scdWriter, 168, musicFileName, sliderValueVar);
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