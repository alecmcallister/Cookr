using Cookr.Logic;
using Cookr.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cookr.Pages
{
    public partial class Search : Page, CookrPage
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
            SearchField.Text = searchString;
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
            NavigationManager.NavigateToHome();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToSearch(SearchField.Text);
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                NavigationManager.NavigateToSearch(SearchField.Text);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
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
