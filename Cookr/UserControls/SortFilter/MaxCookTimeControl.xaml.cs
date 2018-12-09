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
	public partial class MaxCookTimeControl : UserControl
	{
		public event Action<int> MaxCookTimeUpdatedEvent = new Action<int>(t => { });

		public bool IsFiltering { get { return maxHours > 0 || maxMinutes > 0; } }

		int maxHours;
		int maxMinutes;

		int maxTime { get { return maxHours == 0 && maxMinutes == 0 ? int.MaxValue : (maxHours * 60) + maxMinutes; } }

		public MaxCookTimeControl()
		{
			InitializeComponent();
			Subscribe();
		}

		void Subscribe()
		{
			HoursInput.TextChangedEvent += s => { if (!int.TryParse(s, out maxHours)) { HoursInput.Text = ""; maxHours = 0; } UpdateMaxCookTime(); };
			MinutesInput.TextChangedEvent += s => { if (!int.TryParse(s, out maxMinutes)) { MinutesInput.Text = ""; maxMinutes = 0; } UpdateMaxCookTime(); };
		}

		public void Reset()
		{
			HoursInput.Text = "";
			MinutesInput.Text = "";
		}

		void UpdateMaxCookTime()
		{
			MaxCookTimeUpdatedEvent(maxTime);
		}
	}
}
