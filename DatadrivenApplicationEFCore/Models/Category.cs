using System.ComponentModel.DataAnnotations;

namespace DatadrivenApplicationEFCore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "The name should be no longer than 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "The description should be no longer than 1000 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name="Data Added")]
        public DateTime DateAdded { get; set; }
        public ICollection<Cake>? Cakes { get; set; }
    }
}
