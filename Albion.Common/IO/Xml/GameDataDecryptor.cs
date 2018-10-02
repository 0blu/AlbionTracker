using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Albion.Common.IO.Xml
{
    internal class GameDataDecryptor
    {
        private static readonly byte[] _rgbKey = { 48, 239, 114, 71, 66, 242, 4, 50 };

        private static readonly byte[] _rgbIv = { 14, 166, 220, 137, 219, 237, 220, 79 };

        private static Stream GetOpenFileStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static Stream GetDecryptedFileStream(string filePath)
        {
            var fileStream = GetOpenFileStream(filePath);
            DES des = new DESCryptoServiceProvider();
            var compressedStream = new CryptoStream(fileStream, des.CreateDecryptor(_rgbKey, _rgbIv), CryptoStreamMode.Read);
            return new GZipStream(compressedStream, CompressionMode.Decompress);
        }
    }
}
