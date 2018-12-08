﻿using System;
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
    /// <summary>
    /// Interaction logic for CategoryTextButton.xaml
    /// </summary>
    public partial class CategoryTextButton : UserControl
    {
        string category;

        public CategoryTextButton()
        {
            InitializeComponent();
        }

        public CategoryTextButton(string _category)
        {
            InitializeComponent();
            category = _category;
            CategoryButtonText.Text = category;
        }
        

        void CategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            // Cause a search to occur with the category string..
        }

        protected async override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            await DoHover(true);
        }

        protected async override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            await DoHover(false);
        }

        SolidColorBrush bgBrush { get { return (SolidColorBrush)TryFindResource("BGNormal"); } }
        Color bgNormalColor { get { return (Color)TryFindResource("Primary"); } }
        Color bgHoveredColor { get { return (Color)TryFindResource("Primary-Dark"); } }

        float enterTime = 0.3f;
        float exitTime = 0.2f;

        IEasingFunction enterEaseFunction = new CubicEase();
        IEasingFunction exitEaseFunction = new CubicEase();

        async Task DoHover(bool entered)
        {
            float duration = entered ? enterTime : exitTime;
            IEasingFunction ease = entered ? enterEaseFunction : exitEaseFunction;

            Color toBGColor = entered ? bgHoveredColor : bgNormalColor;

            await bgBrush.AnimateToColor(toBGColor, duration, ease);
        }
    }
}
