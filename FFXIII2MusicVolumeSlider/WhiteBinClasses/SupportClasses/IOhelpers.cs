using System;
using System.IO;

namespace FFXIII2MusicVolumeSlider.WhiteBinClasses.SupportClasses
{
    public static class IOhelpers
    {
        public static void ErrorExit(string errorMsg)
        {
            Console.WriteLine(errorMsg);
            Console.ReadLine();
            Environment.Exit(0);
        }


        public static void CheckFileExists(this string fileVar, string missingErrorMsg)
        {
            if (!File.Exists(fileVar))
            {
                Console.WriteLine(missingErrorMsg);
                ErrorExit("");
            }
        }


        public static void IfFileExistsDel(this string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        public static void IfDirExistsDel(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}