using Cookr.Logic;
using Cookr.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

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

            buttonClearFilters.Click += ButtonClearFilters_Click;
            comboBoxSortBy.SelectionChanged += ComboBoxSortBy_SelectionChanged;
            comboBoxMinRating.SelectionChanged += ComboBoxMinRating_SelectionChanged;
            textBoxMaxCookHours.PreviewTextInput += TextBoxMaxCookHours_PreviewTextInput;
            textBoxMaxCookHours.TextChanged += TextBoxMaxCookHours_TextChanged;
            textBoxMaxCookMinutes.PreviewTextInput += TextBoxMaxCookMinutes_PreviewTextInput;
            textBoxMaxCookMinutes.TextChanged += TextBoxMaxCookMinutes_TextChanged;
            listBoxIncludeIngredients.ItemsSource = SearchEngine.filter.mustIncludeIngredients;
            listBoxIncludeIngredients.MouseDoubleClick += ListBoxIncludeIngredients_MouseDoubleClick;
            listBoxExcludeIngredients.ItemsSource = SearchEngine.filter.mustExcludeIngredients;
            listBoxExcludeIngredients.MouseDoubleClick += ListBoxExcludeIngredients_MouseDoubleClick;
            buttonIncludeIngredient.Click += ButtonIncludeIngredient_Click;
            buttonExcludeIngredient.Click += ButtonExcludeIngredient_Click;

            search(searchString);
            LoadSearchResults(searchResults);
            
        }

        private void ListBoxExcludeIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(listBoxExcludeIngredients.SelectedItem != null)
            {
                SearchEngine.filter.mustExcludeIngredients.Remove((string)listBoxExcludeIngredients.SelectedItem);
                search(searchString);
                LoadSearchResults(searchResults);
            }
        }

        private void ListBoxIncludeIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxIncludeIngredients.SelectedItem != null)
            {
                SearchEngine.filter.mustIncludeIngredients.Remove((string)listBoxIncludeIngredients.SelectedItem);
                search(searchString);
                LoadSearchResults(searchResults);
            }
        }

        private void TextBoxMaxCookMinutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterMaxCookTime();
        }

        private void TextBoxMaxCookMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void TextBoxMaxCookHours_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterMaxCookTime();
        }

        private void TextBoxMaxCookHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void updateFilterMaxCookTime()
        {
            string hours = textBoxMaxCookHours.Text;
            string minutes = textBoxMaxCookMinutes.Text;

            if (hours.Equals("") && minutes.Equals(""))
            {
                SearchEngine.filter.maxTime = int.MaxValue;
            }
            else if (hours.Equals(""))
            {
                SearchEngine.filter.maxTime = Convert.ToInt32(minutes);
            }
            else if (minutes.Equals(""))
            {
                SearchEngine.filter.maxTime = 60 * Convert.ToInt32(hours);
            }
            else
            {
                SearchEngine.filter.maxTime = 60 * Convert.ToInt32(hours) + Convert.ToInt32(minutes);
            }

            search(searchString);
            LoadSearchResults(searchResults);
        }

        private void ButtonExcludeIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = textBoxExcludeIngredient.Text;
            textBoxExcludeIngredient.Text = "";
            SearchEngine.filter.mustExcludeIngredients.Add(ingredient);

            search(searchString);
            LoadSearchResults(searchResults);
        }

        private void ButtonIncludeIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = textBoxIncludeIngredient.Text;
            textBoxIncludeIngredient.Text = "";
            SearchEngine.filter.mustIncludeIngredients.Add(ingredient);

            search(searchString);
            LoadSearchResults(searchResults);
        }

        private void ComboBoxMinRating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchEngine.filter.minRating = Convert.ToInt32(((ComboBoxItem)(comboBoxMinRating.SelectedItem)).Content);
            search(searchString);
            LoadSearchResults(searchResults);
        }

        private void ButtonClearFilters_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSortBy.SelectedIndex = 0;
            comboBoxMinRating.SelectedIndex = 0;
            textBoxMaxCookHours.Text = "";
            textBoxMaxCookMinutes.Text = "";

            SearchEngine.clearFilters();
            search(searchString);
            LoadSearchResults(searchResults);
        }

        private void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchEngine.SortBy sortBy;
            if (comboBoxSortBy.SelectedItem.Equals("Rating"))
            {
                sortBy = SearchEngine.SortBy.Rating;
            }
            else if (comboBoxSortBy.SelectedItem.Equals("Cook Time"))
            {
                sortBy = SearchEngine.SortBy.CookTime;
            }
            else if (comboBoxSortBy.SelectedItem.Equals("Relevance"))
            {
                sortBy = SearchEngine.SortBy.Relevance;
            }
            else
            {
                sortBy = SearchEngine.SortBy.Relevance;
            }

            SearchEngine.filter.sortBy = sortBy;
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
            SearchResultsStack.Children.Clear();
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
