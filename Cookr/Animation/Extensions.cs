using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace Cookr
{
	public static partial class Extensions
	{
		public static async Task TransitionToFrame(this Frame from, Frame to, PageAnimationType direction, float time, IEasingFunction ease)
		{
			Storyboard fromStoryboard = new Storyboard();
			Storyboard toStoryboard = new Storyboard();

			double width = from.ActualWidth;

			if (from.Parent.GetType().IsSubclassOf(typeof(FrameworkElement)))
				width = ((FrameworkElement)from.Parent).ActualWidth;

			fromStoryboard.AnimateFrameOut(width, direction, time, ease);
			toStoryboard.AnimateFrameIn(width, direction, time, ease);


			fromStoryboard.Begin(from);
			toStoryboard.Begin(to);

			to.Visibility = Visibility.Visible;

			await Task.Delay(TimeSpan.FromSeconds(time));

			from.Visibility = Visibility.Collapsed;
		}

		#region Animate frame in/ out

		// TODO: Cleanup duplicate code

		public static void AnimateFrameIn(this Storyboard storyboard, double width, PageAnimationType animType, float time, IEasingFunction ease)
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

			storyboard.Children.Add(animation);
			Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
		}

		public static void AnimateFrameOut(this Storyboard storyboard, double width, PageAnimationType animType, float time, IEasingFunction ease)
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

			storyboard.Children.Add(animation);
			Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
		}

		#endregion
	}
}
