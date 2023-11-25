using Ionic.Zlib;
using System.IO;

namespace FFXIII2MusicVolumeSlider.WhiteBinTools.SupportClasses
{
    public static class ZlibFunctions
    {
        public static void ZlibDecompress(this Stream cmpStreamName, Stream outStreamName)
        {
            using (ZlibStream decompressor = new ZlibStream(cmpStreamName, CompressionMode.Decompress))
            {
                decompressor.CopyTo(outStreamName);
            }
        }
    }
}