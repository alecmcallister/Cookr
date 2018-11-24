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
    public partial class Search : Page
    {
        String searchString;
        List<RecipeObject> searchResults;

        public Search()
        {
            InitializeComponent();
        }
        public Search(String _search)
        {
            InitializeComponent();

            searchString = _search;
            SearchedLabel.Content = "\"" + searchString + "\"";
            search(searchString);
            LoadSearchResults(searchResults);
            
        }

        private void search(string searchString)
        {
            searchString = searchString.ToLower();
            string[] delimiters = { " ", "," };
            string[] tagStrings = searchString.Split(delimiters, StringSplitOptions.None);
            List<string> tags = new List<string>(tagStrings);

            searchResults = SearchEngine.TagSearch(RecipeManager.GetRecipes(),tags);
        }

        private void LoadSearchResults(List<RecipeObject> searchResults)
        {
            if (searchResults.Count == 0)
            {
                TextBlock noResults = new TextBlock();
                noResults.Text = "Sorry! No recipes were found!!";
                SearchResultsStack.Children.Add(noResults);
                return;
            }
            searchResults.ForEach(recipe => SearchResultsStack.Children.Add(new RecipeCard(recipe)));
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Search(SearchField.Text));
        }
    }
}
