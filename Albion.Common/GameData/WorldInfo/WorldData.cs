using System.Collections.Generic;
using System.Xml;

namespace Albion.Common.GameData.WorldInfo
{
    public class WorldData : AlbionXmlData
    {
        private readonly Dictionary<string, ArcheTypeInfo> _archeTypes = new Dictionary<string, ArcheTypeInfo>();
        private readonly Dictionary<string, ClusterTypeInfo> _clusterTypes = new Dictionary<string, ClusterTypeInfo>();

        internal override void LoadDataFromXml(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                switch (element.Name)
                {
                    case "archetypes":
                        ParseArcheTypes(element);
                        break;
                    case "clustertypes":
                        ParseClusterTypes(element);
                        break;
                }
            }

            foreach (var clusterType in _clusterTypes.Values)
            {
                clusterType.PostProcess(this);
            }
        }

        internal override void PostProcess(GameData gameData)
        {
            foreach (var archeType in _archeTypes.Values)
            {
                archeType.PostProcess(gameData);
            }
        }

        private void ParseArcheTypes(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                ArcheTypeInfo info = ArcheTypeInfo.ParseFromXml(element);

                _archeTypes.Add(info.Name, info);
            }
        }

        private void ParseClusterTypes(XmlElement rootElement)
        {
            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {

                if (!(xmlNode is XmlElement element))
                    continue;

                ClusterTypeInfo info = ClusterTypeInfo.ParseFromXml(element);

                _clusterTypes.Add(info.Name, info);
            }
        }

        public ArcheTypeInfo GetArchType(string archName)
        {
            return _archeTypes.TryGetValue(archName, out var info) ? info : null;
        }

        public ClusterTypeInfo GetClusterType(string internalClusterType)
        {
            return _clusterTypes.TryGetValue(internalClusterType, out var info) ? info : null;
        }
    }
}
