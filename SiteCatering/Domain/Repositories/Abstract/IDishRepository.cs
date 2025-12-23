using SiteCatering.Domain.Entities;
using SiteCatering.Domain.Enums;

namespace SiteCatering.Domain.Repositories.Abstract
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetDishesAsync();
        Task<Dish?> GetDishByIdAsync(int id);
        Task SaveDishAsync(Dish dish);
        Task DeleteDishAsync(int id);
        Task<IEnumerable<Dish>> GetDishesByCategoryAsync(MenuCategoryEnum category);
        Task<IEnumerable<Dish>> GetRecommendedDishesAsync();
    }
}
