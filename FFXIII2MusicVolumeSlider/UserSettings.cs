using System.IO;
using System.Xml.Serialization;

namespace FFXIII2MusicVolumeSlider
{
    public class UserSettings
    {
        public string ExePath { get; set; }

        public string VoiceOver { get; set; }

        public string FileSystem { get; set; }

        public int SliderValue { get; set; }

        public void SaveSettings()
        {
            using (FileStream xmlFileToSave = new FileStream("UserSettings.xml", FileMode.Create, FileAccess.Write))
            {
                var saveXmlData = new XmlSerializer(typeof(UserSettings));
                saveXmlData.Serialize(xmlFileToSave, this);
            }
        }

        public static UserSettings LoadSettings()
        {
            using (FileStream xmlFileToLoad = new FileStream("UserSettings.xml", FileMode.Open))
            {
                var loadXmlData = new XmlSerializer(typeof(UserSettings));
                return (UserSettings)loadXmlData.Deserialize(xmlFileToLoad);
            }
        }
    }
}