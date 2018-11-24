using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookr.Logic
{
    public static class RecipeManager
    {
        private static List<RecipeObject> recipes;

        public static void LoadRecipes() {
            /// For now this function just generated a list with 5 recipe objects
            /// It will be replaced by a XML reader

            List<RecipeObject> loadedRecipes = new List<RecipeObject>();
            loadedRecipes.Add(new RecipeObject("Asparagus Soup", true, new List<string> { "asparagus", "Soup" }));
            loadedRecipes.Add(new RecipeObject("Lasgna", true, new List<String> { "lasgna", "mild" }));
            loadedRecipes.Add(new RecipeObject("Chicken Burger", false, new List<String> { "burger", "Chicken" }));
            loadedRecipes.Add(new RecipeObject("Pizza", true, new List<String> { "pizza", "cheese" }));

            recipes = loadedRecipes;

            return;

        }

        public static List<RecipeObject> GetRecipes()
        {
            if (recipes == null)
                LoadRecipes();
            return recipes;
        }
    }
}
