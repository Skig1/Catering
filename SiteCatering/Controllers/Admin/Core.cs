using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteCatering.Domain;

namespace SiteCatering.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public partial class AdminController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            _dataManager = dataManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Dishes = await _dataManager.DishRepository.GetDishesAsync();
            return View();
        }

      
        [HttpGet]
        public async Task<IActionResult> RecommendedDishes()
        {
            ViewBag.Dishes = await _dataManager.DishRepository.GetDishesAsync();  
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RecommendedDishes(int[] selectedIds)  
        {     
            var allDishes = await _dataManager.DishRepository.GetDishesAsync();

          
            foreach (var dish in allDishes)
            {
                dish.IsRecommended = selectedIds.Contains(dish.Id);
                await _dataManager.DishRepository.SaveDishAsync(dish);  
            }


            return RedirectToAction("RecommendedDishes");
        }

        public async Task<string> SaveImg(IFormFile img)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "img/", img.FileName);
            await using FileStream stream = new FileStream(path, FileMode.Create);
            await img.CopyToAsync(stream);
            return path;
        }
    }

}