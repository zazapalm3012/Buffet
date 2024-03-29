using Buffet.ViewModels;
using Buffet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Buffet.Controllers
{
    public class RestaurantController : Controller
    {
        public readonly BuffetContext _db;
        public RestaurantController(BuffetContext db) { _db = db; }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }

            //เขียน LINQ อ่านค่าจาก Table Products ผ่าน DBContext
            //ค่าที่ได้โยนใส่ Obj pd
            //var pd = from p in _db.Products
            //         select p;
            var pdvm = from i in _db.Restaurants


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


                       };

            //ถ้าค่าที่อ่านได้เป็น Null ก็ Return เรียก Function NorFound()
            if (pdvm == null) return NotFound();

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            //ถ้าพบ ส่ง Obj pd ที่ได้ให้ View ไปแสดง
            return View(pdvm);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            ViewData["Type"] = new SelectList(_db.RestaurantsTypes, "TypeId", "TypeName");
            ViewData["Course"] = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View();
        }

        public IActionResult CreateCourse()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            /*ViewData["Type"] = new SelectList(_db.RestaurantsTypes, "TypeId", "TypeName");
            ViewData["Course"] = new SelectList(_db.Courses, "CourseId", "CourseName");*/
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Restaurant obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ResIdCount = (from res in _db.Restaurants select res).Count();
                    var ResId = ResIdCount + 1;
                    string ResIds = ResId.ToString();
                    obj.ResId = ResIds;
                    _db.Restaurants.Add(obj);
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
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CourseIdCount = (from course in _db.Courses select course).Count();
                    var CourseId = CourseIdCount + 1;
                    obj.CourseId = CourseId;
                    _db.Courses.Add(obj);
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
            return View(obj);
        }

        public IActionResult Edit(string id)
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
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
            ViewData["Type"] = new SelectList(_db.RestaurantsTypes, "TypeId", "TypeName");
            ViewData["Course"] = new SelectList(_db.Courses, "CourseId", "CourseName");

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Restaurant obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Restaurants.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "ฐานข้อมูลไม่พร้อมทำงาน";
                    return View(obj);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
         
        }

        public IActionResult Delete(string id)
        {

            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                TempData["ErrorMessage"] = "ระบุ ID";
                return RedirectToAction("Index");
            }
            var obj = _db.Restaurants.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบข้อมูล";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string ResId)
        {
            try
            {
                var obj = _db.Restaurants.Find(ResId);
                if (ModelState.IsValid)
                {
                    _db.Restaurants.Remove(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index","staff");
                }
                else
                {
                    ViewBag.ErrorMessage = "ฐานข้อมูลไม่พร้อมทำงาน";
                    return View(obj);
                }

            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "ลบข้อมูลไม่สำเร็จ";
                return RedirectToAction("Index", "staff");
            }
        }

    }
}
