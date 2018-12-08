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
		public static event Action<RecipeObject> RecipeSelectedEvent;

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
			RecipeSelectedEvent?.Invoke(recipe);
		}

		protected override async void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);

			await DoHover(true);
		}

		protected override async void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			await DoHover(false);
		}

		#region Animation

		SolidColorBrush bgBrush { get { return (SolidColorBrush)TryFindResource("BGNormal"); } }
		Color bgNormalColor { get { return (Color)TryFindResource("FG"); } }
		Color bgHoveredColor { get { return (Color)TryFindResource("FG-Hover"); } }

		TimeSpan enterTime { get { return TimeSpan.FromSeconds(0.3f); } }
		TimeSpan exitTime { get { return TimeSpan.FromSeconds(0.2f); } }

		IEasingFunction enterEaseFunction = new CubicEase();
		IEasingFunction exitEaseFunction = new CubicEase();

		async Task DoHover(bool entered)
		{
			TimeSpan duration = entered ? enterTime : exitTime;
			IEasingFunction ease = entered ? enterEaseFunction : exitEaseFunction;

			Color toBGColor = entered ? bgHoveredColor : bgNormalColor;
			double toScaleX = entered ? 1.1f : 1.05f;
			double toDepth = entered ? 4f : 1f;
			double toBlur = entered ? 8f : 3f;
			Thickness toMargin = entered ? new Thickness(10, 5, 10, 15) : new Thickness(10);

			bgBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(toBGColor, duration) { EasingFunction = ease });
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(toScaleX, duration) { EasingFunction = ease });
			RecipeCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(toScaleX, duration) { EasingFunction = ease });
			RecipeCardShadow.BeginAnimation(DropShadowEffect.ShadowDepthProperty, new DoubleAnimation(toDepth, duration) { EasingFunction = ease });
			RecipeCardShadow.BeginAnimation(DropShadowEffect.BlurRadiusProperty, new DoubleAnimation(toBlur, duration) { EasingFunction = ease });
			RecipeCardGrid.BeginAnimation(MarginProperty, new ThicknessAnimation(toMargin, duration) { EasingFunction = ease });

			await Task.Delay(duration);
		}

		#endregion

	}
}
