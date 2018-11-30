using Cookr.Logic;
using Cookr.Logic.RecipeComponents;
using Cookr.UserControls;
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

namespace Cookr.Pages
{
    enum RecipeUserVisible { invisible, partial, full, };

    // ANDY - All the changes you need to make would be on this page.
    public partial class Recipe : Page, CookrPage
    {
        public RecipeObject recipe;

        // List of the buttons making up that list on the left
        public List<RecipeStepButton> stepButtons;

        // List of the elements in the recipe's RecipeStepStack that can be used for navigation.
        public List<UIElement> recipeScrollableNavigationThingys;

        public Recipe(RecipeObject _recipe)
        {
            // This incoming RecipeObject contains all the information from a single XML files including steps, tools, igredients, filepaths etc..
            InitializeComponent();
            recipe = _recipe;

            // Populate Title, time, star rating, TitleImage.
            RecipeTitle.Content = recipe.Title;
            RecipeTime.Content = recipe.TotalTime.ToString() + " minutes";
            RecipeStarRating.Value = (int)(recipe.Rating);
            RecipeTitleImage.ImageSource = new BitmapImage(
                                           new Uri("Images/" + recipe.TitleImage, UriKind.Relative));

            // Populate recipe introduction, ingredients, and tools.
            DescriptionTextBlock.Text = recipe.RecipeIntroduction;
            GenerateIngredientsList(IngredientsTextBlock);
            GenerateToolsList(ToolsTextBlock);

            LoadRecipeSteps();

            fillStepButtonsList();

            SetUpButtonDefaults();
            fillRecipeScrollableNavigationThingyList();
        }

        /// <summary>
        /// Set up the first 3 buttons for the recipe more directly.
        /// Also set up a callback function for when the buttons are clicked.
        /// </summary>
        private void SetUpButtonDefaults()
        {
            stepButtons[0].SidebarRecipeStepButton.Content = recipe.Title;
            stepButtons[1].SidebarRecipeStepButton.Content = "Ingredients";
            stepButtons[2].SidebarRecipeStepButton.Content = "Tools";
            stepButtons[0].SidebarRecipeStepButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            stepButtons[1].SidebarRecipeStepButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            stepButtons[2].SidebarRecipeStepButton.HorizontalContentAlignment = HorizontalAlignment.Center;

            foreach(RecipeStepButton b in stepButtons)
            {
                b.Listener = NavigationButton_Click;
            }
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < stepButtons.Count; i++)
            {
                if((RecipeStepButton)sender == stepButtons[i])
                {
                    //Navigate to the corresponding UI element In the recipe stackpanel
                    UIElement item = recipeScrollableNavigationThingys[i];
                    Rect bounds = item.TransformToAncestor(RecipeStepScrollViewer).TransformBounds(new Rect(0.0, 0.0, item.RenderSize.Width, item.RenderSize.Height));
                    RecipeStepScrollViewer.ScrollToVerticalOffset(bounds.Y + RecipeStepScrollViewer.ContentVerticalOffset);
                }
            }
        }

        /// <summary>
        /// An absolute hack way to get all the navigation buttons in a list, where we can do whatever
        /// We want to em. This might not be the best way to get around the fact that 2 of those boys
        /// are mushed together in a grid but this works.
        /// </summary>
        private void fillRecipeScrollableNavigationThingyList()
        {
            recipeScrollableNavigationThingys = new List<UIElement>();
            // Add the pieces we already know are going to be there directly
            recipeScrollableNavigationThingys.Add(RecipeTitleImageGrid);
            recipeScrollableNavigationThingys.Add(IngredientsTextBlock);
            recipeScrollableNavigationThingys.Add(ToolsTextBlock);

            // And then make sure we don't add them again...
            // We're skipping the title image, description, ingredients, tools.
            int elementsToSkip = 4;
            foreach (UIElement e in RecipeStepStack.Children)
            {
                if(elementsToSkip > 0)
                {
                    elementsToSkip--;
                    continue;
                }
                recipeScrollableNavigationThingys.Add(e);
            }

            // Remove the last two elements we added since they're the little bar that says done and the rating section...
            recipeScrollableNavigationThingys.RemoveRange(recipeScrollableNavigationThingys.Count - 2, 2);

        }

        private void fillStepButtonsList()
        {
            stepButtons = new List<RecipeStepButton>();
            foreach (UIElement e in RecipeButtonStack.Children)
            {
                if (e.GetType() == typeof(Grid))
                {
                    Grid grid = (Grid)e;
                    foreach (UIElement ge in grid.Children)
                    {
                        stepButtons.Add((RecipeStepButton)ge);
                    }
                }
                else
                {
                    stepButtons.Add((RecipeStepButton)e);
                }
            }
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
                RecipeStepStack.Children.Add(new RecipeStepLayout(rs, this));
            }

            // Finally, add little "All done!" strip and recipe rating section
            RecipeStepStack.Children.Add(new RecipePhase("All done!"));

            // Add the rating bar at the bottom. populate the already voted value.
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
        private void GenerateIngredientsList(TextBlock textblock)
        {
            textblock.Inlines.Clear();
            textblock.Inlines.Add("Ingredients:\r\n\r\n");
            foreach(Ingredient i in recipe.Ingredients)
            {
                textblock.Inlines.Add("- " + i.Quanitity + " ");
                if (i.ToolTipID != 0)
                {
                    textblock.Inlines.Add(CreateInTextToolTip(i.Name, i.ToolTipID));
                }
                else
                {
                    textblock.Inlines.Add(i.Name);
                }
                if(i.Optional)
                {
                    textblock.Inlines.Add(" (Optional)");
                }
                textblock.Inlines.Add("\r\n");
            }

        }

        // Uses the recipe object to parse the tools list into something that's nice to display
        private void GenerateToolsList(TextBlock textblock)
        {
            textblock.Inlines.Clear();
            textblock.Inlines.Add("Tools:\r\n\r\n");
            foreach (Tool t in recipe.Tools)
            {
                if (t.ToolTipID != 0)
                {
                    textblock.Inlines.Add("- ");
                    textblock.Inlines.Add(CreateInTextToolTip(t.Name, t.ToolTipID));
                    textblock.Inlines.Add("\r\n");
                }
                else
                {
                    textblock.Inlines.Add("- " + t.Name + "\r\n");
                }
            }
        }
        private void RunButtonsListUpdate()
        {
            int i = 0;
            int full = 0, partial = 0, invisible = 0;
            UIElementCollection recipeButtons = RecipeButtonStack.Children;
            foreach(UIElement e in RecipeStepStack.Children)
            {
                if(i >= recipeButtons.Count)
                {
                    break;
                }
                switch (IsUserElementVisible(e, Application.Current.MainWindow))
                {
                    case RecipeUserVisible.full:
                        full++;
                        break;

                    case RecipeUserVisible.partial:
                        partial += 1;
                        break;

                    case RecipeUserVisible.invisible:
                        invisible += 1;
                        break;
                }
            }
        }

        private RecipeUserVisible IsUserElementVisible(UIElement element, FrameworkElement container)
        {
            if (!element.IsVisible)
                return RecipeUserVisible.invisible;

            RecipeUserVisible visibility = RecipeUserVisible.invisible;
            if (container == null) throw new ArgumentNullException("container");

            Rect bounds = element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.RenderSize.Width, element.RenderSize.Height));
            Rect rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            
            if (rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight) || rect.IntersectsWith(bounds))
            {
                visibility = RecipeUserVisible.partial;
            }
            if (rect.Contains(bounds))
            {
                visibility = RecipeUserVisible.full;
            }
            return visibility;
        }

        public Run CreateInTextToolTip(string text, int ToolTipID)
        {

            Run popup_link = new Run();
            popup_link.Cursor = Cursors.Hand;
            popup_link.TextDecorations = TextDecorations.Underline;
            popup_link.Foreground = new SolidColorBrush(Colors.RoyalBlue);
            popup_link.MouseUp += new MouseButtonEventHandler(ToolTip_Click);
            popup_link.Text = text;
            popup_link.Tag = ToolTipID;
            return popup_link;
        }

        private void ToolTip_Click(object sender, RoutedEventArgs e)
        {
            Run link = (Run)sender;
            if((int)link.Tag != 0)
            {
                Logic.RecipeComponents.ToolTip tip = recipe.GetToolTip((int)link.Tag);
                if(tip == null)
                {
                    return;
                }
                PopupContent.TooltipImage.Visibility = Visibility.Collapsed;
                if(tip.Images.Count > 0)
                {
                    if (File.Exists("Images/" + tip.Images[0]))
                    {
                        Image img = new Image();
                        PopupContent.TooltipImage.Source = new BitmapImage(new Uri("/Images/" + tip.Images[0], UriKind.Relative));
                        PopupContent.TooltipImage.Visibility = Visibility.Visible;
                    }
                }
                PopupContent.TooltipText.Text = tip.Text;
                InformationPopup.IsOpen = true;
            }
        }

        // Detect when the view has been scrolled so we close popups.
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            InformationPopup.IsOpen = false;
            RunButtonsListUpdate();
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
