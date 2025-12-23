using Microsoft.AspNetCore.Mvc;
using SiteCatering.Domain.Entities;

namespace SiteCatering.Controllers.Admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> DishesEdit(int id)
        {
            Dish? entity = id == default ? new Dish() : await _dataManager.DishRepository.GetDishByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DishesEdit(Dish dish, IFormFile? TitleImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(dish);
            }

            if (TitleImageFile != null)
            {
                dish.Photo = TitleImageFile.FileName;
                await SaveImg(TitleImageFile);
            }

            await _dataManager.DishRepository.SaveDishAsync(dish);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DishesDelete(int id)
        {
            await _dataManager.DishRepository.DeleteDishAsync(id);

            return RedirectToAction("Index");
        }
    }
}