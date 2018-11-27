using Cookr.Logic;
using Cookr.Pages;
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

namespace Cookr.UserControls
{
    /// <summary>
    /// Interaction logic for RecipeCard.xaml
    /// </summary>
    public partial class RecipeCard : UserControl
    {
        private RecipeObject recipe;

        public RecipeCard(RecipeObject _recipe)
        {
            InitializeComponent();
            this.Title.Content = _recipe.Title;
            recipe = _recipe;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationManager.NavigateToRecipe(recipe);
        }
    }
}
