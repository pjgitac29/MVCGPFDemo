using Microsoft.AspNetCore.Mvc;

namespace WebAdminTemplate.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Dashboard")]
    public class DashboardController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
