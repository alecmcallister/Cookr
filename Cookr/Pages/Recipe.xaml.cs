using Cookr.Logic;
using Cookr.Logic.RecipeComponents;
using Cookr.UserControls;
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

    // ANDY - All the changes you need to make would be on this page.
    public partial class Recipe : Page, CookrPage
    {
        public RecipeObject recipe;

        public Recipe(RecipeObject _recipe)
        {
            // This incoming RecipeObject contains all the information from a single XML files including steps, tools, igredients, filepaths etc..
            InitializeComponent();
            recipe = _recipe;

            // Populate Title, time, star rating, TitleImage.
            RecipeTitle.Content = recipe.Title;
            RecipeTime.Content = recipe.TotalTime.ToString() + " m";
            RecipeStarRating.Value = (int)(recipe.Rating);
            RecipeTitleImage.ImageSource = new BitmapImage(
                                           new Uri("Images/" + recipe.TitleImage, UriKind.Relative));

            // Populate recipe introduction, ingredients, and tools.
            DescriptionTextBlock.Text = recipe.RecipeIntroduction;
            IngredientsTextBlock.Text = GenerateIngredientsString();
            ToolsTextBlock.Text = GenerateToolsString();

            LoadRecipeSteps();
        }

        private void LoadRecipeSteps()
        {
            // Use the list of RecipeSteps to populate the list of buttons and recipe steps

            // Keep track of the current step type so we know when to add in the little extra bars
            // That say Cook or Prep or whatever.
            string currentType = "";
            foreach(RecipeStep rs in recipe.RecipeSteps)
            {
                if(!rs.Type.Equals(currentType))
                {
                    // We changed to a different phase/type of step, so add in some little bar guys.
                    currentType = rs.Type;
                    RecipeButtonStack.Children.Add(new RecipeStepButton(currentType, true));
                    RecipeStepStack.Children.Add(new RecipePhase(currentType));
                }

                // Add the for realsies button for this step
                RecipeButtonStack.Children.Add(new RecipeStepButton(rs.Number.ToString() + ". " + rs.Title, false));

                // Add the for realsies step... for this step.
                RecipeStepStack.Children.Add(new RecipeStepLayout(rs));
            }

            // Finally, add little "All done!" strip and recipe rating section
            RecipeStepStack.Children.Add(new RecipePhase("All done!"));

            RecipeRatingStepLayout rateStep = new RecipeRatingStepLayout();
            rateStep.RecipeRating.listener = RecipeRatingUpdated;
            
            rateStep.RecipeRating.Value = recipe.UserRating;

            RecipeStepStack.Children.Add(rateStep);

        }

        public void RecipeRatingUpdated(int rating)
        {
            recipe.UserRating = rating;
        }

        // Uses the recipe object to parse the ingredients list into something that's nice to display
        private string GenerateIngredientsString()
        {
            string ingredients = "Ingredients:\r\n\r\n";
            foreach(Ingredient i in recipe.Ingredients)
            {
                ingredients += "- " + i.Quanitity + " " + i.Name;
                if(i.Optional)
                {
                    ingredients += " (Optional)";
                }
                ingredients += "\r\n";
            }

            return ingredients;
        }

        // Uses the recipe object to parse the tools list into something that's nice to display
        private string GenerateToolsString()
        {
            string tools = "Tools:\r\n\r\n";
            foreach (Tool t in recipe.Tools)
            {
                tools += "- " + t.Name + "\r\n";
            }

            return tools;
        }

        private void DescriptionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToolsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IngredientsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecipeBtnSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToSearch(RecipeSearchField.Text);
        }


        private void RecipeSearchField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RecipeSearchField.Text == "Search")
            {
                RecipeSearchField.Text = "";
                RecipeSearchField.Foreground = Brushes.Black;
            }
        }

        private void RecipeSearchField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RecipeSearchField.Text == "")
            {
                RecipeSearchField.Text = "Search";
                RecipeSearchField.Foreground = Brushes.LightGray;
            }
        }

        private void RecipeSearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigationManager.NavigateToSearch(RecipeSearchField.Text);
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToPrev();
        }

        public void SetBackButton()
        {
            if (!NavigationManager.allowPrev())
            {
                BackBtn.Visibility = Visibility.Collapsed;
            }
            else
                BackBtn.Visibility = Visibility.Visible;
        }
    }
}
