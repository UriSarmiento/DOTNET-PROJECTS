using COMMERCE_WEB_APP_RAZOR.Data;
using COMMERCE_WEB_APP_RAZOR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace COMMERCE_WEB_APP_RAZOR.Pages.Categories
{
    [BindProperties] //This bind user input to every property defined in the model
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbContext _db;
        //[BindProperty] //Thi is to bind user input to the Category object, so it can be used to add it to DB (Individual properties)
		public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }


		public void OnGet()
        {
        }

        public IActionResult OnPost() //Post handlers are created like this for razor pages
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category " + Category.Name + " created successfully";
            return RedirectToPage("Index"); //This works because it's in the same folder
        }
    }
}
