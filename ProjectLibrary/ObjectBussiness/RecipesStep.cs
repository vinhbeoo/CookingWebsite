using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.ObjectBussiness;

public partial class RecipesStep
{
	[Key]
	public int Step { get; set; }
	[Key]
	public int? RecipeId { get; set; }

	public string? Description { get; set; }

	public string? ImageUrl { get; set; }

	public string? VideoUrl { get; set; }

	public virtual Recipe? Recipe { get; set; }
}
