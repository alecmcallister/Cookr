using System;
using System.Xml.Serialization;

namespace Cookr.Logic.RecipeComponents
{
    [Serializable]
    public class Ingredient
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("Quantity")]
        public string Quanitity;

        [XmlAttribute("Optional")]
        public bool Optional;

        [XmlAttribute("ToolTipID")]
        public int ToolTipID;

    }
}
