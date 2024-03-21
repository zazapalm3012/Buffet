using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
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
            HttpContext.Session.SetString("ResId", id);
            var shop = from i in _db.Restaurants
                       where i.ResId.Equals(id)

                       join c in _db.Courses on i.CourseId equals c.CourseId into join_c
                       from i_c in join_c

                       select new Pdvm
                       {
                           ResId = i.ResId,
                           ResName = i.ResName,
                           ResImg = i.ResImg,
                           ResPhone = i.ResPhone,
                           ResAvg = i.ResAvg,
                           ResLocation = i.ResLocation,
                           ResDtl = i.ResDtl,
                           CourseId = i.CourseId,
                           CourseName = i_c.CourseName,
                           CoursePrice = i_c.CoursePrice,
                           CourseDtl = i_c.CourseDtl,
                           CourseType = i_c.CourseType

                       };
            return View(shop);
        }

        public IActionResult Reserve()
        {

            if (HttpContext.Session.GetString("CusId") == null)
            {
                TempData["ErrorMessage"] = "กรุณาล็อกอิน";
                return RedirectToAction("Login", "Home");
            }
            DateTime theDate = DateTime.Now.Date;
            var rep = from b in _db.Books

                      join co in _db.Courses on b.CourseId equals co.CourseId into join_co
                      from b_co in join_co

                      join t in _db.Tables on b.TableId equals t.TableId into join_t
                      from b_t in join_t

                      select new Book
                      {
                          BookId = b.BookId,
                          ResId = b.ResId,
                          CourseId = b.CourseId,
                          TableId = b.TableId,

                      };
            // return View(rep);
            ViewData["Crs"] = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(Book obj)
        {
        /*    var rep = from b in _db.Books

                      join co in _db.Courses on b.CourseId equals co.CourseId into join_co
                      from b_co in join_co

                      join t in _db.Tables on b.TableId equals t.TableId into join_t
                      from b_t in join_t

                      select new Book
                      {
                          BookId = b.BookId,
                          ResId = b.ResId,
                          CourseId = b.CourseId,
                          TableId = b.TableId,

                      };*/
            obj.SelectDate = DateTime.Now;
            var bookIdCount = (from id in _db.Books select id).Count();
            obj.BookId = "B" + 00 + bookIdCount;
            if (obj.BookSeat <= 4)
            {
                obj.TableId = "A1";
            }
            else if (obj.BookSeat <= 6)
            {
                obj.TableId = "A2";
            }
            else if (obj.BookSeat <= 10)
            {
                obj.TableId = "A3";
            }

            TempData["data"] = obj;
            return RedirectToAction("Total");

        }


        public IActionResult Payment()
        {
            var obj = TempData["data"];
            _db.Books.Add((Book)obj);
            _db.SaveChanges();

            return View();
        }

    }
}
