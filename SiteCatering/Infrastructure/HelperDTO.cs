using SiteCatering.Domain.Entities;
using SiteCatering.Models;

namespace SiteCatering.Infrastructure
{
    public static class HelperDTO
    {
        public static DishesDTO TransormDishInDTO(Dish entity)
        {
            DishesDTO dishesDTO = new DishesDTO();

            dishesDTO.id = entity.Id;
            dishesDTO.FoodCategoryEnum = entity.FoodCategoryEnum;
            dishesDTO.MenuCategoryEnum = entity.MenuCategoryEnum;
            dishesDTO.Description = entity.Description;
            dishesDTO.Price = entity.Price;
            dishesDTO.Kkal = entity.Kkal;
            dishesDTO.Photo = entity.Photo;
            dishesDTO.Weight = entity.Weight;
            dishesDTO.Сompound = entity.Сompound;
            dishesDTO.Name = entity.Name;
            dishesDTO.IsRecomended = entity.IsRecommended;

            return dishesDTO;
        }

        public static IEnumerable<DishesDTO> TransformDishInDTO(IEnumerable<Dish> entities)
        {
            List<DishesDTO> entitiesDTO = new List<DishesDTO>();
            foreach (Dish entity in entities) 
                entitiesDTO.Add(TransormDishInDTO(entity));

            return entitiesDTO;
        }

    }
}
