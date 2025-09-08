using DatadrivenApplicationEFCore.Models;
using DatadrivenApplicationEFCore.Models.Repositories;
using DatadrivenApplicationEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatadrivenApplicationEFCore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            CategoryListViewModel model = new CategoryListViewModel()
            {
                Categories = (await _categoryRepository.GetAllCategoriesAsync()).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCategory =await _categoryRepository.GetCategoryByIdAsync(id.Value);
            return View(selectedCategory);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name, Description, DateAdded")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.AddCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", $"Adding category failed, Please try again.. Error: {ex.Message}");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var selectCategory =await _categoryRepository.GetCategoryByIdAsync(id.Value);
            return View(selectCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.UpdateCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating category failed please try again! error:{ex.Message}");
            }

            return View(category);
        }
    }
}
