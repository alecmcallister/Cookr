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
		public Dictionary<int, TextBlock> FGList;

		public bool IsFiltering { get { return currIndex != 0; } }

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
			FGList = new Dictionary<int, TextBlock>()
			{
				{0, RatingFG },
				{1, CookTimeFG },
				{2, RelevanceFG }
			};
		}

		public void Reset()
		{
			HighlightListItem(0);
			SortByEvent(SortBy.Rating);
		}

		void SortClicked(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			int index = int.Parse(button.Content.ToString());
			HighlightListItem(index);
			SortByEvent((SortBy)index);
		}

		#region Animation

		float time = 0.2f;
		IEasingFunction ease = new CubicEase();

		public void HighlightListItem(int item)
		{
			if (item == currIndex)
				return;

			currIndex = item;

			Color highlight = (Color)TryFindResource("Primary");
			Color normal = (Color)TryFindResource("BG");
			Color highlightFG = (Color)TryFindResource("Text-Light");
			Color normalFG = (Color)TryFindResource("Text-Dark");


			BGList[item].AnimateBGToColor(highlight, time, ease);
			FGList[item].AnimateFGToColor(highlightFG, time, ease);

			if (prevIndex != currIndex)
			{
				BGList[prevIndex].AnimateBGToColor(normal, time, ease);
				FGList[prevIndex].AnimateFGToColor(normalFG, time, ease);
			}

			prevIndex = currIndex;
		}

		#endregion
	}
}
