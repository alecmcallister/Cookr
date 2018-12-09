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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cookr
{
    public partial class CircularIconButton : UserControl
    {
		public string IconText { get { return theButton.Content.ToString(); } set { theButton.Content = value; } }

        public CircularIconButton()
        {
            InitializeComponent();
        }

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			DoHover(true);
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			DoHover(false);
		}

		void DoHover(bool hovered)
		{
			Color to = (Color)(hovered ? TryFindResource("Primary-Dark") : TryFindResource("Primary"));
			((SolidColorBrush)FindResource("BGNormal")).AnimateToColor(to, 0.3f, new CubicEase());
		}
	}
}
