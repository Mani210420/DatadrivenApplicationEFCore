using System.ComponentModel.DataAnnotations;

namespace DatadrivenApplicationEFCore.Models
{
    public class Cake
    {
        public int CakeId { get; set; }

        [Display(Name ="Name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ShortDescription { get; set; }

        [StringLength(1000)]
        public string? LongDescription { get; set; }

        [StringLength(1000)]
        public string? AllergyInfo { get; set; }

        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnail { get; set; }
        public bool IsCakeOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Ingredient>? Ingredients { get; set; }
    }
}
