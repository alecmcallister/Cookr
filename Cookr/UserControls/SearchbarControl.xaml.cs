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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cookr
{
	public partial class SearchbarControl : UserControl
	{
		public event Action<string> SearchEvent = new Action<string>(s => { });

		public bool HasText { get { return InputField?.Text.Length > 0; } }

		public string Text { get { return InputField?.Text; } set { InputField.Text = value; } }

		public SearchbarControl()
		{
			InitializeComponent();
			InputField.Placeholder = "Search...";
			InputField.SubmitEvent += s => { SearchbarButton_Click(null, null); };
		}

		void SearchbarTextChanged(object sender, TextChangedEventArgs e)
		{
			UpdatePlaceholder();
		}

		void SearchbarButton_Click(object sender, RoutedEventArgs e)
		{
			if (!HasText)
				return;

			SearchEvent(InputField.Text);
			Text = "";
		}

		void SearchbarTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.Return)
				SearchbarButton_Click(sender, e);
		}

		void UpdatePlaceholder()
		{
			SearchbarButton.Foreground = (Brush)(HasText ? TryFindResource("Text-Dark-Brush") : TryFindResource("Inactive-Brush"));
		}
	}
}
