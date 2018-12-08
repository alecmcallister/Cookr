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
    public partial class RecipeStepLayout : UserControl
    {
        public RecipeStepLayout()
        {
            InitializeComponent();
        }
        public RecipeStepLayout(RecipeStep step, Recipe recipePage)
        {
            InitializeComponent();
            StepTitle.Text = step.Number.ToString() + ". " + step.Title;
            if(step.Warning != null && step.Warning.Length > 0)
            {
                StepWarningPanel.Visibility = Visibility.Visible;
                StepWarning.Text = step.Warning;
            }
            
            StepInstruction.Inlines.Clear();
            List<RecipeStep.StepTips> tipList = new List<RecipeStep.StepTips>(step.stepTips);
            ProcessTextForTips(step.StepText, tipList, recipePage);

            // Add the list of images for the step
            foreach (string i in step.Images)
            {
                if(File.Exists("Images/" + i))
                {
                    RoundedCornerImage roundedImage = new RoundedCornerImage();
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("Images/" + i, UriKind.Relative));
                    roundedImage.RoundedCornerImageGrid.Height = 180;
                    roundedImage.RoundedCornerImageGrid.Width = (180.0 / img.Source.Height) * img.Source.Width;

                    ImageBrush image = new ImageBrush(img.Source) { Stretch = Stretch.UniformToFill,  };
                    roundedImage.ImageBrushContent.Background = image;

                    ScaleTransform st = new ScaleTransform(1.05f, 1.05f, 0.5f, 0.5f);
                    roundedImage.ImageBrushContent.Background.RelativeTransform = st;

                    ImageStackPanel.Children.Add(roundedImage);
                }
            }


        }

        /// <summary>
        /// Recursive adventure to add tips to the first occurence of the word they should be giving you a tip about
        /// </summary>
        /// <param name="stepText">The text we want to put the tip inside of</param>
        /// <param name="tipStack">The List of tips left to be inserted</param>
        /// <param name="recipePage">Reference to the recipePage so we can call CreateInTextToolTip.</param>
        private List<RecipeStep.StepTips> ProcessTextForTips(string stepText, List<RecipeStep.StepTips> tipList, Recipe recipePage)
        {
            if(tipList.Count == 0)
            {
                if(stepText.Length > 0)
                {
                    StepInstruction.Inlines.Add(stepText);
                }
                return tipList;
            }
            else
            {
                for(int i = 0; i < tipList.Count; i++)
                {
                    RecipeStep.StepTips tip = tipList[i];
                    if (stepText.Contains(tip.TargetText))
                    {
                        string[] upperlower = stepText.Split(new string[] { tip.TargetText }, 2, StringSplitOptions.None);
                        //upperlower[1] = upperlower[1].Substring(tip.TargetText.Length - 1);
                        // Remove the tip from the list so other iterations of this function don't use it again.
                        tipList.Remove(tip);

                        // Process the text before the word we're inserting as a tooltip
                        tipList = ProcessTextForTips(upperlower[0], tipList, recipePage);

                        // Actually add the tooltop
                        StepInstruction.Inlines.Add(recipePage.CreateInTextToolTip(tip.TargetText, tip.ToolTipID));

                        // Process the text following the tooltip
                        tipList = ProcessTextForTips(upperlower[1], tipList, recipePage);
                        return tipList;
                    }
                }
            }
            if (stepText.Length > 0)
            {
                StepInstruction.Inlines.Add(stepText);
            }
            return tipList;
        }
    }
}
