using Microsoft.AspNetCore.Mvc;
using ModelViewController.DataBase;

namespace ModelViewController.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductController(ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _context = db;
        }
        [HttpPost]
        public async Task<IActionResult> DownloadFile()
        {
            var uploadedFile = Request.Form.Files[0];
            if (uploadedFile != null)
            {
                var last = _context.Products.ToList().LastOrDefault();
                // путь к папке 
                string path = "/images/ProductPhoto" + (last == null ? 1 : last.Id + 1).ToString() + "." +
                    uploadedFile.ContentType.Split("/")[1];//uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                PathSaver.ImagePath = path;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

            }
            return PartialView();
        }
        //public IActionResult DownloadFile(IFormFile uploadedFile)
        //{
        //    var a = Request.Files[0];
        //    if (uploadedFile != null)
        //    {
        //        var last = _context.Products.ToList().LastOrDefault();
        //        // путь к папке 
        //        string path = "/images/ProductPhoto" + (last == null ? 1: last.Id+1).ToString()+"."+
        //            uploadedFile.ContentType.Split("/")[1];//uploadedFile.FileName;
        //        // сохраняем файл в папку Files в каталоге wwwroot
        //        PathSaver.ImagePath = path;
        //        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //        {
        //            uploadedFile.CopyTo(fileStream);
        //        }

        //    }
        //    return PartialView();
        //}

        [HttpPost]
        public ActionResult OnPostProduct(Product product)
        {
            if (product.Name != null &&
                product.Characteristics != null &&
                product.ModelName != null &&
                product.Description != null)
            {
                product.Image = PathSaver.ImagePath;
                _context.Products.Add(product);
                _context.SaveChangesAsync();
                PathSaver.ImagePath = "/images/nothing.png";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index1()
        {
            return PartialView();
        }
    }
}
