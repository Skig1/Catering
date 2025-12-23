using Microsoft.AspNetCore.Mvc;
using SiteCatering.Domain;
using SiteCatering.Domain.Entities;
using SiteCatering.Domain.Enums;
using SiteCatering.Infrastructure;
using SiteCatering.Models;

namespace SiteCatering.Controllers
{
    public class DishesController : Controller
    {
        private readonly DataManager _dataManager;

        public DishesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public async Task<IActionResult> Index(string category)
        {
            IEnumerable<Dish> list;

            if (string.IsNullOrEmpty(category))
            {
                list = await _dataManager.DishRepository.GetDishesAsync();
            }
            else
            {

                if (Enum.TryParse<MenuCategoryEnum>(category, out var enumCategory))
                {
                    list = await _dataManager.DishRepository.GetDishesByCategoryAsync(enumCategory);
                }
                else
                {

                    list = await _dataManager.DishRepository.GetDishesAsync();
                }
            }

            IEnumerable<DishesDTO> dishesDTO = HelperDTO.TransformDishInDTO(list);
            return View(dishesDTO);
        }

        public async Task<IActionResult> Show(int id)
        {
            Dish? entity = await _dataManager.DishRepository.GetDishByIdAsync(id);

            if (entity == null)
                return NotFound();

            DishesDTO entityDTO = HelperDTO.TransormDishInDTO(entity);

            return View(entityDTO);
        }
    }
}
