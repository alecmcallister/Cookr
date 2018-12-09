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
	public partial class ClearFiltersButton : UserControl
	{
		public event Action ClearFiltersEvent = new Action(() => { });

		public ClearFiltersButton()
		{
			InitializeComponent();
			IsHitTestVisible = false;
		}

		void ClearFiltersClick(object sender, RoutedEventArgs e)
		{
			ClearFiltersEvent();
			SetVisibility(false);
		}

		#region Animation

		public void SetVisibility(bool visible)
		{
			IsHitTestVisible = visible;
			this.AnimateDoubleProperty(OpacityProperty, visible ? 1f : 0f, 0.3f, new CubicEase());
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

		Color bgNormalColor { get { return (Color)TryFindResource("Text-Dark"); } }
		Color bgHoveredColor { get { return (Color)TryFindResource("Primary-Dark"); } }

		void DoHover(bool hovered)
		{
			((SolidColorBrush)TryFindResource("BGNormal")).AnimateToColor(hovered ? bgHoveredColor : bgNormalColor, 0.3f, new CubicEase());
		}

		#endregion
	}
}
