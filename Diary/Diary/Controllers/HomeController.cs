using Diary.DataBase;
using Diary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using noTanish.DataBase;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Diary.Controllers
{
    public class HomeController : Controller
    {
        private DataViewModel _viewModel;
        private readonly string _dateCookieName;
        private ApplicationContext _applicationContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _dateCookieName = "SelectionDate";
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Data(DateTime date)
        {
            
            var options = new CookieOptions()
            { 
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddYears(1)
            };

            HttpContext.Response.Cookies.Append(_dateCookieName, date.ToString("yyyy-MM-dd"), options);

            _viewModel = new DataViewModel()
            {
                DayInMonth = DateTime.DaysInMonth(date.Year, date.Month),
                FirstDayOfMonth = new DateTime(date.Year, date.Month, 1),
                Days = _applicationContext.Day.ToList()
                    .Where(day => day.Date.Month == date.Month && day.Date.Year == date.Year).OrderBy(d => d.Date).ToList()
            };
            return PartialView(_viewModel);
        }

        [HttpPut]
        public IActionResult UpdateData(int Id,DateTime dateTime, string value = "")
        {
            Day day;
            if (Id == 0)
            {
                day = new Day()
                {
                    Value = value,
                    Date = dateTime
                };
                _applicationContext.Day.Add(day);
            }
            else
            {
                day = _applicationContext.Day.ToList().FirstOrDefault(d => d.Id == Id);
                day.Value = value;
                _applicationContext.Day.Update(day);
            }

            _applicationContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult Privacy()
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