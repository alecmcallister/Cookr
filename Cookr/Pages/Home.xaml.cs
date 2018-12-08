using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cookr.Categories;

namespace Cookr
{
	public partial class Home : Page
	{
		public Home()
		{
			InitializeComponent();
			Load_PopularToday();
            Load_Categories();


            ExpandCategoriesButton.ToggleButtonEvent += b => { ExpandTheThing(null, null); };
		}

        private void Load_Categories()
        {
            Categories categories = RecipeManager.GetCategories();
            foreach(Category c in categories.ListedCategories)
            {
                CategoryPictureStack.Children.Add(new RecipeCategoryCard(c));
            }

            BrutalCategoryHardcode();
            

        }

		private void Load_PopularToday()
		{
			List<RecipeObject> recipes = RecipeManager.GetRecipes();
			List<RecipeCard> popularToday = new List<RecipeCard>();
			recipes.ForEach(recipe =>
			{
				if (recipe.PopularToday)
					popularToday.Add(new RecipeCard(recipe));
			});
			popularToday.ForEach(recipe => PopularTodayStack.Children.Add(recipe));
		}

		#region Animation

		bool animating;
		bool expanded;
		IEasingFunction easeFunction = new CubicEase();
		float animationTime = 0.3f;

		async void ExpandTheThing(object sender, RoutedEventArgs e)
		{
			if (animating)
				return;

			animating = true;

			expanded = !expanded;
			double height = expanded ? 350f : 0f;

			await ChangeExpandableGridHeight(height);

			animating = false;
		}

		async Task ChangeExpandableGridHeight(double height)
		{
			await AllCategoriesExpandablePanel.AnimateDoubleProperty(HeightProperty, height, animationTime, easeFunction);
		}

        public struct CategoryGroup
        {
            public string GroupName;
            public List<string> categories;

        }

        /// <summary>
        /// Forgive me...
        /// </summary>
        private void BrutalCategoryHardcode()
        {

            List<CategoryGroup> CategoryGroups = new List<CategoryGroup>();

            CategoryGroups.Add(new CategoryGroup() {
                GroupName = "Time of Day",
                categories = new List<string>(new string[] { "Breakfast", "Lunch", "Dinner", "Dessert" })
            });
            CategoryGroups.Add(new CategoryGroup()
            {
                GroupName = "Ingredients",
                categories = new List<string>(new string[] { "Chicken", "Beef", "Pork", "Fish", "Rice", "Fruit", "Potatoes", "Turkey", "Egg", "Onion", "Tomato" })
            });
            CategoryGroups.Add(new CategoryGroup()
            {
                GroupName = "Cuisine",
                categories = new List<string>(new string[] { "Italian", "Indian", "Chinese", "French", "Greek", "Thai" })
            });
            CategoryGroups.Add(new CategoryGroup()
            {
                GroupName = "Other",
                categories = new List<string>(new string[] { "Salad", "Soup", "Pizza", "Pasta", "Seafood", "Spicy", "Quick" })
            });


            for (int i = 0; i < CategoryGroups.Count; i++)
            {
                // Add in a bunch of categories...
                TextBlock categoryGroup = new TextBlock();
                categoryGroup.FontSize = (double)FindResource("Text-Large");
                categoryGroup.Foreground = (Brush)FindResource("Text-Light-Brush");
                categoryGroup.Effect = (Effect)FindResource("DropShadow");
                categoryGroup.FontFamily = (FontFamily)FindResource("RobotoLight");
                categoryGroup.Text = CategoryGroups[i].GroupName;
                AllCategoriesStackPanel.Children.Add(categoryGroup);

                WrapPanel categoryButtonStack = new WrapPanel();
                categoryButtonStack.HorizontalAlignment = HorizontalAlignment.Left;
                categoryButtonStack.VerticalAlignment = VerticalAlignment.Top;
                categoryButtonStack.Orientation = Orientation.Horizontal;

                for (int c = 0; c < CategoryGroups[i].categories.Count; c++)
                {
                    CategoryTextButton categoryButton = new CategoryTextButton(CategoryGroups[i].categories[c]);
                    categoryButtonStack.Children.Add(categoryButton);
                }
                AllCategoriesStackPanel.Children.Add(categoryButtonStack);
            }
        }

		#endregion
	}
}
