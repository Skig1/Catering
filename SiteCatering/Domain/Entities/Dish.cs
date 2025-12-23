using SiteCatering.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteCatering.Domain.Entities
{
    public class Dish : EntityBase
    {
        [Display(Name="Выберите в какое меню добавить")]
        public MenuCategoryEnum MenuCategoryEnum { get; set; }

        [Display(Name = "Выберите категорию блюда")]
        public FoodCategoryEnum FoodCategoryEnum { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Калорийность блюда")]
        public int? Kkal {  get; set; }

        [Display(Name = "Цена")]
        public int? Price { get; set; }

        [Display(Name = "Состав")]
        public string? Сompound { get; set; }

        [Display(Name = "Вес")]
        public int? Weight { get; set; }

        [Display(Name = "Фотография блюда")]
        public string? Photo {  get; set; }

        public bool IsRecommended { get; set; } = false;
    }
}
