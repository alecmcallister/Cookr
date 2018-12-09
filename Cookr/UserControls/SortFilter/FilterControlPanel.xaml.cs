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
		public event Action<int> MaxCookTimeUpdatedEvent = new Action<int>(t => { });
		public event Action<List<string>> MustIncludeListUpdatedEvent = new Action<List<string>>(l => { }); 
		public event Action<List<string>> MustExcludeListUpdatedEvent = new Action<List<string>>(l => { });

		public bool IsFiltering { get { return Sort.IsFiltering || MinRating.IsFiltering || MaxCookTime.IsFiltering || MustInclude.IsFiltering || MustExclude.IsFiltering; } }

		public FilterControlPanel()
		{
			InitializeComponent();
			Subscribe();
		}

		void Subscribe()
		{
			Sort.SortByEvent += s => { SortByEvent(s); };
			MinRating.MinRatingSetEvent += r => { MinStarRatingEvent(r); };
			MaxCookTime.MaxCookTimeUpdatedEvent += t => { MaxCookTimeUpdatedEvent(t); };
			MustInclude.MustInludeUpdatedEvent += l => { MustIncludeListUpdatedEvent(l); };
			MustExclude.MustExcludeUpdatedEvent += l => { MustExcludeListUpdatedEvent(l); };
		}

		public void ClearFilters()
		{
			Sort.Reset();
			MinRating.Reset();
			MaxCookTime.Reset();
			MustInclude.Reset();
			MustExclude.Reset();
		}

	}
}
