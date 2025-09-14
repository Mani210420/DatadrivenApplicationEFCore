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
        [Display(Name ="Short Description")]
        public string? ShortDescription { get; set; }

        [StringLength(1000)]
        [Display(Name = "LongDescription")]
        public string? LongDescription { get; set; }

        [StringLength(1000)]
        [Display(Name = "AllergyInfo")]
        public string? AllergyInfo { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "ImageUrl")]
        public string? ImageUrl { get; set; }

        [Display(Name = "ImageThumbnail")]
        public string? ImageThumbnail { get; set; }

        [Display(Name = "IsCakeOfTheWeek")]
        public bool IsCakeOfTheWeek { get; set; }

        [Display(Name = "InStock")]
        public bool InStock { get; set; }

        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public Category? Category { get; set; }

        [Display(Name = "Ingredients")]
        public ICollection<Ingredient>? Ingredients { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
