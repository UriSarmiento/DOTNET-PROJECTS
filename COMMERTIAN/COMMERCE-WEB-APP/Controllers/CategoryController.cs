using COMMERCE_WEB_APP.Data;
using Microsoft.AspNetCore.Mvc;
using COMMERCE_WEB_APP.Models;

namespace COMMERCE_WEB_APP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db; // This variable is so we can use the ApplicationDbContext retrieved in the constructor
        // ctor + tab creates the empty template for a constructor method
        public CategoryController(ApplicationDbContext db) // We always use ApplicationDbContext when we work with data
        {                           // in Program.cs we already defined that ApplicationDbContext is configured to use our database
            _db = db;               // so basically this is to extract data from it, and it saves us the effort of creating an object of AppDBContext each time
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); // We display the data in te Categories table as a list in our view
            return View(objCategoryList);
        }
    }
}
