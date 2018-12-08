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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cookr
{
	public partial class FilterControlPanel : UserControl
	{
		public event Action FilterUpdatedEvent = new Action(() => { });

		public event Action<SortBy> SortByEvent = new Action<SortBy>(s => { });
		public event Action<int> MinStarRatingEvent = new Action<int>(r => { });

		public FilterControlPanel()
		{
			InitializeComponent();

			Sort.SortByEvent += s => { SortByEvent(s); };
			MinRating.MinRatingSetEvent += r => { MinStarRatingEvent(r); };
		}
	}
}
