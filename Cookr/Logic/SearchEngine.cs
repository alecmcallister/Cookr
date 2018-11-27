using System.Collections.Generic;
using System.Linq;

namespace Cookr.Logic {
    /// <summary>
    /// This class is a collection of methods required 
    /// </summary>
    public static class SearchEngine
    {
        public struct SearchObject
        {
            public RecipeObject recipe;
            public int searchCorr;
        }

        public static List<RecipeObject> TagSearch(List<RecipeObject> _recipes, List<string> _tags)
        {
            List<RecipeObject> filtered = new List<RecipeObject>();

            List<SearchObject> searched = new List<SearchObject>();

            _recipes.ForEach(recipe => {
                if (recipe.Tags.Any(tag => _tags.Contains(tag)))
                {
                    searched.Add(new SearchObject()
                    {
                        recipe = recipe,
                        searchCorr = recipe.Tags.Count(_tags.Contains)
                    });
                }
                searched = searched.OrderByDescending(x => x.searchCorr).ToList();
            });

            searched.ForEach(searchObj => {
                filtered.Add(searchObj.recipe);
            });

            return filtered;
        }
    }
}
