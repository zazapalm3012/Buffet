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
            return View();
        }
        public IActionResult SaleDaily()
        {
            DateTime thedate = DateTime.Today;
            var rep = from cd in _db.Books
                      where cd.SelectDate >= thedate 
                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,

                      };
            ViewBag.theDate = thedate.ToString("yyyy-MM-dd HH:mm:ss");
            return View(rep);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleDaily(DateTime thedate)
        {
            var rep = from cd in _db.Books

                      select new RepBook
                      {
                          ResId = cd.ResId,
                          SelectDate = cd.SelectDate,

                      };
            ViewBag.theDate = thedate.ToString("yyyy-MM-dd HH:mm:ss");

            return View(rep);

        }

        /* public IActionResult SaleMonthly()
        {
            //กำหนดวันแรก และคำนวณหาวันสุดท้ายของเดือนปัจจุบัน
            var theMonth = DateTime.Now.Month;
            var theYear = DateTime.Now.Year;
            //วันแรกคือวันที่ 1 ของเดือน
            DateOnly sDate = new DateOnly(theYear, theMonth, 1);
            //วันสุดท้ายคือวันที 1 ของเดือนหน้า ลบไป1วัน
            DateOnly eDate = new DateOnly(theYear, theMonth, 1).AddMonths(1).AddDays(-1);

            var rep = from cd in _db.CartDtls

                      join p in _db.Products on cd.PdId equals p.PdId into join_cd_p
                      from cd_p in join_cd_p.DefaultIfEmpty()

                      join c in _db.Carts on cd.CartId equals c.CartId into join_cd
                      from c_cd in join_cd
                      where c_cd.CartDate >= sDate && c_cd.CartDate <= eDate
                      group cd by new { cd.PdId, cd_p.PdName } into g
                      select new RepSale
                      {
                          PdId = g.Key.PdId,
                          PdName = g.Key.PdName,
                          CdtlMoney = g.Sum(x => x.CdtlMoney),
                          CdtlQty = g.Sum(x => x.CdtlQty)
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd");

            return View(rep);


        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaleMonthly(DateOnly sDate, DateOnly eDate)
        {
            var rep = from cd in _db.CartDtls

                      join p in _db.Products on cd.PdId equals p.PdId into join_cd_p
                      from cd_p in join_cd_p.DefaultIfEmpty()

                      join c in _db.Carts on cd.CartId equals c.CartId into join_cd
                      from c_cd in join_cd
                      where c_cd.CartDate >= sDate && c_cd.CartDate <= eDate
                      group cd by new { cd.PdId, cd_p.PdName } into g
                      select new RepSale
                      {
                          PdId = g.Key.PdId,
                          PdName = g.Key.PdName,
                          CdtlMoney = g.Sum(x => x.CdtlMoney),
                          CdtlQty = g.Sum(x => x.CdtlQty)
                      };
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd");
            return View(rep);

        }*/


    }


}
