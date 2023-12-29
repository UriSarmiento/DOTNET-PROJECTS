using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using COMMERCE_WEB_APP_RAZOR.Data;
using COMMERCE_WEB_APP_RAZOR.Models;

namespace COMMERCE_WEB_APP_RAZOR.Pages.Categories
{
	[BindProperties]
    public class DeleteModel : PageModel
    {

		private readonly ApplicationDbContext _db;
		//[BindProperty] //Thi is to bind user input to the Category object, so it can be used to add it to DB (Individual properties)
		public Category Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
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

		public IActionResult OnPost() // The ? means that its nullable
		{
			string? TempName;

			Category? obj = _db.Categories.Find(Category.CategoryId);
			TempName = obj.Name;

			if (obj == null)
			{
				return NotFound();
			}
			else
			{

				_db.Categories.Remove(obj);
				_db.SaveChanges();
				TempData["success"] = "Category " + TempName + " deleted successfully";
				return RedirectToPage("Index");

			}
		}

	}
}
