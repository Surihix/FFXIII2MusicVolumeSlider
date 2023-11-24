using System.IO;

namespace FFXIII2MusicVolumeSlider.WhiteBinClasses.SupportClasses
{
    public static class IOhelpers
    {
        public static void IfDirExistsDel(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}