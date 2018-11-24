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
    public partial class Search : Page
    {
        String SearchString;
        public Search()
        {
            InitializeComponent();
        }
        public Search(String search)
        {
            SearchString = search;
            InitializeComponent();
            SearchedLabel.Content = "\"" + SearchString + "\"";
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Search(SearchField.Text));
        }
    }
}
