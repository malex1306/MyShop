using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Dtos;

namespace MyShop.Web.Controllers.Order
{
    public class OrderViewController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Zeigt z.B. das Formular zum Erstellen
        }

        [HttpPost]
        public IActionResult Create(CreateOrderDto dto)
        {
            // ggf. Services aufrufen
            return RedirectToAction("Success");
        }
    }
}
