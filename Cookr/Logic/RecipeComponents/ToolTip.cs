using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cookr.Logic.RecipeComponents
{
    [Serializable]
    public class ToolTip
    {
        [XmlAttribute("Id")]
        public int Id;

        [XmlAttribute("Text")]
        public string Text;

        [XmlArray("Images"), XmlArrayItem("Image")]
        public List<string> Images;
    }
}
