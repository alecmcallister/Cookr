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
	public partial class MinRatingControl : UserControl
	{
		public event Action<int> MinRatingSetEvent = new Action<int>(i => { });

		public List<TextBlock> Stars;

		public MinRatingControl()
		{
			InitializeComponent();

			Stars = new List<TextBlock>()
			{
				Star0,
				Star1,
				Star2,
				Star3,
				Star4
			};
		}

		void ClearButtonClick(object sender, RoutedEventArgs e)
		{
			SetMinRating(0);
			MinRatingSetEvent(0);
		}

		void StarClick(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;

			int star = int.Parse(button.Content.ToString());

			SetMinRating(star);
			MinRatingSetEvent(star);
		}

		public void SetMinRating(int rating)
		{
			for (int i = 0; i < Stars.Count; i++)
				SetStarActive(i, rating > i);
		}

		#region Animation

		float time = 0.2f;
		IEasingFunction ease = new CubicEase();

		//Color starActiveColor { get { return (Color)TryFindResource("Star"); } }
		Color starActiveColor { get { return (Color)TryFindResource("Text-Dark"); } }
		Color starInactiveColor { get { return (Color)TryFindResource("Inactive"); } }

		void SetStarActive(int star, bool active)
		{
			// Can animate fontsize (double)

			string icon = (string)TryFindResource(active ? "Star-Icon" : "Star-Empty-Icon");
			Stars[star].Text = icon;

			double toSize = active ? 38f : 36f;
			Stars[star].AnimateDoubleProperty(TextBlock.FontSizeProperty, toSize, time, ease);
			Stars[star].AnimateIconToColor(active ? starActiveColor : starInactiveColor, time, ease);
		}

		#endregion

	}
}
