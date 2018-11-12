using HomeZone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeZone.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
