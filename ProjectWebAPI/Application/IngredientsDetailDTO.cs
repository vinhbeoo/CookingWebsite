namespace ProjectWebAPI.Application
{
	public class IngredientsDetailDTO
	{
		public int IngredientId { get; set; }

		public int Stt { get; set; }

		public string Description { get; set; } = null!;

		public int RecipeId { get; set; }
	}
}
