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
	public class Recipe
	{
		public string Name { get; set; }

		Dictionary<string, RecipeStep> _steps;
		public Dictionary<string, RecipeStep> Steps { get { return _steps ?? (_steps = new Dictionary<string, RecipeStep>()); } set { _steps = value; } }
	}
}
