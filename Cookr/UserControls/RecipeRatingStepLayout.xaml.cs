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

namespace Cookr
{
    /// <summary>
    /// Interaction logic for RecipeStepLayout.xaml
    /// </summary>
    public partial class RecipeRatingStepLayout : UserControl
    {

        //public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Rating", typeof(int), typeof(StarRating),
        //    new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(RatingChanged)));

        public delegate void MyDelegate(int rating);

        public MyDelegate listener { get; set; }

        public List<TextBlock> starsList = new List<TextBlock>();

        private int _max = 5;

        private int ValueProperty;

        public int Value
        {
            get
            {
                return (int)(ValueProperty);
            }
            set
            {
                if (value < 0)
                {
                    ValueProperty = 0;
                }
                else if (value > _max)
                {
                    ValueProperty = _max;
                }
                else
                {
                    ValueProperty = value;
                }
                RatingChanged(ValueProperty);
            }
        }

        public RecipeRatingStepLayout()
        {
            InitializeComponent();
            starsList = new List<TextBlock>() { Star1, Star2, Star3, Star4, Star5 }; ;
            listener = null;
        }

        private void RatingChanged(int valueToDisplay)
        {
            int newval = valueToDisplay;

            TextBlock textblock = null;
            
            for (int i = 0; i < newval; i++)
            {
                textblock = starsList[i] as TextBlock;
                if (textblock != null)
                {
                    textblock.Text = (string)FindResource("Star-Icon");
                    textblock.Foreground = (SolidColorBrush)FindResource("Star-Brush");
                }
            }
            
            for (int i = newval; i < starsList.Count; i++)
            {
                textblock = starsList[i] as TextBlock;
                if (textblock != null)
                {
                    textblock.Text = (string)FindResource("Star-Empty-Icon");
                    textblock.Foreground = (SolidColorBrush)FindResource("Inactive-Light-Brush");
                }
            }

        }

        private void MouseEnterStar(object sender, MouseEventArgs e)
        {
            // Change the displayed value to whatever star the user is hovering over.
            TextBlock textblock = sender as TextBlock;
            int tempValue = int.Parse(textblock.Tag.ToString());
            RatingChanged(tempValue);
        }

        private void MouseLeaveStar(object sender, MouseEventArgs e)
        {
            // Change the displayed value back to what the real value should be
            RatingChanged(ValueProperty);
        }

        private void MouseUpStar(object sender, MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;
            int newvalue = int.Parse(textblock.Tag.ToString());
            Value = newvalue;
            if (listener != null)
            {
                listener(Value);
            }
        }
    }
}
