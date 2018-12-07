using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cookr
{
    [Serializable]
    public class RecipeStep
    {
        public struct StepTips
        {
            [XmlAttribute("TargetText")]
            public string TargetText;

            [XmlAttribute("ToolTipID")]
            public int ToolTipID;
        }

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

        [XmlArray("StepTips"), XmlArrayItem("StepTip")]
        public List<StepTips> stepTips;
    }
}
