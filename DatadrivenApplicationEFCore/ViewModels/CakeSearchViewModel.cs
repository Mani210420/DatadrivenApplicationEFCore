using DatadrivenApplicationEFCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatadrivenApplicationEFCore.ViewModels
{
    public class CakeSearchViewModel
    {
        public IEnumerable<Cake>? Cakes { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; } = default!;
        public string? SearchQuery { get; set; }
        public int? SearchCategory { get; set; }
    }
}
