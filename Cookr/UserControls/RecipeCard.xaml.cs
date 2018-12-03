using Cookr.Logic;
using Cookr.Pages;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cookr
{
	public partial class RecipeCard : UserControl
	{
		RecipeObject recipe;

		public RecipeCard()
		{
			InitializeComponent();

		}

		public RecipeCard(RecipeObject _recipe)
		{
			InitializeComponent();
			RecipeCardTitle.Text = _recipe.Title;
			recipe = _recipe;
			TimeSpan total = TimeSpan.FromMinutes(_recipe.TotalTime);

			ImageBrush image = new ImageBrush(recipe.TitleBitmap) { Stretch = Stretch.UniformToFill };
			RecipeCardImage.Background = image;
			string duration = total.ToString("g");
			RecipeDurationText.Text = duration.Substring(0, duration.LastIndexOf(':'));

			ScaleTransform st = new ScaleTransform(1.05f, 1.05f, 0.5f, 0.5f);
			RecipeCardImage.Background.RelativeTransform = st;
		}

		void RecipeCardMouseDown(object sender, MouseButtonEventArgs e)
		{
			NavigationManager.NavigateToRecipe(recipe);
		}

		protected override async void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);

			await DoMouseEnter();
		}

		protected override async void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			await DoMouseExit();
		}


		// TODO: Actually finish the animations...

		#region Animation

		float upTime = 0.3f;
		float downTime = 0.2f;

		//IEasingFunction upEase = new BackEase();
		IEasingFunction upEase = new CubicEase();
		IEasingFunction downEase = new CubicEase();

		async Task DoMouseEnter()
		{
			DoubleAnimation scalex = new DoubleAnimation()
			{
				To = 1.1f,
				Duration = new Duration(TimeSpan.FromSeconds(upTime)),
				EasingFunction = upEase
			};
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scalex);
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scalex);

			DoubleAnimation depthAnimation = new DoubleAnimation()
			{
				To = 4,
				Duration = new Duration(TimeSpan.FromSeconds(upTime)),
				EasingFunction = upEase
			};

			DoubleAnimation blurAnimation = new DoubleAnimation()
			{
				To = 8,
				Duration = new Duration(TimeSpan.FromSeconds(upTime)),
				EasingFunction = upEase
			};

			ThicknessAnimation marginAnimation = new ThicknessAnimation()
			{
				To = new Thickness(10, 5, 10, 15),
				Duration = new Duration(TimeSpan.FromSeconds(upTime)),
				EasingFunction = upEase
			};

			RecipeCardShadow.BeginAnimation(DropShadowEffect.ShadowDepthProperty, depthAnimation);
			RecipeCardShadow.BeginAnimation(DropShadowEffect.BlurRadiusProperty, blurAnimation);

			RecipeCardGrid.BeginAnimation(MarginProperty, marginAnimation);

			await Task.Delay(TimeSpan.FromSeconds(upTime));
		}

		async Task DoMouseExit()
		{
			DoubleAnimation scalex = new DoubleAnimation()
			{
				To = 1.05f,
				Duration = new Duration(TimeSpan.FromSeconds(downTime)),
				EasingFunction = downEase
			};
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scalex);
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scalex);


			DoubleAnimation depthAnimation = new DoubleAnimation()
			{
				To = 1,
				Duration = new Duration(TimeSpan.FromSeconds(downTime)),
				EasingFunction = downEase
			};

			DoubleAnimation blurAnimation = new DoubleAnimation()
			{
				To = 3,
				Duration = new Duration(TimeSpan.FromSeconds(downTime)),
				EasingFunction = downEase
			};

			ThicknessAnimation marginAnimation = new ThicknessAnimation()
			{
				To = new Thickness(10),
				Duration = new Duration(TimeSpan.FromSeconds(downTime)),
				EasingFunction = downEase
			};

			RecipeCardShadow.BeginAnimation(DropShadowEffect.ShadowDepthProperty, depthAnimation);
			RecipeCardShadow.BeginAnimation(DropShadowEffect.BlurRadiusProperty, blurAnimation);

			RecipeCardGrid.BeginAnimation(MarginProperty, marginAnimation);

			await Task.Delay(TimeSpan.FromSeconds(downTime));
		}

		#endregion

	}
}
