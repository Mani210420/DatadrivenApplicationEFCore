using DatadrivenApplicationEFCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatadrivenApplicationEFCore.ViewModels
{
    public class CakeAddViewModel
    {
        public IEnumerable<SelectListItem>? Categories { get; set; } = default!;
        public Cake? Cake { get; set; }
    }
}
