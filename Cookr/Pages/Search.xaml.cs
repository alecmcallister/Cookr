using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Cookr
{
	public partial class Search : Page
	{
		string _currentSearch;
		public string CurrentSearch { get { return _currentSearch; } set { _currentSearch = value; ResultText = value; } }

		public string ResultText { get { return SearchResultText.Text; } set { SearchResultText.Text = '"' + value + '"'; } }

		List<RecipeObject> searchResults;

		public Search()
		{
			InitializeComponent();
			CurrentSearch = "";

			Subscribe();

			#region OLD CODE: CHANGE THIS

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

			#endregion
		}

		void Subscribe()
		{
			ExpandButton.ToggleButtonEvent += async (b) => { await ToggleFilterPanelVisibility(b); };

			FilterControl.SortByEvent += s => { SearchEngine.filter.sortBy = s; UpdateSearchResults(); };
			FilterControl.MinStarRatingEvent += r => { SearchEngine.filter.minRating = r;UpdateSearchResults(); };
		}

		public void DoSearch(string value)
		{
			CurrentSearch = value.ToLower();

			string[] delimiters = { " ", "," };
			string[] tagStrings = CurrentSearch.Split(delimiters, StringSplitOptions.None);
			List<string> tags = new List<string>(tagStrings);

			searchResults = SearchEngine.TagSearch(RecipeManager.GetRecipes(), tags);
			LoadSearchResults(searchResults);
		}

		public void UpdateSearchResults()
		{
			DoSearch(CurrentSearch);
		}

		#region Animation

		double filterPanelExpandedHeight = 235f;
		float animationTime = 0.3f;
		IEasingFunction ease = new CubicEase();

		async Task ToggleFilterPanelVisibility(bool visible)
		{
			await ExpandableGrid.AnimateDoubleProperty(HeightProperty, visible ? filterPanelExpandedHeight : 0f, animationTime, ease);
		}

		#endregion

		#region HOLY SHIT CHANGE THIS

		void ListBoxExcludeIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listBoxExcludeIngredients.SelectedItem != null)
			{
				SearchEngine.filter.mustExcludeIngredients.Remove((string)listBoxExcludeIngredients.SelectedItem);
				UpdateSearchResults();
			}
		}

		void ListBoxIncludeIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listBoxIncludeIngredients.SelectedItem != null)
			{
				SearchEngine.filter.mustIncludeIngredients.Remove((string)listBoxIncludeIngredients.SelectedItem);
				UpdateSearchResults();
			}
		}

		void TextBoxMaxCookMinutes_TextChanged(object sender, TextChangedEventArgs e)
		{
			updateFilterMaxCookTime();
		}

		void TextBoxMaxCookMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
		}

		void TextBoxMaxCookHours_TextChanged(object sender, TextChangedEventArgs e)
		{
			updateFilterMaxCookTime();
		}

		void TextBoxMaxCookHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
		}

		void updateFilterMaxCookTime()
		{
			string hours = textBoxMaxCookHours.Text;
			string minutes = textBoxMaxCookMinutes.Text;

			if (hours.Equals("") && minutes.Equals(""))
				SearchEngine.filter.maxTime = int.MaxValue;

			else if (hours.Equals(""))
				SearchEngine.filter.maxTime = Convert.ToInt32(minutes);

			else if (minutes.Equals(""))
				SearchEngine.filter.maxTime = 60 * Convert.ToInt32(hours);

			else
				SearchEngine.filter.maxTime = 60 * Convert.ToInt32(hours) + Convert.ToInt32(minutes);

			UpdateSearchResults();
		}

		void ButtonExcludeIngredient_Click(object sender, RoutedEventArgs e)
		{
			string ingredient = textBoxExcludeIngredient.Text;
			textBoxExcludeIngredient.Text = "";
			SearchEngine.filter.mustExcludeIngredients.Add(ingredient);

			UpdateSearchResults();
		}

		void ButtonIncludeIngredient_Click(object sender, RoutedEventArgs e)
		{
			string ingredient = textBoxIncludeIngredient.Text;
			textBoxIncludeIngredient.Text = "";
			SearchEngine.filter.mustIncludeIngredients.Add(ingredient);

			UpdateSearchResults();
		}

		void ComboBoxMinRating_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SearchEngine.filter.minRating = Convert.ToInt32(((ComboBoxItem)(comboBoxMinRating.SelectedItem)).Content);

			UpdateSearchResults();
		}

		void ButtonClearFilters_Click(object sender, RoutedEventArgs e)
		{
			comboBoxSortBy.SelectedIndex = 0;
			comboBoxMinRating.SelectedIndex = 0;
			textBoxMaxCookHours.Text = "";
			textBoxMaxCookMinutes.Text = "";

			SearchEngine.clearFilters();
			UpdateSearchResults();
		}

		void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//SearchEngine.SortBy sortBy;
			//if (comboBoxSortBy.SelectedValue.Equals("Rating"))
			//	sortBy = SearchEngine.SortBy.Rating;

			//else if (comboBoxSortBy.SelectedValue.Equals("Cook Time"))
			//	sortBy = SearchEngine.SortBy.CookTime;

			//else if (comboBoxSortBy.SelectedValue.Equals("Relevance"))
			//	sortBy = SearchEngine.SortBy.Relevance;

			//else
			//	sortBy = SearchEngine.SortBy.Relevance;

			//SearchEngine.filter.sortBy = sortBy;
			//UpdateSearchResults();
		}

		void LoadSearchResults(List<RecipeObject> searchResults)
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

		#endregion

	}
}
