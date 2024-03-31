using Buffet.ViewModels;
using Buffet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var pdvm = from i in _db.Restaurants

                       select new Pdvm
                       {
                           ResId = i.ResId,
                           ResName = i.ResName,
                           ResPhone = i.ResPhone,
                           ResAvg = i.ResAvg,
                           ResLocation = i.ResLocation,
                           ResDtl = i.ResDtl
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
            ViewData["Set"] = new SelectList(_db.Tablesets, "TablesetIds", "Total");

            return View();


        }

        public IActionResult CreateCourse()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            //ViewData["Type"] = new SelectList(_db.RestaurantsTypes, "TypeId", "TypeName");
            ViewData["name"] = new SelectList(_db.Restaurants, "ResId", "ResName");
            return View();
        }
        public IActionResult CreateTablesets()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTablesets(Tableset obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CourseIdCount = (from course in _db.Tablesets select course).Count();
                    var CourseId = CourseIdCount + 1;
                    var Total = "S=" + obj.Ssize + " M=" + obj.Msize + " L=" + obj.Lsize;
                    obj.TablesetIds = CourseId;
                    obj.Total = Total;
                    _db.Tablesets.Add(obj);
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
            ViewData["Set"] = new SelectList(_db.Tablesets, "TablesetIds", "Total");

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
                    return RedirectToAction("Index", "staff");
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

        public IActionResult ResImgUpload(IFormFile imgfiles, string id)
        {
            var FileName = id;
            var FileExtension = Path.GetExtension(imgfiles.FileName);
            var SaveFilename = FileName + FileExtension;
            var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgres");
            var SaveFilePath = Path.Combine(SavePath, SaveFilename);
            using (FileStream fs = System.IO.File.Create(SaveFilePath))
            {
                imgfiles.CopyTo(fs);
                fs.Flush();
            }

            var obj = _db.Restaurants.Find(id);
            if (obj != null)
            {
                obj.ResImg = SaveFilename;
                _db.SaveChanges();
            }
            return RedirectToAction("Index","Staff");

        }

        public IActionResult ResImgDelete(string id)
        {
            var filename = id.ToString() + ".jpg";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgres");
            var filePath = Path.Combine(imagePath, filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var obj = _db.Restaurants.Find(id);
            if (obj != null)
            {
                obj.ResImg = null;
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Staff");

        }

        public IActionResult CreateResType()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            //ViewData["CreateType"] = new SelectList(_db.RestaurantsTypes, "TypeId", "TypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResType(RestaurantsType obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var TypeIdCount = (from course in _db.RestaurantsTypes select course).Count();
                    var TypeId = TypeIdCount + 1;
                    obj.TypeId = TypeId.ToString();
                    _db.RestaurantsTypes.Add(obj);
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

        public IActionResult CourseList()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            var pdvm = from i in _db.Courses

                       select new Pdvm
                       {
                           CourseId = i.CourseId,
                           ResId = i.ResId,
                           CourseName = i.CourseName,
                           CoursePrice = i.CoursePrice,
                           CourseType = i.CourseType,
                           CourseDtl = i.CourseDtl
                       };

            //ถ้าค่าที่อ่านได้เป็น Null ก็ Return เรียก Function NorFound()
            if (pdvm == null) return NotFound();

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            //ถ้าพบ ส่ง Obj pd ที่ได้ให้ View ไปแสดง
            return View(pdvm);
        }

        public IActionResult SetList()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            var pdvm = from i in _db.Tablesets

                       select new Pdvm
                       {
                           TablesetIds = i.TablesetIds.ToString(),
                           Ssize = i.Ssize,
                           Msize = i.Msize,
                           Lsize = i.Lsize,

                       };

            //ถ้าค่าที่อ่านได้เป็น Null ก็ Return เรียก Function NorFound()
            if (pdvm == null) return NotFound();

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            //ถ้าพบ ส่ง Obj pd ที่ได้ให้ View ไปแสดง
            return View(pdvm);
        }
        public IActionResult TypeList()
        {
            if (HttpContext.Session.GetString("DutyId") != "staff" && HttpContext.Session.GetString("DutyId") != "admin")
            {
                TempData["ErrorMessage"] = "ไม่มีสิทธิใช้งาน";
                return RedirectToAction("Index", "Home");
            }
            var pdvm = from i in _db.RestaurantsTypes

                       select new Pdvm
                       {
                           TypeId = i.TypeId,
                           TypeName = i.TypeName,
                       };

            //ถ้าค่าที่อ่านได้เป็น Null ก็ Return เรียก Function NorFound()
            if (pdvm == null) return NotFound();

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            //ถ้าพบ ส่ง Obj pd ที่ได้ให้ View ไปแสดง
            return View(pdvm);
        }

        public IActionResult DeleteCourse(int id)
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
            var obj = _db.Courses.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบข้อมูล";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePostsCourse(int CourseId)
        {
            try
            {
                var obj = _db.Courses.Find(CourseId);
                if (ModelState.IsValid)
                {
                    _db.Courses.Remove(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "staff");
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

        public IActionResult DeleteSet(int id)
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
            var obj = _db.Tablesets.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบข้อมูล";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePostsSet(int TablesetIds)
        {
            try
            {
                var obj = _db.Tablesets.Find(TablesetIds);
                if (ModelState.IsValid)
                {
                    _db.Tablesets.Remove(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "staff");
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

        public IActionResult DeleteType(string id)
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
            var obj = _db.RestaurantsTypes.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบข้อมูล";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePostsType(string TypeId)
        {
            try
            {
                var obj = _db.RestaurantsTypes.Find(TypeId);
                if (ModelState.IsValid)
                {
                    _db.RestaurantsTypes.Remove(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "staff");
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