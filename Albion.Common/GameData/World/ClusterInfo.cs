using System.Xml;
using Albion.Common.GameData.WorldInfo;
using Albion.Common.Xml;

namespace Albion.Common.GameData.World
{
    public class ClusterInfo
    {
        public string Name
        {
            get;
            private set;
        }
        
        public string DisplayName
        {
            get;
            private set;
        }

        public ClusterTypeInfo ClusterType
        {
            get;
            private set;
        }

        private string _internalClusterType;

        private ClusterInfo()
        {

        }
        
        public static ClusterInfo ParseFromXml(XmlElement element)
        {
            ClusterInfo info = new ClusterInfo();

            info.Name = XmlUtils.GetXmlAttributeString(element, "id");
            info.DisplayName = XmlUtils.GetXmlAttributeString(element, "displayname");
            info._internalClusterType = XmlUtils.GetXmlAttributeString(element, "type");

            return info;
        }

        internal void PostProcess(GameData gameData)
        {
            ClusterType = gameData.WorldData.GetClusterType(_internalClusterType);
        }
    }
}
