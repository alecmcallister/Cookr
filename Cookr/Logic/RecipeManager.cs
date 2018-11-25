using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookr.Logic
{
    public static class RecipeManager
    {
        private static List<RecipeObject> recipes;

        public static void LoadRecipes() {

            List<RecipeObject> loadedRecipes = new List<RecipeObject>();
            //loadedRecipes.Add(new RecipeObject("Asparagus Soup", true, new List<string> { "asparagus", "Soup" }));
            //loadedRecipes.Add(new RecipeObject("Lasgna", true, new List<String> { "lasgna", "mild" }));
            //loadedRecipes.Add(new RecipeObject("Chicken Burger", false, new List<String> { "burger", "Chicken" }));
            //loadedRecipes.Add(new RecipeObject("Pepperoni Pizza", true, new List<String> { "pizza", "cheese", "pepperoni" }));

            string mainPath = ".\\Recipes";

            IEnumerable<string> recipeFiles = Directory.EnumerateFiles(mainPath, "*.xml");

            foreach (string file in recipeFiles)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RecipeObject));
                using (FileStream fileStream = new FileStream(file, FileMode.Open))
                {
                    RecipeObject result = (RecipeObject)serializer.Deserialize(fileStream);
                    loadedRecipes.Add(result);
                }
            }

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
