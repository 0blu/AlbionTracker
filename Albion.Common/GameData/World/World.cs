using System.Collections.Generic;
using System.Xml;

namespace Albion.Common.GameData.World
{
    public class World : AlbionXmlData
    {
        private readonly Dictionary<string, ClusterInfo> _clusterByName = new Dictionary<string, ClusterInfo>();

        internal override void LoadDataFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "clusters":
                        ParseClusters(element);
                        break;
                }
            }
        }

        internal override void PostProcess(GameData gameData)
        {
            foreach (var clusterInfo in _clusterByName.Values)
            {
                clusterInfo.PostProcess(gameData);
            }
        }

        private void ParseClusters(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                ClusterInfo info = ClusterInfo.ParseFromXml(element);

                _clusterByName.Add(info.Name, info);
            }
        }

        public ClusterInfo GetClusterByName(string clusterName)
        {
            return _clusterByName.TryGetValue(clusterName, out var info) ? info : null;
        }
    }
}
