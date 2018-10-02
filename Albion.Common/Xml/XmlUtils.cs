using Albion.Common.Math;
using System.Globalization;
using System.Xml;

namespace Albion.Common.Xml
{
    internal class XmlUtils
    {
        public static string GetXmlAttributeString(XmlElement xmlElement, string attributeName)
        {
            return xmlElement.GetAttribute(attributeName);
        }
        
        public static short GetXmlAttributeShort(XmlElement xmlElement, string attributeName, short defaultValue)
        {
            if (xmlElement.HasAttribute(attributeName))
                return short.Parse(xmlElement.Attributes[attributeName].InnerText, CultureInfo.InvariantCulture.NumberFormat);
            return defaultValue;
        }

        public static FixPoint GetXmlAttributeFixPoint(XmlElement xmlElement, string attributeName, FixPoint defaultValue)
        {
            if (xmlElement.HasAttribute(attributeName))
                return FixPoint.FromFloatingPointValue(double.Parse(xmlElement.Attributes[attributeName].InnerText, CultureInfo.InvariantCulture.NumberFormat));
            return defaultValue;
        }

        public static float GetXmlAttributeFloat(XmlElement xmlElement, string attributeName, short defaultValue)
        {
            if (xmlElement.HasAttribute(attributeName))
                return float.Parse(xmlElement.Attributes[attributeName].InnerText, CultureInfo.InvariantCulture.NumberFormat);
            return defaultValue;
        }
    }
}
