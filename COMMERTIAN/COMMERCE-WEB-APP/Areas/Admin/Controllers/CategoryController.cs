using Microsoft.AspNetCore.Mvc;
using ECOMM.Models;
using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;

namespace COMMERCE_WEB_APP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //We are implementing the Repository to take the dbContext from there
        //  private readonly ApplicationDbContext _db; // This variable is so we can use the ApplicationDbContext retrieved in the constructor
        // ctor + tab creates the empty template for a constructor method

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        /*
        public CategoryController(ApplicationDbContext db) // We always use ApplicationDbContext when we work with data
        {                           // in Program.cs we already defined that ApplicationDbContext is configured to use our database
            _db = db;               // so basically this is to extract data from it, and it saves us the effort of creating an object of AppDBContext each time
        }
        */
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _db.Categories.ToList(); // We display the data in te Categories table as a list in our view
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() // Opens the Category creation window
        {
            return View();
        }
        [HttpPost] // Ths is so our controller knows this method has to be executed once something is posted
        public IActionResult Create(Category obj) // Receives the input data in the Create views form
        {
           /* if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot be the same as the Name."); // Adding a custom validation
            }*/
			if (_unitOfWork.Category.Get(u => u.DisplayOrder == obj.DisplayOrder && u.CategoryId != obj.CategoryId) is not null)
			{
				ModelState.AddModelError("DisplayOrder", "There cannot be repeated display orders."); 
			}


			if (ModelState.IsValid) // With ths statement we tell the controller to check the model for validations
            {
                //  _db.Categories.Add(obj); // Here we're telling the controller that it needs to add the category to the table, but not saving yet
                //  _db.SaveChanges(); //Will save all the changes made, like doing a commit in SQL
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category " + obj.Name + " created successfully"; //Temp data only stays for one request, if you refresh the message goes away                
                return RedirectToAction("Index", "Category"); // We are being explicit redirecting to Index action from the Category Controller, but if we are on the same controller you can just write the action

            }
            return View();
        }
        public IActionResult Edit(int? CategoryId) // The ? means that its nullable
        {
            if (CategoryId == null || CategoryId == 0)
            {
                return NotFound();
            }
            //Category? categoryFromDB = _db.Categories.Find(CategoryId); //Finds the category with the id received in the method
            Category? categoryFromDB = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId); //Finds the category with the id received in the method, With interface
                                                                                                  //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.CategoryId == id);
                                                                                                  //Category? categoryFromDB2 = _db.Categories.Where(u=>u.CategoryId == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj) // Receives the input data in the Create views form
        {
            if (_unitOfWork.Category.Get(u => u.DisplayOrder == obj.DisplayOrder && u.CategoryId != obj.CategoryId) is not null)
            {
                ModelState.AddModelError("DisplayOrder", "There cannot be repeated display orders."); // Adding a custom validation
            }
            if (ModelState.IsValid) // With ths statement we tell the controller to check the model for validations
            {
                //_db.Categories.Update(obj); // Here we're telling the controller that it needs to add the category to the table, but not saving yet
                //_db.SaveChanges(); //Will save all the changes made, like doing a commit in SQL

                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Category " + obj.Name + " updated successfully";
                return RedirectToAction("Index", "Category"); // We are being explicit redirecting to Index action from the Category Controller, but if we are on the same controller you can just write the action

            }
            return View();
        }
        public IActionResult Delete(int? CategoryId) // The ? means that its nullable
        {
            if (CategoryId == null || CategoryId == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId); //Finds the category with the id received in the method

            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? CategoryId) // Receives the input data in the Create views form
        {
            string? TempName;

            // Category? obj = _db.Categories.Find(CategoryId); // Original way to find registers
            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
            TempName = obj.Name;

            if (obj == null)
            {
                return NotFound();
            }
            else
            {

                //_db.Categories.Remove(obj);
                //_db.SaveChanges();
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category " + TempName + " deleted successfully";
                return RedirectToAction("Index", "Category");

            }

        }
    }
}
