using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cookr.Logic {
    /// <summary>
    /// This class is a collection of methods required 
    /// </summary>
    public static class SearchEngine
    {
        public static Filter filter;

        public struct SearchObject
        {
            public RecipeObject recipe;
            public int searchCorr;
        }

        public enum SortBy
        {
            Rating,
            CookTime,
            Relevance
        }

        public struct Filter
        {
            public int minRating;
            public int maxTime;
            public ObservableCollection<string> mustIncludeIngredients;
            public ObservableCollection<string> mustExcludeIngredients;
            public SortBy sortBy;
        }

        static SearchEngine()
        {
            clearFilters();
        }

        public static void clearFilters()
        {
            filter.minRating = 1;
            filter.maxTime = int.MaxValue;
            filter.mustIncludeIngredients = new ObservableCollection<string>();
            filter.mustExcludeIngredients = new ObservableCollection<string>();
            filter.sortBy = SortBy.Rating;
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
            });



            IEnumerable<RecipeObject> recipeQuery =
                from searchObj in searched
                where searchObj.recipe.Rating >= filter.minRating
                && searchObj.recipe.TotalTime <= filter.maxTime
                && filter.mustIncludeIngredients.All(includeIngredient => searchObj.recipe.Ingredients.Any(ingredient => ingredient.Name.ToLower().Contains(includeIngredient.ToLower())))
                && filter.mustExcludeIngredients.All(excludeIngredient => searchObj.recipe.Ingredients.All(ingredient => !ingredient.Name.ToLower().Contains(excludeIngredient.ToLower())))
                orderby searchObj.searchCorr descending
                select searchObj.recipe;
            
            switch (filter.sortBy)
            {
                case SortBy.Rating:
                    filtered = recipeQuery.OrderByDescending(recipe => recipe.Rating).ToList();
                    break;
                case SortBy.CookTime:
                    filtered = recipeQuery.OrderBy(recipe => recipe.TotalTime).ToList();
                    break;
                case SortBy.Relevance:
                    // Already sorted by relevance
                default:
                    filtered = recipeQuery.ToList();
                    break;
            }

            return filtered;
        }
    }
}
