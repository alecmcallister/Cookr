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
	public partial class FilteredIngredient : UserControl
	{
		public event Action<FilteredIngredient> StopFilteringIngredient = new Action<FilteredIngredient>(i => { }); 
		public string IngredientName { get { return IngredientNameText.Text; } set { IngredientNameText.Text = value; } }

		public FilteredIngredient()
		{
			InitializeComponent();
		}

		void StopFilteringClick(object sender, RoutedEventArgs e)
		{
			StopFilteringIngredient(this);
		}
	}
}
