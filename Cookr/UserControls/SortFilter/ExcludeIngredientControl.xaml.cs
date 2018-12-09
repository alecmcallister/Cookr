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
	public partial class ExcludeIngredientControl : UserControl
	{
		public event Action<List<string>> MustExcludeUpdatedEvent = new Action<List<string>>(l => { });

		public bool IsFiltering { get { return ingredients.Count > 0; } }

		List<string> ingredients = new List<string>();

		public ExcludeIngredientControl()
		{
			InitializeComponent();
			IngredientField.Placeholder = "Can't contain...";

			IngredientField.SubmitEvent += i => { AddIngredient(i); };

			Reset();
		}

		public void Reset()
		{
			ingredients.Clear();
			FilteredIngredientStack.Children.Clear();

			MustExcludeUpdatedEvent(ingredients);
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

			MustExcludeUpdatedEvent(ingredients);
		}

		public void RemoveIngredient(FilteredIngredient ingredient)
		{
			ingredients.Remove(ingredient.IngredientName);

			FilteredIngredientStack.Children.Remove(ingredient);

			MustExcludeUpdatedEvent(ingredients);
		}

	}
}
