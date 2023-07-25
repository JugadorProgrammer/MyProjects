using Microsoft.AspNetCore.Mvc;
using ModelViewController.DataBase;
using ModelViewController.Models;
using System.Diagnostics;

namespace ModelViewController.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext Context { get; private set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            Context = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateStudent()
        {
            return View();
        }
        public IActionResult Delivery()
        {
            return View(Context.Regions.ToList());
        }
		public IActionResult CitiesList(int Id)
		{
			return PartialView(Context.Cities.ToList().Where(c=>c.RegionId == Id));
		}
		public IActionResult Service()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}