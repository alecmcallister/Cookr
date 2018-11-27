using System;
using System.Xml.Serialization;

namespace Cookr.Logic.RecipeComponents
{
    [Serializable]
    public class Tool
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("ToolTipId")]
        public int ToolTipId;

    }
}
