using System.Xml;
using Albion.Common.Math;
using Albion.Common.Xml;

namespace Albion.Common.GameData.Characters
{
    public class CharacterData : AlbionXmlData
    {
        public FixPoint PremiumDestinyBoardProgressionFactor { get; private set; }

        internal override void LoadDataFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "Characters":
                        ParseCharactersFromXml(element);
                        break;
                }
            }
        }
        
        internal override void PostProcess(GameData gameData)
        {
        }

        protected void ParseCharactersFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "DefaultValues":
                        PremiumDestinyBoardProgressionFactor = XmlUtils.GetXmlAttributeFixPoint(element, "premiumdestinyboardprogressionfactor", FixPoint.One);
                        break;
                }
            }
        }
    }
}
