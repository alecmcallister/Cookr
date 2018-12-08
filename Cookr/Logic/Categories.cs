using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookr
{
    [Serializable, XmlRoot("Categories")]
    public class Categories
    {
        public struct Category
        {
            [XmlElement("Name")]
            public string name;

            [XmlElement("Image")]
            public string image;
        }

        [XmlElement("Category")]
        public List<Category> ListedCategories;

    }
}
