using Microsoft.AspNetCore.Mvc;

namespace WebAdminTemplate.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/login")]
    [Route("admin")]
    public class LoginController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
