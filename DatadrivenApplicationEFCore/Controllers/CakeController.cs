using DatadrivenApplicationEFCore.Models;
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
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                IEnumerable<SelectListItem> selectListItems = new SelectList(categories, "CategoryId", "Name", null);
                var cakeAddViewModel = new CakeAddViewModel()
                {
                    Categories = selectListItems
                };
                return View(cakeAddViewModel);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"There is an error: {ex.Message}";
            }
            return View(new CakeAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CakeAddViewModel cakeAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var cake = new Cake()
                {
                    Name = cakeAddViewModel.Cake.Name,
                    CategoryId = cakeAddViewModel.Cake.CategoryId,
                    ShortDescription = cakeAddViewModel.Cake.ShortDescription,
                    LongDescription = cakeAddViewModel.Cake.LongDescription,
                    Price = cakeAddViewModel.Cake.Price,
                    AllergyInfo = cakeAddViewModel.Cake.AllergyInfo,
                    ImageThumbnail = cakeAddViewModel.Cake.ImageThumbnail,
                    ImageUrl = cakeAddViewModel.Cake.ImageUrl,
                    InStock = cakeAddViewModel.Cake.InStock,
                    IsCakeOfTheWeek = cakeAddViewModel.Cake.IsCakeOfTheWeek,
                };
                await _cakeRepository.AddCakeAsync(cake);
                return RedirectToAction(nameof(Index));
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);
            cakeAddViewModel.Categories = selectListItems;
            return View(cakeAddViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);

            var selectedCake = await _cakeRepository.GetCakeByIdAsync(id.Value);
            CakeEditViewModel cakeEditViewModel = new CakeEditViewModel()
            {
                Categories = selectListItems,
                Cake = selectedCake
            };
            return View(cakeEditViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CakeEditViewModel cakeEditViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cakeRepository.UpdateCakeAsync(cakeEditViewModel.Cake);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", $"Updating cake details failed.. Error: {ex.Message}");
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);
            cakeEditViewModel.Categories = selectListItems;
            return View(cakeEditViewModel);
        }
    }
}
