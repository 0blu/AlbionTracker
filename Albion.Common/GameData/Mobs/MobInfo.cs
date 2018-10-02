using System.Xml;
using Albion.Common.Xml;

namespace Albion.Common.GameData.Mobs
{
    public class MobInfo : GameDataInfo<MobInfo>
    {
        public string AvatarRing { protected set; get; }

        protected override void ParseFrom(XmlElement rootElement)
        {
            base.ParseFrom(rootElement);
            AvatarRing = XmlUtils.GetXmlAttributeString(rootElement, "avatarring");
        }
    }
}
