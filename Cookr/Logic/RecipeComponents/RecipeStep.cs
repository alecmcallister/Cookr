using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cookr.Logic.RecipeComponents
{
    [Serializable]
    public class RecipeStep
    {
        [XmlAttribute("Type")]
        public string Type;

        [XmlAttribute("Number")]
        public int Number;

        [XmlElement("Warning")]
        public string Warning;

        [XmlElement("StepText")]
        public string StepText;

        [XmlElement("Title")]
        public string Title;

        [XmlArray("Images"), XmlArrayItem("image")]
        public List<string> Images;
    }
}
