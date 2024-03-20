using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Buffet.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly BuffetContext _db;

        public RestaurantController(BuffetContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Total()
        {
            return View();
        }

        public IActionResult Store(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ระบุ ID";
                return RedirectToAction("Index");
            }
            var obj = _db.Restaurants.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบ ID";
                return RedirectToAction("Index");
            }
            var shop = from i in _db.Restaurants
                       where i.ResId.Equals(id)
                       select new Pdvm
                       {
                           ResId = i.ResId,
                           ResName = i.ResName,
                           ResImg = i.ResImg,
                           ResPhone = i.ResPhone,
                           ResAvg = i.ResAvg,
                           ResLocation = i.ResLocation
                       };
            return View(shop);
        }

        public IActionResult Reserve()
        {

            return View();
        }

    }
}
