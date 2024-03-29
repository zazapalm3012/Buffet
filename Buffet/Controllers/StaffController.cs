using Microsoft.AspNetCore.Mvc;

namespace Buffet.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
