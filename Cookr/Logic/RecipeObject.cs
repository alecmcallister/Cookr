﻿using Cookr.Logic.RecipeComponents;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cookr.Logic
{
    [Serializable, XmlRoot("Recipe")]
    public class RecipeObject
    {
        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("PopularToday")]
        public bool PopularToday { get; set; }

        [XmlArray("Tags"), XmlArrayItem("Tag")]
        public List<string> Tags { get; set; }

        [XmlElement("Rating")]
        public double Rating { get; set; }

        [XmlElement("TitleImage")]
        public string TitleImage { get; set; }

        [XmlElement("RecipeIntroduction")]
        public string RecipeIntroduction { get; set; }

        [XmlElement("PrepTime")]
        public int PrepTime { get; set; }

        [XmlElement("CookTime")]
        public int CookTime { get; set; }

        [XmlElement("TotalTime")]
        public int TotalTime { get; set; }

        [XmlElement("NumberOfServings")]
        public int NumberOfServings { get; set; }

        [XmlArray("Ingredients"), XmlArrayItem("Ingredient")]
        public List<Ingredient> Ingredients { get; set; }

        [XmlArray("Tools"), XmlArrayItem("Tool")]
        public List<Tool> Tools { get; set; }

        [XmlArray("ToolTips"), XmlArrayItem("ToolTip")]
        public List<ToolTip> ToolTips { get; set; }

        [XmlArray("RecipeSteps"), XmlArrayItem("RecipeStep")]
        public List<RecipeStep> RecipeSteps { get; set; }
    }
}