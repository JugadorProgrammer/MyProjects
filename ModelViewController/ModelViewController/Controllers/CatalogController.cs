using Microsoft.AspNetCore.Mvc;
using ModelViewController.DataBase;
using ModelViewController.Models;

namespace ModelViewController.Controllers
{ 
    public class CatalogController : Controller
    {
        public ApplicationContext Context { get; private set; }

        public CatalogController(ApplicationContext db)
        {
            Context = db;
        }
        public IActionResult Index()
        {
            return View(Context.Products.ToList());
        }
        public IActionResult Individual(int id)
        {
            return View(Context.Products.ToList().Where(p=>p.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult SortedProduct(bool[] cheks,int price,int selectedIndex,string name)
        {
            List<Product> list = Context.Products.ToList().Where(p => p.Price <= price).ToList(),
             products = new List<Product>();

            if (!string.IsNullOrEmpty(name))
            {
                if (name.Replace(" ", "") != string.Empty)
                {
                    return PartialView(Context.Products.ToList().Where(p => p.Name.Contains(name)).ToList());
                }
            }
            if (cheks[0])
            {
                products = products.Concat(list.Where(p=>p.ModelName == "Canon")).ToList();
            }
            if (cheks[1])
            {
                products = products.Concat(list.Where(p => p.ModelName == "Polaroid")).ToList();
            }
            if (cheks[2])
            {
                products = products.Concat(list.Where(p => p.ModelName == "FujiFilm")).ToList();
            }
            switch (selectedIndex)
            {
                case 0: products = products.OrderBy(p => p.Price).ToList(); break;
                case 1: products = products.OrderByDescending(p => p.Price).ToList(); break;
                case 2: products = products.OrderBy(p => p.Name).ToList(); break;
                case 3: products = products.OrderByDescending(p => p.Name).ToList(); break;
            }
            return PartialView(products);
        }

        [HttpPost]
        public IActionResult Characteristics(int Id)
        {
            var product = Context.Products.ToList().Where(p=>p.Id==Id).FirstOrDefault();
            return PartialView(product);
        }
        [HttpPost]
        public IActionResult AboutProduct(int Id)
        {
            var product = Context.Products.ToList().Where(p => p.Id == Id).FirstOrDefault();
            return PartialView(product);
        }
        [HttpPost]
        public IActionResult ServicesProduct()
        {
            return PartialView();
        }
    }
}
