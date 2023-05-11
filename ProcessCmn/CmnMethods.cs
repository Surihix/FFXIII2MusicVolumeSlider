using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FFXIII2MusicVolumeSlider.ProcessCmn
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

        public static void FFXiiiCryptTool(string cryptDir, string action, string fileListName, ref string actionType)
        {
            using (Process xiiiCrypt = new Process())
            {
                xiiiCrypt.StartInfo.WorkingDirectory = cryptDir;
                xiiiCrypt.StartInfo.FileName = "ffxiiicrypt.exe";
                xiiiCrypt.StartInfo.Arguments = action + fileListName + actionType;
                xiiiCrypt.StartInfo.UseShellExecute = true;
                xiiiCrypt.Start();
                xiiiCrypt.WaitForExit();
            }
        }
    }
}