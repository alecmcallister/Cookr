using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Cookr
{
	[Serializable, XmlRoot("Recipe")]
	public class RecipeObject
	{
		[XmlElement("Title")]
		public string Title { get; set; }

		[XmlElement("PopularToday")]
		public bool PopularToday { get; set; }

		[XmlArray("Tags"), XmlArrayItem("Tag")]
		public List<string> Tags { get; set; }

		[XmlElement("Rating")]
		public double Rating { get; set; }

		public int UserRating { get; set; }


		[XmlElement("TitleImage")]
		public string TitleImage { get; set; }

		public BitmapImage TitleBitmap
		{
			get
			{
				return new BitmapImage(new Uri("Images/" + TitleImage, UriKind.Relative));
			}
		}

		[XmlElement("RecipeIntroduction")]
		public string RecipeIntroduction { get; set; }

		[XmlElement("PrepTime")]
		public int PrepTime { get; set; }

		[XmlElement("CookTime")]
		public int CookTime { get; set; }

		[XmlElement("TotalTime")]
		public int TotalTime { get; set; }

		[XmlElement("NumberOfServings")]
		public int NumberOfServings { get; set; }

		[XmlArray("Ingredients"), XmlArrayItem("Ingredient")]
		public List<Ingredient> Ingredients { get; set; }

		[XmlArray("Tools"), XmlArrayItem("Tool")]
		public List<Tool> Tools { get; set; }

		[XmlArray("ToolTips"), XmlArrayItem("ToolTip")]
		public List<ToolTip> ToolTips { get; set; }

		[XmlArray("RecipeSteps"), XmlArrayItem("RecipeStep")]
		public List<RecipeStep> RecipeSteps { get; set; }

		public ToolTip GetToolTip(int ID)
		{
			foreach (ToolTip tip in ToolTips)
			{
				if (tip.Id == ID)
				{
					return tip;
				}
			}
			return null;
		}
	}
}
