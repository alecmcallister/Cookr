using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookr
{
	/// <summary>
	/// THIS WAS DONE IN 2 MINUTES
	/// </summary>
	[Serializable]
	public class RecipeStep
	{
		public string Title { get; set; }
		public string Content { get; set; }

		public Dictionary<string, string> Tooltips;

		public RecipeStep() : this("", "") { }

		public RecipeStep(string title, string content)
		{
			Title = title;
			Content = content;
		}
	}
}
