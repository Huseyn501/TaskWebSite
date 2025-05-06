using Microsoft.AspNetCore.Mvc;

namespace PreSchool.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
