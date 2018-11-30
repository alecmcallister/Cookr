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
    /// Interaction logic for RecipeStepButton.xaml
    /// </summary>
    public partial class RecipeStepButton : UserControl
    {
        public delegate void MyDelegate(object sender, RoutedEventArgs e);

        public MyDelegate Listener { get; set; }

        public RecipeStepButton()
        {
            InitializeComponent();
        }

        public RecipeStepButton(string title, bool isPhase)
        {
            InitializeComponent();
            SidebarRecipeStepButton.Content = title;
            if(isPhase)
            {
                // For the little buttons representing phases, make them smaller.
                SidebarRecipeStepButton.Height = 20;
                SidebarRecipeStepButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
        }

        private void SidebarRecipeStepButton_Click(object sender, RoutedEventArgs e)
        {
            Listener(this, e);
        }
    }
}
