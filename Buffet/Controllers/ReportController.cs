using Buffet.Controllers;
using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KuShop.Controllers
{
    public class ReportController : Controller
    {
        readonly BuffetContext _db;
        public ReportController(BuffetContext db) { _db = db; }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult SaleDaily()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            DateTime thedate = DateTime.Today;
            DateTime endDate = thedate.AddDays(1).Date;
            var rep = from cd in _db.Books
                      where cd.SelectDate >= thedate && cd.SelectDate <= endDate
                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,

                      };
            ViewBag.theDate = thedate.ToString("yyyy/MM/dd HH:mm:ss");
            return View(rep);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleDaily(DateTime thedate)
        {
            if (HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            DateTime endDate = thedate.AddDays(1).Date;

            var rep = from cd in _db.Books
                      where cd.SelectDate >= thedate && cd.SelectDate <= endDate

                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,

                      };
            ViewBag.theDate = thedate.ToString("yyyy/MM/dd HH:mm:ss");

            return View(rep);

        }

        public IActionResult SaleMonthly()
        {
            if (HttpContext.Session.GetString("DutyId") != "admin")
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

                      where cd.SelectDate >= sDate && cd.SelectDate <= eDate

                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,
                          /*CdtlMoney = g.Sum(x => x.CdtlMoney),
                          CdtlQty = g.Sum(x => x.CdtlQty)*/
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd HH:mm:ss");

            return View(rep);


        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleMonthly(DateTime sDate, DateTime eDate)
        {
            if (HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            var rep = from cd in _db.Books

                      where cd.SelectDate >= sDate && cd.SelectDate <= eDate

                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,
                          /*CdtlMoney = g.Sum(x => x.CdtlMoney),
                          CdtlQty = g.Sum(x => x.CdtlQty)*/
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd HH:mm:ss");
            return View(rep);

        }


    }


}
