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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cookr.Categories;

namespace Cookr
{
    /// <summary>
    /// Interaction logic for RecipeCategoryCard.xaml
    /// </summary>
    public partial class RecipeCategoryCard : UserControl
    {
        public static event Action<string> CategorySelectedEvent;

        Category category;

        public RecipeCategoryCard()
        {
            InitializeComponent();
        }

        public RecipeCategoryCard(Category _category)
        {
            InitializeComponent();
            category = _category;
            RecipeCategoryCardTitle.Text = category.name;

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Images/Categories/" + category.image, UriKind.Relative));
            ImageBrush image = new ImageBrush(img.Source) { Stretch = Stretch.UniformToFill, };
            RecipeCategoryCardImage.Background = image;

            ScaleTransform st = new ScaleTransform(1.05f, 1.05f, 0.5f, 0.5f);
            RecipeCategoryCardImage.Background.RelativeTransform = st;
        }

        void RecipeCategoryCardMouseDown(object sender, MouseButtonEventArgs e)
        {
            CategorySelectedEvent?.Invoke(category.name);
        }

        protected override async void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            await DoHover(true);
        }

        protected override async void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            await DoHover(false);
        }

        #region Animation

        SolidColorBrush bgBrush { get { return (SolidColorBrush)TryFindResource("BGNormal"); } }
        Color bgNormalColor { get { return (Color)TryFindResource("FG"); } }
        //Color bgHoveredColor { get { return (Color)TryFindResource("Primary-Light"); } }
        Color bgHoveredColor { get { return (Color)TryFindResource("FG-Hover"); } }

        TimeSpan enterTime { get { return TimeSpan.FromSeconds(0.3f); } }
        TimeSpan exitTime { get { return TimeSpan.FromSeconds(0.2f); } }

        IEasingFunction enterEaseFunction = new CubicEase();
        IEasingFunction exitEaseFunction = new CubicEase();

        async Task DoHover(bool entered)
        {
            TimeSpan duration = entered ? enterTime : exitTime;
            IEasingFunction ease = entered ? enterEaseFunction : exitEaseFunction;

            Color toBGColor = entered ? bgHoveredColor : bgNormalColor;
            double toScaleX = entered ? 1.1f : 1.05f;
            double toDepth = entered ? 4f : 1f;
            double toBlur = entered ? 8f : 3f;
            Thickness toMargin = entered ? new Thickness(5, 5, 5, 15) : new Thickness(5);

            bgBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(toBGColor, duration) { EasingFunction = ease });
            RecipeCategoryCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(toScaleX, duration) { EasingFunction = ease });
            RecipeCategoryCardImage.Background.RelativeTransform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(toScaleX, duration) { EasingFunction = ease });
            RecipeCategoryCardShadow.BeginAnimation(DropShadowEffect.ShadowDepthProperty, new DoubleAnimation(toDepth, duration) { EasingFunction = ease });
            RecipeCategoryCardShadow.BeginAnimation(DropShadowEffect.BlurRadiusProperty, new DoubleAnimation(toBlur, duration) { EasingFunction = ease });
            RecipeCategoryCardGrid.BeginAnimation(MarginProperty, new ThicknessAnimation(toMargin, duration) { EasingFunction = ease });

            await Task.Delay(duration);
        }

        #endregion

    }
}
