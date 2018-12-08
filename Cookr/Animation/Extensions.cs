using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cookr
{
	public static partial class Extensions
	{

		#region Frame transitioning

		public static async Task TransitionToFrame(this Frame from, Frame to, PageAnimationType direction, float time, IEasingFunction ease)
		{
			double width = from.ActualWidth;

			if (from.Parent.GetType().IsSubclassOf(typeof(FrameworkElement)))
				width = ((FrameworkElement)from.Parent).ActualWidth;

			from.AnimateFrameOut(width, direction, time, ease);
			to.AnimateFrameIn(width, direction, time, ease);

			to.Visibility = Visibility.Visible;

			await Task.Delay(TimeSpan.FromSeconds(time));

			from.Visibility = Visibility.Collapsed;
		}

		#endregion

		#region Animate frame in/ out

		public static void AnimateFrameIn(this FrameworkElement element, double width, PageAnimationType animType, float time, IEasingFunction ease)
		{
			float x = animType == PageAnimationType.Right ? -1f : 1f;

			Thickness from = new Thickness(x * width, 0, -x * width, 0);
			Thickness to = new Thickness(0);

			ThicknessAnimation animation = new ThicknessAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				From = from,
				To = to,
				EasingFunction = ease
			};
			element.BeginAnimation(FrameworkElement.MarginProperty, animation);
		}

		public static void AnimateFrameOut(this FrameworkElement element, double width, PageAnimationType animType, float time, IEasingFunction ease)
		{
			float x = animType == PageAnimationType.Left ? -1f : 1f;

			Thickness from = new Thickness(0);
			Thickness to = new Thickness(x * width, 0, -x * width, 0);

			ThicknessAnimation animation = new ThicknessAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				From = from,
				To = to,
				EasingFunction = ease
			};
			element.BeginAnimation(FrameworkElement.MarginProperty, animation);
		}

		#endregion

		#region Generic Animation

		public static async Task AnimateDoubleProperty<T>(this T element, DependencyProperty property, double to, float time, IEasingFunction easeFunction) where T : IAnimatable
		{
			DoubleAnimation animation = new DoubleAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				To = to,
				EasingFunction = easeFunction
			};

			element.BeginAnimation(property, animation);

			await Task.Delay(TimeSpan.FromSeconds(time));
		}

		public static async Task AnimateThicknessProperty(this FrameworkElement element, DependencyProperty property, Thickness to, float time, IEasingFunction easeFunction)
		{
			ThicknessAnimation animation = new ThicknessAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				To = to,
				EasingFunction = easeFunction
			};

			element.BeginAnimation(property, animation);

			await Task.Delay(TimeSpan.FromSeconds(time));
		}

		#endregion

		#region Animate color

		public static async Task AnimateToColor(this SolidColorBrush element, Color to, float time, IEasingFunction easeFunction)
		{
			ColorAnimation animation = new ColorAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				To = to,
				EasingFunction = easeFunction
			};

			element.BeginAnimation(SolidColorBrush.ColorProperty, animation);

			await Task.Delay(TimeSpan.FromSeconds(time));
		}

		public static async Task AnimateBGToColor(this Border element, Color to, float time, IEasingFunction easeFunction)
		{
			element.Background = new SolidColorBrush(((SolidColorBrush)element.Background).Color);
			ColorAnimation animation = new ColorAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				To = to,
				EasingFunction = easeFunction
			};

			element.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);

			await Task.Delay(TimeSpan.FromSeconds(time));
		}

		public static async Task AnimateIconToColor(this TextBlock element, Color to, float time, IEasingFunction easeFunction)
		{
			element.Foreground = new SolidColorBrush(((SolidColorBrush)element.Foreground).Color);
			ColorAnimation animation = new ColorAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(time)),
				To = to,
				EasingFunction = easeFunction
			};

			element.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);

			await Task.Delay(TimeSpan.FromSeconds(time));
		}

		#endregion
	}
}
