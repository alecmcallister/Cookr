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
			double height = expanded ? 150f : 0f;

			await ChangeExpandableGridHeight(height);

			animating = false;
		}

		async Task ChangeExpandableGridHeight(double height)
		{
			await AllCategoriesExpandablePanel.AnimateDoubleProperty(HeightProperty, height, animationTime, easeFunction);
		}

		#endregion
	}
}
