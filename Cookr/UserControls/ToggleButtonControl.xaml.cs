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
	/// <summary>
	/// Interaction logic for ToggleButton.xaml
	/// </summary>
	public partial class ToggleButtonControl : UserControl
	{
		public event Action<bool> ToggleButtonEvent = new Action<bool>(b => { });

		bool toggled;

		public ToggleButtonControl()
		{
			InitializeComponent();
		}

		void ToggleClicked(object sender, RoutedEventArgs e)
		{
			toggled = !toggled;

			ToggleButtonEvent(toggled);

			// TODO: Make async and wait until done animating before changing text
			ToggleButtonArrowIcon.Text = GetIconString();
			ToggleButtonText.Text = toggled ? "Less" : "Expand";
		}

		string GetIconString()
		{
			return toggled ? (string)TryFindResource("UpArrow-Icon") : (string)TryFindResource("DownArrow-Icon");
		}

		protected async override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);

			await DoHover(true);
		}

		protected async override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			await DoHover(false);
		}

		SolidColorBrush bgBrush { get { return (SolidColorBrush)TryFindResource("BGNormal"); } }
		Color bgNormalColor { get { return (Color)TryFindResource("Primary"); } }
		Color bgHoveredColor { get { return (Color)TryFindResource("Primary-Dark"); } }

		TimeSpan enterTime { get { return TimeSpan.FromSeconds(0.3f); } }
		TimeSpan exitTime { get { return TimeSpan.FromSeconds(0.2f); } }

		IEasingFunction enterEaseFunction = new CubicEase();
		IEasingFunction exitEaseFunction = new CubicEase();

		async Task DoHover(bool entered)
		{
			TimeSpan duration = entered ? enterTime : exitTime;
			IEasingFunction ease = entered ? enterEaseFunction : exitEaseFunction;

			Color toBGColor = entered ? bgHoveredColor : bgNormalColor;

			bgBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(toBGColor, duration) { EasingFunction = ease });

			await Task.Delay(duration);
		}

	}
}
