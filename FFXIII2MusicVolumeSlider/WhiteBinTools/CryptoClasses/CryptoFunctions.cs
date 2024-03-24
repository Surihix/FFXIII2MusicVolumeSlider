namespace FFXIII2MusicVolumeSlider.WhiteBinTools.CryptoClasses
{
    internal static class CryptoFunctions
    {
        public static uint LoopAByte(this uint decryptedByte, byte[] xorTable, uint tableOffset)
        {
            var byteIterator = 0;

            while (byteIterator < 8)
            {
                int integerVal = IntegersArray.Integers[decryptedByte];

                var xorTableByte = xorTable[tableOffset + byteIterator];
                var computedValue = integerVal - xorTableByte;

                if (computedValue < 0)
                {
                    decryptedByte = (uint)computedValue & 0xFF;
                }
                else
                {
                    decryptedByte = (uint)computedValue;
                }

                byteIterator++;
            }

            return decryptedByte;
        }
    }
}