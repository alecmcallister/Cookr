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
	public partial class MustIncludeIngredientControl : UserControl
	{
		public event Action<List<string>> MustInludeUpdatedEvent = new Action<List<string>>(l => { });

		public bool IsFiltering { get { return ingredients.Count > 0; } }

		List<string> ingredients = new List<string>();

		public MustIncludeIngredientControl()
		{
			InitializeComponent();
			IngredientField.Placeholder = "Must include...";

			IngredientField.SubmitEvent += i => { AddIngredient(i); };

			Reset();
		}

		public void Reset()
		{
			ingredients.Clear();
			FilteredIngredientStack.Children.Clear();

			MustInludeUpdatedEvent(ingredients);
		}

		void AddItemButton_Click(object sender, RoutedEventArgs e)
		{
			if (IngredientField.Text.Length == 0)
				return;

			AddIngredient(IngredientField.Text);
		}

		public void AddIngredient(string ingredient)
		{
			string formatted = ingredient.ToLower();

			if (ingredients.Contains(formatted))
				return;

			ingredients.Add(formatted);

			FilteredIngredient filtered = new FilteredIngredient() { IngredientName = formatted };
			filtered.StopFilteringIngredient += RemoveIngredient;
			FilteredIngredientStack.Children.Add(filtered);
			IngredientField.Text = "";

			MustInludeUpdatedEvent(ingredients);
		}

		public void RemoveIngredient(FilteredIngredient ingredient)
		{
			ingredients.Remove(ingredient.IngredientName);

			FilteredIngredientStack.Children.Remove(ingredient);

			MustInludeUpdatedEvent(ingredients);
		}

	}
}
