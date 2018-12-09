using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Globalization;

namespace Cookr
{
	public partial class Search : Page
	{
		TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

		string _currentSearch;
		public string CurrentSearch { get { return _currentSearch; } set { _currentSearch = value; ResultText = value; } }

		public string ResultText { get { return SearchResultText.Text; } set { SearchResultText.Text = '"' + " " + textInfo.ToTitleCase(value.ToLower()) + " " + '"'; } }

		public Search()
		{
			InitializeComponent();
			CurrentSearch = "";

			Subscribe();
		}

		void Subscribe()
		{
			ExpandButton.ToggleButtonEvent += async (b) => { await ToggleFilterPanelVisibility(b); };

			FilterControl.SortByEvent += s => { SearchEngine.filter.sortBy = s; UpdateSearchResults(); };
			FilterControl.MinStarRatingEvent += r => { SearchEngine.filter.minRating = r; UpdateSearchResults(); };
			FilterControl.MaxCookTimeUpdatedEvent += t => { SearchEngine.filter.maxTime = t; UpdateSearchResults(); };
			FilterControl.MustIncludeListUpdatedEvent += l => { SearchEngine.filter.mustIncludeIngredients = l; UpdateSearchResults(); };
			FilterControl.MustExcludeListUpdatedEvent += l => { SearchEngine.filter.mustExcludeIngredients = l; UpdateSearchResults(); };

			ClearButton.ClearFiltersEvent += ClearFilters;
		}

		void ClearFilters()
		{
			FilterControl.ClearFilters();
		}

		public void DoSearch(string value)
		{
			CurrentSearch = value.ToLower();

			string[] delimiters = { " ", "," };
			string[] tagStrings = CurrentSearch.Split(delimiters, StringSplitOptions.None);
			List<string> tags = new List<string>(tagStrings);

			LoadSearchResults(SearchEngine.TagSearch(RecipeManager.GetRecipes(), tags));
		}

		public void UpdateSearchResults()
		{
			DoSearch(CurrentSearch);

			ClearButton.SetVisibility(FilterControl.IsFiltering);
		}

		#region Animation

		double filterPanelExpandedHeight = 220f;
		float animationTime = 0.3f;
		IEasingFunction ease = new CubicEase();

		async Task ToggleFilterPanelVisibility(bool visible)
		{
			await ExpandableGrid.AnimateDoubleProperty(HeightProperty, visible ? filterPanelExpandedHeight : 0f, animationTime, ease);
		}

		#endregion

		void LoadSearchResults(List<RecipeObject> searchResults)
		{
			SearchResultsStack.Children.Clear();
			if (searchResults.Count == 0)
			{
				TextBlock noResults = new TextBlock();
				noResults.Style = (Style)FindResource("DropShadowText");
				noResults.FontSize = (double)FindResource("Text-Large");
				noResults.VerticalAlignment = VerticalAlignment.Center;
				noResults.HorizontalAlignment = HorizontalAlignment.Center;
				noResults.Text = "No recipes found :(";
				SearchResultsStack.Children.Add(noResults);
				return;
			}
			searchResults.ForEach(recipe => SearchResultsStack.Children.Add(new RecipeCard(recipe)));
		}

	}
}
