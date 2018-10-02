using Albion.Common.GameData.Tuning;
using Albion.Common.Xml;
using System.Xml;

namespace Albion.Common.GameData.WorldInfo
{
    public class ArcheTypeInfo
    {
        public string Name
        {
            get;
            private set;
        }

        public TuningData.ClusterDangerBonusType ClusterDangerBonusType
        {
            get;
            private set;
        }

        private string _internalClusterDangerBonusType;

        private ArcheTypeInfo()
        {

        }

        public static ArcheTypeInfo ParseFromXml(XmlElement element)
        {
            ArcheTypeInfo info = new ArcheTypeInfo();

            info.Name = XmlUtils.GetXmlAttributeString(element, "name");
            info._internalClusterDangerBonusType = XmlUtils.GetXmlAttributeString(element, "clusterdangerbonustype");

            return info;
        }

        internal void PostProcess(GameData gameData)
        {
            ClusterDangerBonusType = gameData.TuningData.GetClusterDangerBonusType(_internalClusterDangerBonusType);
        }
    }
}
