using Buffet.Models;
using Buffet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace Buffet.Controllers
{
    public class HomeController : Controller
    {
        private readonly BuffetContext _db;

        public HomeController(BuffetContext db)
        {
            _db = db;
        }

        public IActionResult Search(string search)
        {
            var pdvm = (from p in _db.Restaurants
                        join pt in _db.Courses on p.ResId equals pt.ResId into join_p_pt
                        from p_pt in join_p_pt
                        where p.ResName.Contains(search) || p_pt.CourseName.Contains(search) || p_pt.CourseType.Contains(search)
                        select new Pdvm
                        {
                            ResId = p.ResId,
                            ResName = p.ResName,
                            ResPhone = p.ResPhone,
                            ResImg = p.ResImg,
                            ResAvg = p.ResAvg,
                            ResLocation = p.ResLocation,
                            ResDtl = p.ResDtl,
                            CourseId = p_pt.CourseId,
                            CourseName = p_pt.CourseName,
                            CoursePrice = p_pt.CoursePrice,
                            CourseDtl = p_pt.CourseDtl,
                            CourseImg = p_pt.CourseImg,
                            CourseType = p_pt.CourseType
                        }).GroupBy(x => new { x.ResId, x.CourseId,x.ResName,x.CourseType }).Select(g => g.First()); // ใช้ GroupBy เพื่อแบ่งข้อมูลออกเป็นกลุ่มและเลือกข้อมูลแต่ละกลุ่ม
            if (pdvm == null) return NotFound();
            ViewData["Search"] = search;
            return View(pdvm);
        }
        public IActionResult Index()
        {
            var shop = from i in _db.Restaurants

                       select new Pdvm
                       {
                           ResId = i.ResId,
                           ResName = i.ResName,
                           ResPhone = i.ResPhone,
                           ResImg = i.ResImg,
                           ResAvg = i.ResAvg,
                           ResLocation = i.ResLocation,
                           ResDtl = i.ResDtl
                       };
            if (shop == null) return NotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            
            return View(shop);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cusIdCount = (from id in _db.Customers select id).Count();
                    obj.CusId = "C" + 00 + cusIdCount;
                    _db.Customers.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            ViewBag.ErrorMessage = "การบันทึกผิดพลาด";
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPass)
        {
            var cus = from c in _db.Customers
                      where c.CusPhone.Equals(userName) && c.CusPass.Equals(userPass)
                      select c;

            if (cus.ToList().Count() == 0)
            {
                TempData["ErrorMessage"] = "I Show Gay";
                return RedirectToAction("Login");
            }

            string CusId;
            string CusName;

            foreach (var item in cus)
            {
                CusId = item.CusId.ToString();
                CusName = item.CusName;

                HttpContext.Session.SetString("CusId", CusId);
                HttpContext.Session.SetString("CusName", CusName);
                //var theRecord = _db.Customers.Find(CusId);
                //theRecord.LastLogin = DateOnly.FromDateTime(DateTime.Now);

                //_db.Entry(theRecord).State = EntityState.Modified;
            }

            //_db.SaveChanges();
            return RedirectToAction("index");
        }
        

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        /*
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
            var shop = from i in _db.Restaurants where i.ResId.Equals(id) 
                       select new Pdvm
                       {
                           ResId = i.ResId,
                           ResName = i.ResName,
                           
                           ResPhone = i.ResPhone,
                           ResAvg   = i.ResAvg,
                           ResLocation  = i.ResLocation
                       };
            return View(shop);
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
