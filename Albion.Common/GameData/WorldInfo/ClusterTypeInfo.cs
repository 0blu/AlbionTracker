using Albion.Common.Xml;
using System.Xml;

namespace Albion.Common.GameData.WorldInfo
{
    public class ClusterTypeInfo
    {
        public string Name
        {
            get;
            private set;
        }

        public ArcheTypeInfo ArcheType
        {
            get;
            private set;
        }

        private string _internalArcheType;

        private ClusterTypeInfo()
        {

        }

        public static ClusterTypeInfo ParseFromXml(XmlElement element)
        {
            ClusterTypeInfo info = new ClusterTypeInfo();

            info.Name = XmlUtils.GetXmlAttributeString(element, "name");
            info._internalArcheType = XmlUtils.GetXmlAttributeString(element, "archetype");

            return info;
        }

        internal void PostProcess(WorldData worldData)
        {
            ArcheType = worldData.GetArchType(_internalArcheType);
        }
    }
}
