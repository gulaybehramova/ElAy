using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack103.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
