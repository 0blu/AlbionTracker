using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Albion.Common.Math;
using Albion.Common.Xml;

namespace Albion.Common.GameData.Tuning
{
    public class TuningData : AlbionXmlData
    {
        public ServerSettings ServerSettings
        {
            get;
            private set;
        }

        private Dictionary<string, ClusterDangerBonusType> _clusterDangerBonusTypes;

        internal override void LoadDataFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "ServerSettings":
                        ServerSettings = new ServerSettings(element);
                        break;
                    case "ClusterDangerBonusTypes":
                        ParseClusterDangerBonusTypes(element);
                        break;
                }
            }
        }

        internal override void PostProcess(GameData gameData)
        {
        }

        private void ParseClusterDangerBonusTypes(XmlElement rootElement)
        {
            _clusterDangerBonusTypes = new Dictionary<string, ClusterDangerBonusType>();

            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                if (xmlNode.Name != "ClusterDangerBonus")
                    throw new Exception("Expected ClusterDangerBonus");

                var data = new ClusterDangerBonusType
                (
                    XmlUtils.GetXmlAttributeString(element, "type"),
                    XmlUtils.GetXmlAttributeFixPoint(element, "famefactor", FixPoint.One),
                    XmlUtils.GetXmlAttributeFixPoint(element, "moblootfactor", FixPoint.One),
                    XmlUtils.GetXmlAttributeFixPoint(element, "mobsilverlootfactor", FixPoint.One)
                );

                _clusterDangerBonusTypes.Add(data.Type, data);
            }
        }

        public ClusterDangerBonusType GetClusterDangerBonusType(string type)
        {
            return _clusterDangerBonusTypes.TryGetValue(type, out var bonusType) ? bonusType : null;
        }

        public FixPoint GetGroupBonusFactor(int groupSize)
        {
            return ServerSettings.GroupFameBonus.LastOrDefault(info => info.Size <= groupSize)?.Bonus ?? FixPoint.One;
        }

        public class ClusterDangerBonusType
        {
            public readonly string Type;
            public readonly FixPoint FameFactor;
            public readonly FixPoint MobLootFactor;
            public readonly FixPoint MobSilverLootFactor;

            public ClusterDangerBonusType(string type, FixPoint fameFactor, FixPoint mobLootFactor, FixPoint mobSilverLootFactor)
            {
                Type = type;
                FameFactor = fameFactor;
                MobLootFactor = mobLootFactor;
                MobSilverLootFactor = mobSilverLootFactor;
            }
        }
    }
}
