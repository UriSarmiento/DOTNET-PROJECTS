using COMMERCE_WEB_APP_RAZOR.Data;
using COMMERCE_WEB_APP_RAZOR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace COMMERCE_WEB_APP_RAZOR.Pages.Categories
{
	[BindProperties]
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		//[BindProperty] //Thi is to bind user input to the Category object, so it can be used to add it to DB (Individual properties)
		public Category Category { get; set; }
		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet(int? CategoryId)
        {
			if (CategoryId is not null && CategoryId != 0)
			{
				Category = _db.Categories.Find(CategoryId);
			}
        }

		public IActionResult OnPost() //Post handlers are created like this for razor pages
		{
			if (ModelState.IsValid) // With ths statement we tell the controller to check the model for validations
			{
				_db.Categories.Update(Category); // Here we're telling the controller that it needs to add the category to the table, but not saving yet
				_db.SaveChanges(); //Will save all the changes made, like doing a commit in SQL
				TempData["success"] = "Category " + Category.Name + " updated successfully";
				return RedirectToPage("Index"); // We are being explicit redirecting to Index action from the Category Controller, but if we are on the same controller you can just write the action

			}
			return Page();
		}
	}
}
