using Microsoft.AspNetCore.Mvc;
using SiteCatering.Domain;
using SiteCatering.Domain.Entities;
using SiteCatering.Infrastructure;
using SiteCatering.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCatering.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public async Task<IActionResult> Index()
        {

            IEnumerable<Dish> list = await _dataManager.DishRepository.GetDishesAsync();


            IEnumerable<Dish> recommendedDishes = list.Where(d => d.IsRecommended);

            IEnumerable<DishesDTO> recommendedDishesDTO = HelperDTO.TransformDishInDTO(recommendedDishes);


            return View(recommendedDishesDTO);  
        }
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}
