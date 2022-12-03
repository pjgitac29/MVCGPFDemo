using Microsoft.AspNetCore.Mvc;

namespace WebAdminTemplate.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
