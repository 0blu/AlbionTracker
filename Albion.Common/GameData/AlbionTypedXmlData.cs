using System.Collections.Generic;
using System.Xml;

namespace Albion.Common.GameData
{
    public abstract class AlbionTypedXmlData<T> : AlbionXmlData where T : GameDataInfo<T>, new()
    {
        protected Dictionary<string, T> InfoByName = new Dictionary<string, T>();
        protected List<T> InfoByType = new List<T>();

        public T GetByName(string name)
        {
            return InfoByName.TryGetValue(name, out var value) ? value : null;
        }

        public T GetByType(int type)
        {
            return InfoByType.Count > type && type >= 0 ? InfoByType[type] : null;
        }

        internal override void LoadDataFromXml(XmlElement rootElement)
        {
            ResetData();

            foreach (XmlNode xmlNode in rootElement.ChildNodes)
            {
                if (!(xmlNode is XmlElement element))
                    continue;

                var info = GameDataInfo<T>.CreateByElement(element);
                InfoByType.Add(info);
                InfoByName.Add(info.Name, info);
            }
        }

        private void ResetData()
        {
            GameDataInfo<T>.ResetGlobalTypeCounter();
            InfoByName = new Dictionary<string, T>();
            InfoByType = new List<T>();
        }

        internal override void PostProcess(GameData gameData)
        {
        }
    }
}
