using DatadrivenApplicationEFCore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DatadrivenApplicationEFCore.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;

        public CakeController(ICakeRepository cakeRepository)
        {
            _cakeRepository = cakeRepository;
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
    }
}
