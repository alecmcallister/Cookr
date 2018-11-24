using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookr.Logic
{
    public class RecipeObject
    {
        public string title { get; set; }
        public bool popularToday { get; set; }
        public ArrayList tags { get; set; }
        
        public RecipeObject(string _title, bool _popularToday, ArrayList _tags)
        {
            title = _title;
            popularToday = _popularToday;
            tags = _tags;
        }
        
    }
}
