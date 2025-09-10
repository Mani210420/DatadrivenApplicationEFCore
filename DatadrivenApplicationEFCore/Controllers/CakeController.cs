using DatadrivenApplicationEFCore.Models;
using DatadrivenApplicationEFCore.Models.Repositories;
using DatadrivenApplicationEFCore.Utilities;
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
            if (id == null)
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

        public async Task<IActionResult> Delete(int id)
        {
            var selectedCake = await _cakeRepository.GetCakeByIdAsync(id);
            return View(selectedCake);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the cake failed, invalid ID!";
                return View();
            }
            try
            {
                await _cakeRepository.DeleteCakeAsync(id.Value);
                TempData["CakeDeleted"] = "Cake deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the cake failed, please try again! Error: {ex.Message}";
            }
            var selectedCake = await _cakeRepository.GetCakeByIdAsync(id.Value);
            return View(selectedCake);
        }

        private int pageSize = 2;

        public async Task<IActionResult> IndexPaging(int? pageNumber)
        {
            var cakes = await _cakeRepository.GetCakesPagedAsync(pageNumber, pageSize);
            pageNumber ??= 1;

            var count =await _cakeRepository.GetAllCakesCountAsync();

            return View(new PagedList<Cake>(cakes.ToList(), pageNumber.Value, pageSize, count));
        }

        public async Task<IActionResult> IndexPagingSorting(string sortBy, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortBy;

            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortBy) || sortBy == "id_desc" ? "id" : "id_desc";
            ViewData["NameSortParam"] = sortBy == "name" ? "name_desc" : "name";
            ViewData["PriceSortParam"] = sortBy == "price" ? "price_desc" : "price";

            pageNumber ??= 1;

            var cakes = await _cakeRepository.GetCakesSortedAndPagedAsync(sortBy, pageNumber,pageSize);
            var count = await _cakeRepository.GetAllCakesCountAsync();

            return(View(new PagedList<Cake>(cakes.ToList(),pageNumber.Value, pageSize, count)));
        }

        public async Task<IActionResult> Search(string? searchQuery, int?  searchCategory)
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);

            if(searchQuery != null)
            {
                var cakes =await _cakeRepository.SearchCakes(searchQuery, searchCategory);
                return View(new CakeSearchViewModel()
                {
                    Cakes = cakes,
                    Categories = selectListItems,
                    SearchCategory = searchCategory,
                    SearchQuery = searchQuery
                });
            }
            return View(new CakeSearchViewModel() 
            {
                Categories = selectListItems,
                Cakes = new List<Cake>(),
                SearchCategory = null,
                SearchQuery = String.Empty
            });
        }
    }
}