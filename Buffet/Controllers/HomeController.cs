using Buffet.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
                return RedirectToAction("Index");
            }

            string CusId;
            string CusName;

            foreach (var item in cus)
            {
                CusId = item.CusId.ToString();
                CusName = item.CusName;

                HttpContext.Session.SetString("CusId", CusId);
                HttpContext.Session.SetString("CusName", CusName);

              // var theRecord = _db.Customers.Find(CusId);
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

        public IActionResult SignUp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
