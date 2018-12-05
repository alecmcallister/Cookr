using Cookr.Logic;
using Cookr.UserControls;
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

namespace Cookr
{
	public partial class Home : Page, CookrPage
	{
		public Home()
		{
			InitializeComponent();
			Load_PopularToday();

			Searchbar.SearchEvent += s => { NavigationManager.NavigateToSearch(s); Searchbar.Text = ""; };
			ExpandCategoriesButton.ToggleButtonEvent += b => { ExpandTheThing(null, null); };
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

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			NavigationManager.NavigateToSearch("Pizza");
		}

		public void SetBackButton()
		{

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
			DoubleAnimation animation = new DoubleAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(animationTime)),
				From = AllCategoriesExpandablePanel.ActualHeight,
				To = height,
				EasingFunction = easeFunction
			};

			AllCategoriesExpandablePanel.BeginAnimation(HeightProperty, animation);

			await Task.Delay(TimeSpan.FromSeconds(animationTime));
		}

		#endregion
	}
}
