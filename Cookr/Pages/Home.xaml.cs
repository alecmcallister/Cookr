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

			Searchbar.SearchEvent += (s => { NavigationManager.NavigateToSearch(s); Searchbar.Text = ""; });
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
	}
}
