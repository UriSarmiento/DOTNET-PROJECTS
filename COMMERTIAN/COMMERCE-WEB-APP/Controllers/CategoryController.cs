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

        public IActionResult Create() // Opens the Category creation window
        {
            return View();
        }
        [HttpPost] // Ths is so our controller knows this method has to be executed once something is posted
		public IActionResult Create(Category obj) // Receives the input data in the Create views form
		{
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The Display Order cannot be the same as the Name."); // Adding a custom validation
            }
            if (ModelState.IsValid) // With ths statement we tell the controller to check the model for validations
            {
                _db.Categories.Add(obj); // Here we're telling the controller that it needs to add the category to the table, but not saving yet
                _db.SaveChanges(); //Will save all the changes made, like doing a commit in SQL
				return RedirectToAction("Index", "Category"); // We are being explicit redirecting to Index action from the Category Controller, but if we are on the same controller you can just write the action

			}
            return View();
		}
	}
}
