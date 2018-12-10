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
	public partial class MainWindow : Window
	{
		float time = 0.75f;
		IEasingFunction ease = new CubicEase();

		Frame prevFrame;
		Frame currentFrame;

		Home theHome;
		Search theSearch;
		Recipe theRecipe;

		public MainWindow()
		{
			Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata() { DefaultValue = 120 });
			InitializeComponent();

			theHome = new Home();
			HomeFrame.Content = theHome;

			theSearch = new Search();
			SearchFrame.Content = theSearch;

			theRecipe = new Recipe();
			RecipeFrame.Content = theRecipe;

			currentFrame = HomeFrame;
			prevFrame = HomeFrame;

			Subscribe();
		}

		void Subscribe()
		{
			Searchbar.SearchEvent += async s => { theSearch.DoSearch(s); if (currentFrame != SearchFrame) await GoToSearch(); };
			HomeButton.HomeButtonClickedEvent += async () => { await GoToHome(); };
			BackButton.BackButtonClickedEvent += async () => { await BackPressed(); };

			RecipeButton.BackToRecipeClicked += async () => { await GoToRecipe(); };

			RecipeCard.RecipeSelectedEvent += async r => { theRecipe.SetRecipe(r); RecipeButton.SetCurrentRecipt(r.Title); await GoToRecipe(); };
            RecipeCategoryCard.CategorySelectedEvent += async c => { theSearch.DoSearch(c); if (currentFrame != SearchFrame) await GoToSearch(); };
			CategoryTextButton.CategorySelectedEvent += async c => { theSearch.DoSearch(c); if (currentFrame != SearchFrame) await GoToSearch(); };
		}

		#region Frame

		public void MinimizeWindow(object sender, EventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		public void MaximizeWindow(object sender, EventArgs e)
		{
			WindowState ^= WindowState.Maximized;
		}

		public void CloseWindow(object sender, EventArgs e)
		{
			Close();
		}

		#endregion


		#region Navigation

		public async Task BackPressed()
		{
			if (currentFrame == RecipeFrame)
			{
				if (prevFrame == HomeFrame)
				{
					await GoToHome();
				}
				else
				{
					await GoToSearch();
				}
			}
			else if (currentFrame == SearchFrame)
			{
				await GoToHome();
			}
		}

		public async Task GoToHome()
		{
			ShowFullHeader(true);
			SetRecipeButtonVisibility(haveRecipeInProgress);

			await currentFrame.TransitionToFrame(HomeFrame, PageAnimationType.Right, time, ease);

			prevFrame = currentFrame;
			currentFrame = HomeFrame;
		}

		public async Task GoToSearch()
		{
			ShowFullHeader(false);
			SetRecipeButtonVisibility(haveRecipeInProgress);

			if (currentFrame == RecipeFrame)
				await currentFrame.TransitionToFrame(SearchFrame, PageAnimationType.Right, time, ease);

			else if (currentFrame == HomeFrame)
				await currentFrame.TransitionToFrame(SearchFrame, PageAnimationType.Left, time, ease);

			prevFrame = currentFrame;
			currentFrame = SearchFrame;
		}

		public async Task GoToRecipe()
		{
			ShowFullHeader(false);
			SetRecipeButtonVisibility(false);

			await currentFrame.TransitionToFrame(RecipeFrame, PageAnimationType.Left, time, ease);

			prevFrame = currentFrame;
			currentFrame = RecipeFrame;
            theRecipe.RunButtonsListUpdate();
        }

		bool headerFullyVisible = true;
		bool haveRecipeInProgress { get { return theRecipe.recipe != null; } }
		double headerFullHeight = 250f;
		double headerSmallHeight = 100f;
		Thickness headerFullMargin = new Thickness(15);
		Thickness headerSmallMargin = new Thickness(15, 0, 15, 0);
		Thickness backButtonFullMargin = new Thickness(0, 0, -60f, 0);
		Thickness homeButtonFullMargin = new Thickness(-60f, 0, 0, 0);
		Thickness recipeButtonFullMargin = new Thickness(0, 0, -400f, 0);
		Thickness ButtonSmallMargin = new Thickness(0);

		#region Button Visibility

		public async Task SetHomeButtonVisibility(bool visible)
		{
			if (visible)
				HomeButton.Visibility = Visibility.Visible;

			await HomeButton.AnimateThicknessProperty(MarginProperty, visible ? ButtonSmallMargin : homeButtonFullMargin, time, ease);

			if (!visible)
				HomeButton.Visibility = Visibility.Hidden;

			HomeButton.IsHitTestVisible = visible;
		}

		public async Task SetRecipeButtonVisibility(bool visible)
		{
			if (visible)
				RecipeButton.Visibility = Visibility.Visible;

			await RecipeButton.AnimateThicknessProperty(MarginProperty, visible ? ButtonSmallMargin : recipeButtonFullMargin, time, ease);

			if (!visible)
				RecipeButton.Visibility = Visibility.Hidden;

			RecipeButton.IsHitTestVisible = visible;
		}

		public async Task SetBackButtonVisibility(bool visible)
		{
			if (visible)
				BackButton.Visibility = Visibility.Visible;

			await BackButton.AnimateThicknessProperty(MarginProperty, visible ? ButtonSmallMargin : backButtonFullMargin, time, ease);

			if (!visible)
				BackButton.Visibility = Visibility.Hidden;

			BackButton.IsHitTestVisible = visible;
		}

		#endregion

		public async Task ShowFullHeader(bool value)
		{
			if (headerFullyVisible == value)
				return;

			headerFullyVisible = value;

			Thickness hMargin = value ? headerFullMargin : headerSmallMargin;
			HeaderControlsGrid.AnimateThicknessProperty(MarginProperty, hMargin, time, ease);

			SetHomeButtonVisibility(!value);
			SetBackButtonVisibility(!value);

			double hHeight = value ? headerFullHeight : headerSmallHeight;
			await Header.AnimateDoubleProperty(HeightProperty, hHeight, time, ease);
		}

		#endregion
	}
}
