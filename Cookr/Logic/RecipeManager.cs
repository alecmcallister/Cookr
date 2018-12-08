using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookr
{
    public static class RecipeManager
    {
        private static List<RecipeObject> recipes;
        private static Categories categories;

        public static void LoadRecipes() {

            List<RecipeObject> loadedRecipes = new List<RecipeObject>();
            Categories loadedCategories = new Categories();

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

            string CategoriesFile = ".\\Categories.xml";

           // IEnumerable<string> categoryFile = Directory.EnumerateFiles(mainPath, "*.xml");

            foreach (string file in recipeFiles)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Categories));
                using (FileStream fileStream = new FileStream(CategoriesFile, FileMode.Open))
                {
                    Categories result = (Categories)serializer.Deserialize(fileStream);
                    loadedCategories = result;
                }
            }

            recipes = loadedRecipes;
            categories = loadedCategories;
            return;

        }

        public static List<RecipeObject> GetRecipes()
        {
            if (recipes == null)
                LoadRecipes();
            return recipes;
        }

        public static Categories GetCategories()
        {
            if (categories == null)
                LoadRecipes();
            return categories;
        }
    }
}
