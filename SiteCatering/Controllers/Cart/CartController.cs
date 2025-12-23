using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteCatering.Domain;
using SiteCatering.Domain.Entities;
using SiteCatering.Infrastructure;
using SiteCatering.Models;
using System.Text.Json;

namespace SiteCatering.Controllers.Cart
{
    public class CartController : Controller
    {
        private const string SessionKey = "Cart";
        private readonly DataManager _dataManager;

        public CartController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        private List<CartItem> GetCart()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(sessionData))
                return new List<CartItem>();

            try
            {
                return JsonSerializer.Deserialize<List<CartItem>>(sessionData) ?? new List<CartItem>();
            }
            catch
            {

                return new List<CartItem>();
            }
        }


        private void SaveCart(List<CartItem> cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(SessionKey, json);
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int dishId, int quantity = 1)
        {
          
            if (quantity < 1 || quantity > 99)
            {
                TempData["ErrorMessage"] = "Количество должно быть от 1 до 99.";
                return Redirect(Request.Headers["Referer"].ToString()); 
            }

         
            Dish? dish = await _dataManager.DishRepository.GetDishByIdAsync(dishId);
            if (dish == null)
            {
                TempData["ErrorMessage"] = $"Блюдо с ID {dishId} не найдено.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

    
            if (string.IsNullOrEmpty(dish.Name) || dish.Price == null || dish.Weight == null)
            {
                TempData["ErrorMessage"] = "Недостаточно данных о блюде.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(item => item.DishId == dishId);

            if (existingItem != null)
            {
                existingItem.SetQuantity(existingItem.Quantity + quantity);
            }
            else
            {

                var cartItem = new CartItem(
                dishId: dish.Id,
                name: dish.Name,
                price: dish.Price ?? 0,
                weight: dish.Weight ?? 0,
                photoUrl: dish.Photo
                );
                cartItem.SetQuantity(quantity);
                cart.Add(cartItem);
            }

            SaveCart(cart);
            TempData["SuccessMessage"] = "Товар добавлен в корзину!";
            return StatusCode(204); 

        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int dishId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(item => item.DishId == dishId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
                TempData["SuccessMessage"] = "Товар удалён из корзины.";
            }
            else
            {
                TempData["ErrorMessage"] = "Товар не найден в корзине.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantityAjax(int dishId, int quantity)
        {
            if (quantity < 1 || quantity > 99)
                return BadRequest("Количество должно быть от 1 до 99.");

            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.DishId == dishId);

            if (item == null)
                return NotFound("Товар не найден в корзине.");


            item.SetQuantity(quantity);
            SaveCart(cart);

 
            return Json(new
            {
                success = true,
                totalPrice = item.TotalPrice,
                totalCartPrice = cart.Sum(i => i.TotalPrice)
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(SessionKey);
            TempData["SuccessMessage"] = "Корзина очищена.";
            return RedirectToAction("Index");
        }

       
        public IActionResult Preview()
        {
            var cart = GetCart();  
            return View("Preview", cart);  
        }

        
     
        public IActionResult DownloadDocx()
        {
            var cart = GetCart();
            var stream = new MemoryStream();

            using (var doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();

              
                var document = new Document();
                var body = new Body();
                document.Body = body;  
                mainPart.Document = document;

           
                var titleParagraph = new Paragraph(new Run(new Text("Заказ на кейтеринг от \"Прованские травы\"")));
                body.Append(titleParagraph);

       
                var orderInfoParagraph = new Paragraph(new Run(new Text($"Заказ гостя. Дата: {DateTime.Now:dd.MM.yyyy}")));
                body.Append(orderInfoParagraph);

   
                body.Append(new Paragraph());


                var sectionTitleParagraph = new Paragraph(new Run(new Text("Состав заказа:")));
                body.Append(sectionTitleParagraph);

                body.Append(new Paragraph());

         
                if (cart.Any())
                {
                    var table = new Table();

                   
                    var headerRow = new TableRow(
                        new TableCell(new Paragraph(new Run(new Text("Название")))),
                        new TableCell(new Paragraph(new Run(new Text("Количество")))),
                        new TableCell(new Paragraph(new Run(new Text("Цена за шт.")))),
                        new TableCell(new Paragraph(new Run(new Text("Итого")))),
                        new TableCell(new Paragraph(new Run(new Text("Вес на порцию (г)")))),
                        new TableCell(new Paragraph(new Run(new Text("Вес всего (г)"))))
                    );
                    table.Append(headerRow);

               
                    foreach (var item in cart)
                    {
                        var dataRow = new TableRow(
                            new TableCell(new Paragraph(new Run(new Text(item.Name)))),
                            new TableCell(new Paragraph(new Run(new Text(item.Quantity.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text($"{item.Price}")))),
                            new TableCell(new Paragraph(new Run(new Text($"{item.TotalPrice}")))),
                            new TableCell(new Paragraph(new Run(new Text(item.Weight.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(item.TotalWeight.ToString()))))
                        );
                        table.Append(dataRow);
                    }

                    body.Append(table);
                }
                else
                {
                   
                    body.Append(new Paragraph(new Run(new Text("Корзина пуста."))));
                }

             
                if (cart.Any())
                {
                    body.Append(new Paragraph());  
                    body.Append(new Paragraph(new Run(new Text($"Общая сумма: {cart.Sum(i => i.TotalPrice)} руб."))));
                    body.Append(new Paragraph(new Run(new Text($"Общий вес: {cart.Sum(i => i.TotalWeight)} г."))));
                }

                doc.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "cart.docx");
        }

    }
}
