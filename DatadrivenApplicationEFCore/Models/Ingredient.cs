using System.ComponentModel.DataAnnotations;

namespace DatadrivenApplicationEFCore.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [StringLength(50, ErrorMessage = "The name should be no longer than 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "The quantity should be no longer than 100 characters.")]
        [Display(Name = "Quantity")]
        public string Quantity {  get; set; } = string.Empty;
    }
}
