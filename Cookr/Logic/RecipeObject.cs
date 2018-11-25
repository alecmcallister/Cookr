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
        public string title { get; set; }

        [XmlElement("PopularToday")]
        public bool popularToday { get; set; }

        [XmlArray("Tags"), XmlArrayItem("tag")]
        public List<string> tags { get; set; }

        public RecipeObject()
        {
            // XML parser and Serialization need this parameterless constructor
        }
        
        public RecipeObject(string _title, bool _popularToday, List<string> _tags)
        {
            title = _title;
            popularToday = _popularToday;
            tags = _tags;
        }
        
    }
}
