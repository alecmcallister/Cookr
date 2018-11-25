using Cookr.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Cookr.Logic
{
    public static class NavigationManager
    {
        public static Frame navService;
        public static Home homePage = new Home();
        //public static List<Page> prevPage = new List<Page>();
        static List<CookrPage> prevPage = new List<CookrPage>();

        public static void SetNavigationFrame(Frame _nav)
        {
            navService = _nav;
            prevPage.Add(homePage);
        }

        public static void NavigateToRecipe (RecipeObject _recipe)
        {
            prevPage.Add(new Recipe(_recipe));
            navService.Navigate(prevPage[prevPage.Count-1]);
        }

        public static void NavigateToSearch(string searchString)
        {
            prevPage.Add(new Search(searchString));
            navService.Navigate(prevPage[prevPage.Count-1]);
            prevPage[prevPage.Count - 1].SetBackButton();
        }

        public static void NavigateToHome()
        {
            prevPage.Add(homePage);
            navService.Navigate(homePage);
            homePage.SetBackButton();
        }

        internal static void NavigateToPrev()
        {
            if (prevPage.Count < 2)
                return;
           prevPage.RemoveAt(prevPage.Count - 1);
           prevPage[prevPage.Count - 1].SetBackButton();
           navService.Navigate(prevPage[prevPage.Count-1]);
        }

        public static bool allowPrev()
        {
            if (prevPage.Count < 2)
                return false;
            return true;
        }
    }
}
