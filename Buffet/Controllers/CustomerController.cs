using Buffet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Reflection.Metadata;

namespace Buffet.Controllers
{
    public class CustomerController : Controller

    {
        private readonly BuffetContext _db;
        public CustomerController(BuffetContext db)
        {
            _db = db;
        }
        public IActionResult Show(string? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ระบุ ID";
                return RedirectToAction("Index");
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบ ID";
                return RedirectToAction("Index");
            }
            var fileName = id.ToString() + ".jpg";
            var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
            var filePath = Path.Combine(imgPath, fileName);

            if (System.IO.File.Exists(filePath))
                ViewBag.ImgFile = "/imgcus/" + id + ".jpg";
            else
                ViewBag.ImgFile = "/imgcus/login.png";
            return View(obj);
        }

        public IActionResult ImgUpload(IFormFile imgfiles, string theid)
        {
            var FileName = theid;
            var FileExtension = Path.GetExtension(imgfiles.FileName);
            var SaveFilename = FileName + FileExtension;
            var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
            var SaveFilePath = Path.Combine(SavePath, SaveFilename);
            using (FileStream fs = System.IO.File.Create(SaveFilePath))
            {
                imgfiles.CopyTo(fs);
                fs.Flush();
            }

            var obj = _db.Customers.Find(theid);
            if (obj != null)
            {
                obj.CusImg = SaveFilename;
                _db.SaveChanges();
            }
            return RedirectToAction("Show", new { id = theid });

        }

        public IActionResult ImgDelete(string id)
        {
            var filename = id.ToString() + ".jpg";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
            var filePath = Path.Combine(imagePath, filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var obj = _db.Customers.Find(id);
            if (obj != null)
            {
                obj.CusImg = null;
                _db.SaveChanges();
            }

            return RedirectToAction("Show", new { id = id });
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ระบุ ID";
                return RedirectToAction("Index");
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "ไม่พบ ID";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("CusName", obj.CusName);
                    _db.Customers.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Show", new { id = obj.CusId});
                }
                else
                {
                    ViewData["err"] = "ฐานข้อมุลไม่พร้อมทำงาน";
                    return View(obj);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
        }
    }
}
