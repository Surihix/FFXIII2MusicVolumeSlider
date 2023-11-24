using System.IO;
using System.Windows.Forms;

namespace FFXIII2MusicVolumeSlider
{
    internal class CmnMethods
    {
        public static void AppMsgBox(string msg, string msgHeader, MessageBoxIcon msgType)
        {
            MessageBox.Show(msg, msgHeader, MessageBoxButtons.OK, msgType);
        }

        public static void IfFileExistsDel(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}