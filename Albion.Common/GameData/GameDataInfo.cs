using System.Xml;
using Albion.Common.Xml;

namespace Albion.Common.GameData
{
    public abstract class GameDataInfo<T> where T : GameDataInfo<T>, new()
    {
        private static long _globalTypeCounter;

        public readonly long Type = _globalTypeCounter++;

        public string Name { protected set; get; }

        public static void ResetGlobalTypeCounter()
        {
            _globalTypeCounter = 0;
        }

        public static T CreateByElement(XmlElement rootElement)
        {
            var info = new T();
            info.ParseFrom(rootElement);
            return info;
        }

        protected virtual void ParseFrom(XmlElement rootElement)
        {
            Name = XmlUtils.GetXmlAttributeString(rootElement, "uniquename");
        }
    }
}
