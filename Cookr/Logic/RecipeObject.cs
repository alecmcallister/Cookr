using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        double Rating { get; set; }

        [XmlElement("TitleImage")]
        string TitleImage { get; set; }

        /// <summary>
        /// The introductory blurb at the start of the recipe, e.g. "This simple but elegant soup is blah blah blah"
        /// </summary>
        [XmlElement("RecipeIntroduction")]
        string RecipeIntroduction { get; set; }

        /// <summary>
        /// The prep time for the recipe, in minutes.
        /// </summary>
        [XmlElement("PrepTime")]

        int PrepTime { get; set; }
        /// <summary>
        /// The cook time for the recipe, in minutes.
        /// </summary>
        [XmlElement("CookTime")]

        int CookTime { get; set; }
        /// <summary>
        /// The total time for the recipe (Prep time + cook time) in minutes.
        /// </summary>
        [XmlElement("TotalTime")]
        int TotalTime { get; set; }

        /// <summary>
        /// The number of servings (people fed) by the recipe.
        /// </summary>
        [XmlElement("NumberOfServings")]
        int NumberOfServings { get; set; }

        [XmlArray("Ingredients"), XmlArrayItem("Ingredient")]
        List<string> Ingredients { get; set; }

        [XmlArray("Tools"), XmlArrayItem("Tool")]
        List<string> Tools { get; set; }

        //TODO - Figure out Tooltips and RecipeSteps objects


        public RecipeObject()
        {
            // XML parser and Serialization need this parameterless constructor
        }

        //All objects: Title, PopularToday, Tags, Rating, TitleImage, RecipeIntroduction, PrepTime, CookTime, TotalTime, 
        //NumberOfServings, Ingredients, Tools
        public RecipeObject(string _title, bool _popularToday, List<string> _tags, double _rating, string _titleImage, 
            string _recipeIntroduction, int _prepTime, int _cookTime, int _totalTime, int _numberOfServings, 
            List<string> _ingredients, List<string> _tools)
        {
            Title = _title;
            PopularToday = _popularToday;
            Tags = _tags;
            Rating = _rating;
            TitleImage = _titleImage;
            RecipeIntroduction = _recipeIntroduction;
            PrepTime = _prepTime;
            CookTime = _cookTime;
            TotalTime = _totalTime;
            NumberOfServings = _numberOfServings;
            Ingredients = _ingredients;
            Tools = _tools;
        }
        
    }
}
