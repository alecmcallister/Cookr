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

namespace Cookr.UserControls
{
    /// <summary>
    /// Interaction logic for RecipeCard.xaml
    /// </summary>
    public partial class RecipeCard : UserControl
    {
        public RecipeCard(RecipeObject recipe)
        {
            InitializeComponent();
            this.Title.Content = recipe.title;
        }
    }
}
