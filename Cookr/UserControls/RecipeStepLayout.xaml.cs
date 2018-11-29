using Cookr.Logic.RecipeComponents;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for RecipeStepLayout.xaml
    /// </summary>
    public partial class RecipeStepLayout : UserControl
    {
        public RecipeStepLayout()
        {
            InitializeComponent();
        }
        public RecipeStepLayout(RecipeStep step)
        {
            InitializeComponent();
            StepTitle.Text = step.Number.ToString() + ". " + step.Title;
            if(step.Warning.Length > 0)
            {
                StepWarning.Visibility = Visibility.Visible;
                StepWarning.Text = step.Warning;
            }
            StepInstruction.Text = step.StepText;

            // Add the list of images for the step
            foreach(string i in step.Images)
            {
                if(File.Exists("Images/" + i))
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Images/" + i, UriKind.Relative));
                    ImageStackPanel.Children.Add(img);
                }
            }


        }
    }
}
