using SiteCatering.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteCatering.Models
{
    public class DishesDTO
    {
        public int id { get; set; }
        public MenuCategoryEnum MenuCategoryEnum { get; set; }
        public FoodCategoryEnum FoodCategoryEnum { get; set; }
        public string? Description { get; set; }
        public int? Kkal { get; set; }
        public int? Price { get; set; }
        public string? Сompound { get; set; }
        public int? Weight { get; set; }
        public string? Photo { get; set; }
        public string? Name { get; set; }

        public bool? IsRecomended {  get; set; } = false;
    }
}
