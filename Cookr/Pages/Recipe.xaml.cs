using Cookr.Logic;
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

namespace Cookr.Pages
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class Recipe : Page, CookrPage
    {
        public Recipe(RecipeObject _recipe)
        {
            InitializeComponent();
            RecipeTitle.Content = _recipe.title;
            Tags.Content = String.Join(", ", _recipe.tags.ToArray());
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToPrev();
        }

        public void SetBackButton()
        {
            if (!NavigationManager.allowPrev())
            {
                BackBtn.Visibility = Visibility.Collapsed;
            }
            else
                BackBtn.Visibility = Visibility.Visible;
        }

    }
}
