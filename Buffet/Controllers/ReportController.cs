using Buffet.Controllers;
using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Runtime.Intrinsics.X86;

namespace Buffet.Controllers
{
    public class ReportController : Controller
    {
        readonly BuffetContext _db;
        public ReportController(BuffetContext db) { _db = db; }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin" && HttpContext.Session.GetString("DutyId") != "staff")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult SaleDaily()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin" && HttpContext.Session.GetString("DutyId") != "staff")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            DateTime thedate = DateTime.Today;
            DateTime endDate = thedate.AddDays(1).Date;
            var rep = from cd in _db.Books
                      join c in _db.Courses on cd.CourseId equals c.CourseId into join_c
                      from t_c in join_c
                      join r in _db.Restaurants on cd.ResId equals r.ResId into join_r
                      from t_r in join_r
                      join p in _db.Payments on cd.BookId equals p.BookId into join_p
                      from t_p in join_p.DefaultIfEmpty() // เปลี่ยนจาก join ให้เป็น left join
                      where cd.SelectDate >= thedate && cd.SelectDate <= endDate
                      group new { cd, t_r } by new { t_r.ResId, t_r.ResName, t_c.CoursePrice } into g
                      select new RepBook
                      {
                          CoursePrice = g.Key.CoursePrice,
                          ResId = g.Key.ResId,
                          ResName = g.Key.ResName,
                          TotalBookings = g.Sum(x => x.cd.CourseId) // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                                                                    // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                      };

            ViewBag.theDate = thedate.ToString("yyyy/MM/dd HH:mm:ss");
            ViewBag.TotalBookings = rep.Sum(item => item.TotalBookings);
            return View(rep);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleDaily(DateTime thedate)
        {
            if (HttpContext.Session.GetString("DutyId") != "admin" && HttpContext.Session.GetString("DutyId") != "staff")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            DateTime endDate = thedate.AddDays(1).Date;

            var rep = from cd in _db.Books
                      join c in _db.Courses on cd.CourseId equals c.CourseId into join_c
                      from t_c in join_c
                      join r in _db.Restaurants on cd.ResId equals r.ResId into join_r
                      from t_r in join_r
                      join p in _db.Payments on cd.BookId equals p.BookId into join_p
                      from t_p in join_p.DefaultIfEmpty() // เปลี่ยนจาก join ให้เป็น left join
                      where cd.SelectDate >= thedate && cd.SelectDate <= endDate
                      group new { cd, t_r } by new { t_r.ResId, t_r.ResName, t_c.CoursePrice } into g
                      select new RepBook
                      {
                          CoursePrice = g.Key.CoursePrice,
                          ResId = g.Key.ResId,
                          ResName = g.Key.ResName,
                          TotalBookings = g.Sum(x => x.cd.CourseId) // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                                                                    // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                      };
            ViewBag.theDate = thedate.ToString("yyyy/MM/dd HH:mm:ss");
            ViewBag.TotalBookings = rep.Sum(item => item.TotalBookings);
            return View(rep);

        }

        public IActionResult SaleMonthly()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin" && HttpContext.Session.GetString("DutyId") != "staff")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            //กำหนดวันแรก และคำนวณหาวันสุดท้ายของเดือนปัจจุบัน
            var theMonth = DateTime.Now.Month;
            var theYear = DateTime.Now.Year;
            //วันแรกคือวันที่ 1 ของเดือน
            DateTime sDate = new DateTime(theYear, theMonth, 1);
            //วันสุดท้ายคือวันที 1 ของเดือนหน้า ลบไป1วัน
            DateTime eDate = new DateTime(theYear, theMonth, 1).AddMonths(1).AddDays(-1);

            var rep = from cd in _db.Books
                      join c in _db.Courses on cd.CourseId equals c.CourseId into join_c
                      from t_c in join_c
                      join r in _db.Restaurants on cd.ResId equals r.ResId into join_r
                      from t_r in join_r
                      join p in _db.Payments on cd.BookId equals p.BookId into join_p
                      from t_p in join_p.DefaultIfEmpty() // เปลี่ยนจาก join ให้เป็น left join
                      where cd.SelectDate >= sDate && cd.SelectDate <= eDate
                      group new { cd, t_r } by new { t_r.ResId, t_r.ResName , t_c.CoursePrice } into g
                      select new RepBook
                      {
                          CoursePrice = g.Key.CoursePrice,
                          ResId = g.Key.ResId,
                          ResName = g.Key.ResName,
                          TotalBookings = g.Sum(x => x.cd.CourseId) // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                                                                    // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd");

            return View(rep);


        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleMonthly(DateTime sDate, DateTime eDate)
        {
            if (HttpContext.Session.GetString("DutyId") != "admin" && HttpContext.Session.GetString("DutyId") != "staff")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            var rep = from cd in _db.Books
                      join c in _db.Courses on cd.CourseId equals c.CourseId into join_c
                      from t_c in join_c
                      join r in _db.Restaurants on cd.ResId equals r.ResId into join_r
                      from t_r in join_r
                      join p in _db.Payments on cd.BookId equals p.BookId into join_p
                      from t_p in join_p.DefaultIfEmpty() // เปลี่ยนจาก join ให้เป็น left join
                      where cd.SelectDate >= sDate && cd.SelectDate <= eDate
                      group new { cd, t_r } by new { t_r.ResId, t_r.ResName, t_c.CoursePrice } into g
                      select new RepBook
                      {
                          CoursePrice = g.Key.CoursePrice,
                          ResId = g.Key.ResId,
                          ResName = g.Key.ResName,
                          TotalBookings = g.Sum(x => x.cd.CourseId) // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                                                                    // จำนวนรายการที่จองทั้งหมดสำหรับแต่ละกลุ่ม
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd HH:mm:ss");
            return View(rep);

        }


    }


}
