namespace DatadrivenApplicationEFCore.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Quantity {  get; set; } = string.Empty;
    }
}
