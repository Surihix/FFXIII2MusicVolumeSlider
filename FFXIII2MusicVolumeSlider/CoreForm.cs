using FFXIII2MusicVolumeSlider.VolumeClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIII2MusicVolumeSlider
{
    public partial class CoreForm : Form
    {
        public CoreForm()
        {
            InitializeComponent();

            if (!File.Exists("AppHelp.txt"))
            {
                CmnMethods.AppMsgBox("The 'AppHelp.txt' file is missing.\nPlease ensure that this file is present next to the app to use the Help option.", "Warning", MessageBoxIcon.Warning);
            }

            if (!File.Exists("UserSettings.xml"))
            {
                DisableComponents();
                EnVoRadiobutton.Checked = true;
                PackedRadioButton.Checked = true;
                SliderTrackBar.Value = 5;
                SliderTrackBar.Select();

                CmnMethods.AppMsgBox("Please set the path of the 'FFXiii2Launcher.exe' file present in the FINAL FANTASY XIII-2 folder.", "Information", MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    UserSettings loadFromXml = UserSettings.LoadSettings();
                    PathTextBox.Text = loadFromXml.ExePath;
                    if (!File.Exists(PathTextBox.Text + "FFXiii2Launcher.exe"))
                    {
                        CmnMethods.AppMsgBox("Launcher executable file is not present in the location that was set before.\nPlease set the correct path of this file before setting the volume.", "Warning", MessageBoxIcon.Warning);
                        PathTextBox.Text = "";
                    }

                    switch (loadFromXml.VoiceOver)
                    {
                        case "en":
                            EnVoRadiobutton.Checked = true;
                            break;
                        case "jp":
                            JpVoRadiobutton.Checked = true;
                            break;
                        default:
                            CmnMethods.AppMsgBox("Voiceover setting saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            EnVoRadiobutton.Checked = true;
                            break;
                    }

                    switch (loadFromXml.FileSystem)
                    {
                        case "packed":
                            PackedRadioButton.Checked = true;
                            break;
                        case "nova":
                            NovaRadioButton.Checked = true;
                            break;
                        default:
                            CmnMethods.AppMsgBox("FileSystem setting saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            PackedRadioButton.Checked = true;
                            break;
                    }

                    switch (loadFromXml.SliderValue)
                    {
                        case 0:
                            SliderTrackBar.Value = 0;
                            break;
                        case 1:
                            SliderTrackBar.Value = 1;
                            break;
                        case 2:
                            SliderTrackBar.Value = 2;
                            break;
                        case 3:
                            SliderTrackBar.Value = 3;
                            break;
                        case 4:
                            SliderTrackBar.Value = 4;
                            break;
                        case 5:
                            SliderTrackBar.Value = 5;
                            break;
                        case 6:
                            SliderTrackBar.Value = 6;
                            break;
                        case 7:
                            SliderTrackBar.Value = 7;
                            break;
                        case 8:
                            SliderTrackBar.Value = 8;
                            break;
                        case 9:
                            SliderTrackBar.Value = 9;
                            break;
                        case 10:
                            SliderTrackBar.Value = 10;
                            break;
                        default:
                            CmnMethods.AppMsgBox("Slider value saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            SliderTrackBar.Value = 5;
                            break;
                    }

                    SliderTrackBar.Select();
                }
                catch (Exception)
                {
                    CmnMethods.AppMsgBox("Data entered in UserSettings file is corrupt.\nPlease re configure the settings again", "Warning", MessageBoxIcon.Warning);
                    CmnMethods.IfFileExistsDel("UserSettings.xml");
                    DisableComponents();
                    EnVoRadiobutton.Checked = true;
                    PackedRadioButton.Checked = true;
                    SliderTrackBar.Value = 5;
                    SliderTrackBar.Select();
                }
            }
        }


        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var xiii2LauncherExe = "FFXiii2Launcher.exe";
            OpenFileDialog pathSelect = new OpenFileDialog
            {
                FileName = xiii2LauncherExe,
                Filter = xiii2LauncherExe + $"|{xiii2LauncherExe}",
                RestoreDirectory = true
            };

            if (pathSelect.ShowDialog() == DialogResult.OK)
            {
                EnableComponents();

                var xiii2FilePath = pathSelect.FileName;
                var xiii2Folder = Path.GetDirectoryName(xiii2FilePath);

                PathTextBox.Text = xiii2Folder + "\\";
                SliderTrackBar.Select();
            }
        }


        public void EnableComponents()
        {
            EnVoRadiobutton.Enabled = true;
            JpVoRadiobutton.Enabled = true;
            PackedRadioButton.Enabled = true;
            NovaRadioButton.Enabled = true;
            SliderTrackBar.Enabled = true;
            SetVolumeButton.Enabled = true;
        }


        public void DisableComponents()
        {
            EnVoRadiobutton.Enabled = false;
            JpVoRadiobutton.Enabled = false;
            PackedRadioButton.Enabled = false;
            NovaRadioButton.Enabled = false;
            SliderTrackBar.Enabled = false;
            SetVolumeButton.Enabled = false;
        }


        public bool FilesCheck()
        {
            var appFilesDict = new Dictionary<int, (string name, string hash, string type)>
            {
                { 0, ("DotNetZip.dll", "8e9c0362e9bfb3c49af59e1b4d376d3e85b13aed0fbc3f5c0e1ebc99c07345f3", "dll" ) },
                { 1, ("ffxiiicrypt.exe", "f9dbc8ac8b367196e449fbb78df396d15aa5f7d8d07e1e295c4435b6c1192ce3", "exe") }
            };

            var allValid = true;
            for (int c = 0; c < 2; c++)
            {
                if (File.Exists(appFilesDict[c].name))
                {
                    byte[] hashArray;
                    string hash;
                    using (var checkStream = new FileStream(appFilesDict[c].name, FileMode.Open, FileAccess.Read))
                    {
                        using (var fileHash256 = SHA256.Create())
                        {
                            hashArray = fileHash256.ComputeHash(checkStream);
                            hash = BitConverter.ToString(hashArray).Replace("-", "").ToLower();
                        }

                        if (!hash.Equals(appFilesDict[c].hash))
                        {
                            allValid = false;
                            CmnMethods.AppMsgBox($"'{appFilesDict[c].name}' file is corrupt.\nPlease check if this Volume Slider program is properly downloaded.", "Error", MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                else
                {
                    CmnMethods.AppMsgBox($"The '{appFilesDict[c].name}' file is missing.\nPlease ensure that this {appFilesDict[c].type} file is present next to this app's executable file.", "Error", MessageBoxIcon.Error);
                    allValid = false;
                    break;
                }
            }

            return allValid;
        }


        private void SetVolumeButton_Click(object sender, EventArgs e)
        {
            DisableComponents();

            var albaPath = PathTextBox.Text;

            PathTextBox.ReadOnly = true;

            SetVolumeButton.Text = "Setting...";
            int SliderVal = SliderTrackBar.Value;

            SaveValuesToXml();

            if (PackedRadioButton.Checked.Equals(true))
            {
                var langCode = "";
                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    langCode = "u";
                }
                if (JpVoRadiobutton.Checked.Equals(true))
                {
                    langCode = "c";
                }

                Task.Run(() =>
                {
                    try
                    {
                        var filelistscrfile = albaPath + "alba_data\\sys\\filelist_scr" + langCode + ".win32.bin";
                        var whitescrFile = albaPath + "alba_data\\sys\\white_scr" + langCode + ".win32.bin";

                        if (File.Exists(filelistscrfile) && File.Exists(whitescrFile))
                        {
                            try
                            {
                                var allfilesValid = FilesCheck();

                                if (allfilesValid)
                                {
                                    PatchPrep.PackedMode(filelistscrfile, albaPath, langCode, whitescrFile, SliderVal);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            CmnMethods.AppMsgBox("Required game files are missing.\nPlease check if the voice over option that you have set in this app is available for your game.", "Error", MessageBoxIcon.Error);
                        }
                    }
                    finally
                    {
                        BeginInvoke(new Action(() => { EnableComponents(); }));
                        SetVolumeButton.BeginInvoke(new Action(() => SetVolumeButton.Text = "Set Volume"));
                        BeginInvoke(new Action(() => { PathTextBox.ReadOnly = false; }));
                        BeginInvoke(new Action(() => { SliderTrackBar.Select(); }));
                    }
                });
            }

            if (NovaRadioButton.Checked.Equals(true))
            {
                string[] scdListToUse = { };
                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    scdListToUse = SCDArrays.XIII2musicArray_us;
                }
                if (JpVoRadiobutton.Checked.Equals(true))
                {
                    scdListToUse = SCDArrays.XIII2musicArray_jp;
                }

                var unpackedMusicDir1 = "";
                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    unpackedMusicDir1 = PathTextBox.Text + "alba_data\\sound\\pack\\8000\\usa";
                }
                else if (JpVoRadiobutton.Checked.Equals(true))
                {
                    unpackedMusicDir1 = PathTextBox.Text + "alba_data\\sound\\pack\\8000";
                }

                var unpackedMusicDir2 = PathTextBox.Text + "alba_data\\sound\\pack\\8578";
                var unpackedMusicDir3 = PathTextBox.Text + "alba_data\\sound\\pack\\8593";

                if (Directory.Exists(unpackedMusicDir1) && Directory.Exists(unpackedMusicDir2) && Directory.Exists(unpackedMusicDir3))
                {
                    try
                    {
                        PatchPrep.NovaMode(unpackedMusicDir1, unpackedMusicDir2, unpackedMusicDir3, scdListToUse, SliderVal);
                    }
                    catch (Exception ex)
                    {
                        CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                    }
                }
                else
                {
                    CmnMethods.AppMsgBox("Unable to locate the unpacked music folders.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
                }

                EnableComponents();
                SetVolumeButton.Text = "Set Volume";
                PathTextBox.ReadOnly = false;
                SliderTrackBar.Select();
            }
        }


        public void SaveValuesToXml()
        {
            if (!File.Exists(PathTextBox.Text + "FFXiii2Launcher.exe"))
            {
                CmnMethods.AppMsgBox("Unable to locate Launcher executable file.\nPlease set the correct game path.", "Error", MessageBoxIcon.Error);
            }
            else
            {
                UserSettings saveXml = new UserSettings
                {
                    ExePath = PathTextBox.Text
                };

                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    saveXml.VoiceOver = "en";
                }

                if (JpVoRadiobutton.Checked.Equals(true))
                {
                    saveXml.VoiceOver = "jp";
                }

                if (PackedRadioButton.Checked.Equals(true))
                {
                    saveXml.FileSystem = "packed";
                }

                if (NovaRadioButton.Checked.Equals(true))
                {
                    saveXml.FileSystem = "nova";
                }

                int SliderVal = SliderTrackBar.Value;
                switch (SliderVal)
                {
                    case 0:
                        saveXml.SliderValue = 0;
                        break;
                    case 1:
                        saveXml.SliderValue = 1;
                        break;
                    case 2:
                        saveXml.SliderValue = 2;
                        break;
                    case 3:
                        saveXml.SliderValue = 3;
                        break;
                    case 4:
                        saveXml.SliderValue = 4;
                        break;
                    case 5:
                        saveXml.SliderValue = 5;
                        break;
                    case 6:
                        saveXml.SliderValue = 6;
                        break;
                    case 7:
                        saveXml.SliderValue = 7;
                        break;
                    case 8:
                        saveXml.SliderValue = 8;
                        break;
                    case 9:
                        saveXml.SliderValue = 9;
                        break;
                    case 10:
                        saveXml.SliderValue = 10;
                        break;
                }

                saveXml.SaveSettings();
            }
        }


        private void AboutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutForm appAbout = new AboutForm();
            System.Media.SystemSounds.Asterisk.Play();
            appAbout.ShowDialog();
        }


        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("AppHelp.txt"))
            {
                try
                {
                    Process.Start("AppHelp.txt");
                }
                catch (Exception ex)
                {
                    CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                }
            }
            else
            {
                CmnMethods.AppMsgBox("Unable to locate the help text file.\nPlease ensure that this text file is present next to the app before using this option.", "Error", MessageBoxIcon.Error);
            }
        }
    }
}