using System;
using System.IO;
using System.Xml;
using Albion.Common.IO.Xml;

namespace Albion.Common.GameData
{
    public abstract class AlbionXmlData
    {
        internal void LoadFromXmlFile(string filePath)
        {
            var realFilePath = ToAlbionBinPath(filePath);

            if (!File.Exists(realFilePath))
                throw new FileNotFoundException($"Can't find file: '{realFilePath}'");

            var document = new XmlDocument();

            var fileStream = GameDataDecryptor.GetDecryptedFileStream(realFilePath);

            try
            {
                document.Load(fileStream);
            }
            catch (XmlException innerException)
            {
                throw new Exception("Unable to parse XML", innerException);
            }

            LoadDataFromXml(document.DocumentElement);
        }

        internal abstract void LoadDataFromXml(XmlElement rootElement);

        internal virtual void PostProcess(GameData gameData)
        {

        }

        private string ToAlbionBinPath(string filePath)
        {
            return Path.ChangeExtension(filePath, ".bin");
        }
    }

}
