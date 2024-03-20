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
    }
}
