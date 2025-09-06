using DatadrivenApplicationEFCore.Models.Repositories;
using DatadrivenApplicationEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatadrivenApplicationEFCore.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CakeController(ICakeRepository cakeRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cakes = await _cakeRepository.GetAllCakesAsync();
            return View(cakes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cake = await _cakeRepository.GetCakeByIdAsync(id);
            return View(cake);
        }

        public async Task<IActionResult> Add() 
        {
            var categories =await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(categories, "CategoryId", "Name", null);
            var cakeAddViewModel = new CakeAddViewModel()
            {
                Categories = selectListItems
            };
            return View(cakeAddViewModel);
        }
    }
}
