using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;

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
            var sessionId = HttpContext.Session.GetString("ResId");
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
            
            obj.SelectDate = DateTime.Now;
            var bookIdCount = (from id in _db.Books select id).Count();
            obj.BookId = "B" + 00 + bookIdCount;
            var tableUpdate = _db.ResTableTotals.FirstOrDefault();
            if (obj.BookSeat <= 4)
            {
                obj.TableId = "S";
                tableUpdate.Stotal -= 1;
            }
            else if (obj.BookSeat <= 6)
            {
                obj.TableId = "M";
                tableUpdate.Mtotal -= 1;
            }
            else if (obj.BookSeat <= 10 && obj.BookSeat > 6)
            {
                obj.TableId = "L";
                tableUpdate.Ltotal -= 1;
            }

            _db.SaveChanges();
            
            string serializedBook = JsonConvert.SerializeObject(obj);
            TempData["ReservedBook"] = serializedBook;

            return RedirectToAction("Total");

        }
        public IActionResult Total()
        {
            var ids = HttpContext.Session.GetString("ResId");

             var Total = from t in _db.Books
                         where t.ResId.Equals(ids)

                         join c in _db.Courses on t.CourseId equals c.CourseId into join_c
                          from t_c in join_c


                         join r in _db.Restaurants on t.ResId equals r.ResId into join_r
                         from t_r in join_r


                         join p in _db.Payments on t.BookId equals p.BookId into join_p
                         from t_p in join_p

                    select new Pmvm
                         {
                             ResId = t.ResId,
                             ResName = t_r.ResName,
                             ResDtl = t_r.ResDtl,
                           /* CourseName = t_c.CourseName,
                             CoursePrice = t_c.CoursePrice,
                             CourseDtl = t_c.CourseDtl,
                             CourseType = t_c.CourseType*/

                         };
            

            if (Total == null)
            {
                return NotFound();
            }


            return View(Total);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Total(Payment obj)
        {
            

            /*string serializedBook = TempData["ReservedBook"] as string;
            if (!string.IsNullOrEmpty(serializedBook))
            {
                Book reservedBook = JsonConvert.DeserializeObject<Book>(serializedBook);
                if (reservedBook != null)
                {
                    _db.Books.Add(reservedBook);
                    _db.SaveChanges();
                }

            }*/
            return View();
        }


        public IActionResult Payment(string method, string Cardnum, string Exp, string ccv) 
        {
            string serializedBook = TempData["ReservedBook"] as string;
            if (!string.IsNullOrEmpty(serializedBook))
            {
                Book reservedBook = JsonConvert.DeserializeObject<Book>(serializedBook);
                if (reservedBook != null)
                {
                    _db.Books.Add(reservedBook);
                    _db.SaveChanges();
                }

            }
            var PayIdCount = (from pay in _db.Payments select pay).Count();
            var PayId = "P" + 00 + PayIdCount;
            var BookIdCount = (from bookid in _db.Books select bookid).Count();
            var BookId = "B" + 00 + PayIdCount;
            Payment pays = new Payment() { PayId = PayId, CardId  = Cardnum , CardExpire = Exp, CcvNum  =ccv, PayType  = method, BookId = BookId };
            _db.Payments.Add(pays);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
