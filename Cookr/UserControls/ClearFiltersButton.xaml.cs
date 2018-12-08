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
	public partial class ClearFiltersButton : UserControl
	{
		public event Action<bool> ToggleButtonEvent = new Action<bool>(b => { });

		bool toggled;

		public ClearFiltersButton()
		{
			InitializeComponent();
		}
	}
}
