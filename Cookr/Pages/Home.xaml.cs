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

namespace Cookr.Pages
{
    public partial class Home : Page, CookrPage
    {
        public Home()
        {
            InitializeComponent();
            Load_PopularToday();
            BackBtn.Visibility = Visibility.Collapsed;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToSearch(SearchField.Text);
        }

        private void SearchField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchField.Text == "Search")
            {
                SearchField.Text = "";
                SearchField.Foreground = Brushes.Black;
            }
        }

        private void Load_PopularToday()
        {
            List<RecipeObject> recipes = RecipeManager.GetRecipes();
            List<RecipeCard> popularToday = new List<RecipeCard>();
            recipes.ForEach(recipe => {
                if (recipe.popularToday)
                    popularToday.Add(new RecipeCard(recipe));
            });
            popularToday.ForEach(recipe => PopularTodayStack.Children.Add(recipe));
        }

        private void SearchField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchField.Text == "")
            {
                SearchField.Text = "Search";
                SearchField.Foreground = Brushes.LightGray;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationManager.NavigateToSearch("Pizza");
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                NavigationManager.NavigateToSearch(SearchField.Text);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToPrev();
        }

        public void SetBackButton()
        {
            if (!NavigationManager.allowPrev())
            {
                BackBtn.Visibility = Visibility.Collapsed;
            }
            else
                BackBtn.Visibility = Visibility.Visible;
        }
    }
}
