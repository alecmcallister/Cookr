using System;
using System.Xml.Serialization;

namespace Cookr
{
    [Serializable]
    public class Tool
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("ToolTipID")]
        public int ToolTipID;

    }
}
