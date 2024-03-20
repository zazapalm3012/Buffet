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
            DateOnly thedate = DateOnly.FromDateTime(DateTime.Now.Date);
            var rep = from b in _db.Books

                      join r in _db.Restaurants on b.ResId equals r.ResId into join_r
                      from b_r in join_r
                      
                      join co in _db.Courses on b.CourseId equals co.CourseId into join_co
                      from b_co in join_co
                      
                      join t in _db.Tables on b.TableId equals t.TableId into join_t
                      from b_t in join_t

                      select new Universal
                      {
                          BookId = g.BookId,
                          ResId = g.ResId,
                          CourseId = g.CourseId,
                          TableId = g.TableId,
                          
                      };
            return View(rep);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(DateOnly thedate)
        {
            var rep = from b in _db.Books

                      join r in _db.Restaurants on b.ResId equals r.ResId into join_r
                      from b_r in join_r

                      join co in _db.Courses on b.CourseId equals co.CourseId into join_co
                      from b_co in join_co

                      join t in _db.Tables on b.TableId equals t.TableId into join_t
                      from b_t in join_t

                      select new Universal
                      {
                          BookId = g.BookId,
                          ResId = g.ResId,
                          CourseId = g.CourseId,
                          TableId = g.TableId,

                      };
            return View(rep);

        }

    }
}
