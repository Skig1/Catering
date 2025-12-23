namespace SiteCatering.Models
{
    public class CartItem
    {

        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }     
        public int Weight { get; set; }    
        public string? PhotoUrl { get; set; } 
        public int Quantity { get; set; } = 1;

        public int TotalPrice => Price * Quantity;
        public int TotalWeight => Weight * Quantity;

      
        public CartItem(int dishId, string name, int price, int weight, string? photoUrl = null)
        {
            DishId = dishId;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Название блюда не может быть null");
            Price = price >= 0 ? price : throw new ArgumentException("Цена не может быть отрицательной");
            Weight = weight >= 0 ? weight : throw new ArgumentException("Вес не может быть отрицательным");
            PhotoUrl = photoUrl;
            Quantity = 1;
        }

       
        public void SetQuantity(int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("Количество должно быть ≥ 1");
            Quantity = quantity;
        }
    }
    
}

