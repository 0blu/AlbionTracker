using System;
using System.Collections.Generic;
using System.Xml;
using Albion.Common.Math;
using Albion.Common.Time;
using Albion.Common.Xml;

namespace Albion.Common.GameData.Tuning
{
    public class ServerSettings
    {
        public IReadOnlyList<GroupFameBonusInfo> GroupFameBonus
        {
            get;
            private set;
        }

        public GameTimeSpan ClusterChangeCooldown
        {
            get;
            private set;
        }

        public ServerSettings(XmlElement rootElement)
        {
            ParseServerSettingsFromXml(rootElement);
        }

        private void ParseServerSettingsFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "Fame":
                        ParseFameElement(element);
                        break;
                    case "ClusterChange":
                        ClusterChangeCooldown = GameTimeSpan.FromSeconds(XmlUtils.GetXmlAttributeFloat(element, "cooldownseconds", 10));
                        break;
                }
            }
        }

        private void ParseFameElement(XmlElement rootElement)
        {
            if (rootElement.FirstChild == null)
                throw new NullReferenceException(nameof(rootElement.FirstChild));

            var list = new List<GroupFameBonusInfo>();

            foreach (XmlElement xmlElement in rootElement.FirstChild.ChildNodes)
            {
                var size = XmlUtils.GetXmlAttributeShort(xmlElement, "size", 1);
                var bonus = XmlUtils.GetXmlAttributeFixPoint(xmlElement, "bonusfactor", FixPoint.One);
                list.Add(new GroupFameBonusInfo(size, bonus));
            }
            list.Sort();

            GroupFameBonus = list;
        }

        public class GroupFameBonusInfo : IComparable<GroupFameBonusInfo>
        {
            public readonly short Size;
            public readonly FixPoint Bonus;

            public GroupFameBonusInfo(short size, FixPoint bonus)
            {
                Size = size;
                Bonus = bonus;
            }

            public int CompareTo(GroupFameBonusInfo other)
            {
                return Size.CompareTo(other.Size);
            }
        }
    }
}
