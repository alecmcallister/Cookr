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

    public enum VisibilityStyleState { invisible, partial, full, };

    /// <summary>
    /// Interaction logic for RecipeStepButton.xaml
    /// </summary>
    public partial class RecipeStepButtonCustom : UserControl
    {
        public delegate void MyDelegate(object sender, RoutedEventArgs e);

        public MyDelegate Listener { get; set; }

        public VisibilityStyleState _styleState;
        public VisibilityStyleState StyleState
        {
            get { return _styleState; }
            set
            {
                _styleState = value;
                switch (_styleState)
                {
                    case VisibilityStyleState.invisible:
                        HoverCoverPartial.Visibility = Visibility.Hidden;
                        HoverCoverFull.Visibility = Visibility.Hidden;
                        HoverCover.Visibility = Visibility.Visible;
                        break;
                    case VisibilityStyleState.partial:
                        HoverCoverPartial.Visibility = Visibility.Visible;
                        HoverCoverFull.Visibility = Visibility.Hidden;
                        HoverCover.Visibility = Visibility.Hidden;
                        break;
                    case VisibilityStyleState.full:
                        HoverCoverPartial.Visibility = Visibility.Hidden;
                        HoverCoverFull.Visibility = Visibility.Visible;
                        HoverCover.Visibility = Visibility.Hidden;
                        break;
                }
            }
        }
        public RecipeStepButtonCustom()
        {
            InitializeComponent();
            StyleState = VisibilityStyleState.invisible;
        }

        public RecipeStepButtonCustom(string title, bool isPhase)
        {
            InitializeComponent();
            StyleState = VisibilityStyleState.invisible;
            Content.Text = title;
            if(isPhase)
            {
                // For the little buttons representing phases, make them smaller.
                SidebarRecipeStepButtonCustom.Height = 20;
                Content.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        private void SidebarRecipeStepButton_Click(object sender, RoutedEventArgs e)
        {
            Listener(this, e);
        }
    }
}
