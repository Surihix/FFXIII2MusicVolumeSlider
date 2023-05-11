using System;
using System.IO;

namespace FFXIII2MusicVolumeSlider.ProcessVol
{
    internal class AdjustVolume
    {
        public static void SCD(string langCodeVar, BinaryWriter writerNameVar, uint writerPosVar, string scdFileNameVar, int sliderValVar)
        {
            string[] scdListToUse = { };
            switch (langCodeVar)
            {
                case "u":
                    scdListToUse = SCDlist.XIII2musicList_us;
                    break;

                case "c":
                    scdListToUse = SCDlist.XIII2musicList_jp;
                    break;
            }

            foreach (var scd in scdListToUse)
            {
                var defaultScd = scd.Split(':');
                var defaultScdVol = Convert.ToSingle(defaultScd[0]);
                var defaultScdName = defaultScd[1];

                if (scdFileNameVar.Equals(defaultScdName))
                {
                    float volLvlToScale = 0;
                    float newVolLvl = 0;

                    switch (sliderValVar)
                    {
                        case 0:
                            break;

                        case 1:
                            volLvlToScale = Convert.ToSingle(0.40);
                            newVolLvl = defaultScdVol - volLvlToScale;
                            ClampValue(ClampType.low, ref newVolLvl);
                            break;

                        case 2:
                            volLvlToScale = Convert.ToSingle(0.30);
                            newVolLvl = defaultScdVol - volLvlToScale;
                            ClampValue(ClampType.low, ref newVolLvl);
                            break;

                        case 3:
                            volLvlToScale = Convert.ToSingle(0.20);
                            newVolLvl = defaultScdVol - volLvlToScale;
                            ClampValue(ClampType.low, ref newVolLvl);
                            break;

                        case 4:
                            volLvlToScale = Convert.ToSingle(0.10);
                            newVolLvl = defaultScdVol - volLvlToScale;
                            ClampValue(ClampType.low, ref newVolLvl);
                            break;

                        case 5:
                            newVolLvl = defaultScdVol;
                            break;

                        case 6:
                            volLvlToScale = Convert.ToSingle(0.05);
                            newVolLvl = defaultScdVol + volLvlToScale;
                            ClampValue(ClampType.high, ref newVolLvl);
                            break;

                        case 7:
                            volLvlToScale = Convert.ToSingle(0.10);
                            newVolLvl = defaultScdVol + volLvlToScale;
                            ClampValue(ClampType.high, ref newVolLvl);
                            break;

                        case 8:
                            volLvlToScale = Convert.ToSingle(0.15);
                            newVolLvl = defaultScdVol + volLvlToScale;
                            ClampValue(ClampType.high, ref newVolLvl);
                            break;

                        case 9:
                            volLvlToScale = Convert.ToSingle(0.20);
                            newVolLvl = defaultScdVol + volLvlToScale;
                            ClampValue(ClampType.high, ref newVolLvl);
                            break;

                        case 10:
                            newVolLvl = 2;
                            break;
                    }

                    writerNameVar.BaseStream.Position = writerPosVar;
                    writerNameVar.Write((Single)newVolLvl);
                }
            }
        }

        public static void ClampValue(ClampType clampTypeVar, ref float newVolVar)
        {
            switch (clampTypeVar)
            {
                case ClampType.low:
                    if (newVolVar < 0 || newVolVar.Equals(0))
                    {
                        newVolVar = Convert.ToSingle(0.10);
                    }
                    break;

                case ClampType.high:
                    if (newVolVar > 2 || newVolVar.Equals(2))
                    {
                        newVolVar = Convert.ToSingle(1.10);
                    }
                    break;
            }
        }

        public enum ClampType
        {
            low,
            high
        }
    }
}