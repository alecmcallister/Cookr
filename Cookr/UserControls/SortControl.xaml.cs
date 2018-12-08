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
	public partial class SortControl : UserControl
	{
		public event Action<SortBy> SortByEvent = new Action<SortBy>(s => { });

		public Dictionary<int, Border> BGList;

		int currIndex = 0;
		int prevIndex = 0;

		public SortControl()
		{
			InitializeComponent();
			BGList = new Dictionary<int, Border>()
			{
				{0, RatingBG },
				{1, CookTimeBG },
				{2, RelevanceBG }
			};
		}

		private void SortClicked(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			int index = int.Parse(button.Content.ToString());
			HighlightListItem(index);
			SortBy sortBy = (SortBy)index;
			SortByEvent(sortBy);
		}


		#region Animation

		float time = 0.2f;
		IEasingFunction ease = new CubicEase();

		public void HighlightListItem(int item)
		{
			if (item == currIndex)
				return;

			currIndex = item;

			Color highlight = (Color)TryFindResource("Green-Normal");
			Color normal = (Color)TryFindResource("BG");

			BGList[item].AnimateBGToColor(highlight, time, ease);

			if (prevIndex != currIndex)
				BGList[prevIndex].AnimateBGToColor(normal, time, ease);

			prevIndex = currIndex;
		}

		#endregion
	}
}
