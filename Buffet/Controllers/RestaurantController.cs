using Microsoft.AspNetCore.Mvc;

namespace Buffet.Controllers
{
	public class RestaurantController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
