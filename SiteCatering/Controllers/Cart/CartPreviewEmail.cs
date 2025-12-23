using Microsoft.AspNetCore.Mvc;

namespace SiteCatering.Controllers.Cart
{
    public class CartPreviewEmail : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
