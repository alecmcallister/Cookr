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
	public partial class InputField : UserControl
	{
		public event Action<string> SubmitEvent = new Action<string>(s => { });
		public event Action<string> TextChangedEvent = new Action<string>(s => { });

		public bool HasText { get { return TextBox?.Text.Length > 0; } }

		public string Text { get { return TextBox?.Text; } set { TextBox.Text = value; } }

		string _placeholder = "-";
		public string Placeholder { get { return _placeholder; } set { _placeholder = value; UpdatePlaceholder(); } }

		public InputField()
		{
			InitializeComponent();
		}

		void TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdatePlaceholder();
			TextChangedEvent(Text);
		}

		void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Key == Key.Enter || e.Key == Key.Return) && HasText)
				SubmitEvent(Text);
		}

		void UpdatePlaceholder()
		{
			PlaceholderText.Text = HasText ? "" : _placeholder;
		}
	}
}
