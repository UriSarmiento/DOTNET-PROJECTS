using ECOMM.DataAccess.Data;
using ECOMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMMERCE_WEB_APP.Controllers
{
	public class UsersController : Controller
	{
		private readonly ApplicationDbContext _db;
        
		public UsersController(ApplicationDbContext db)
        {
			_db = db;
		}

		public IActionResult Create()
		{
			return View();
		}
		/*
		public IActionResult Index()
		{
			return View();
		}
		*/
		[HttpPost]
		public IActionResult Create(Users obj) // Receives the input data in the Create views form
		{
			if (ModelState.IsValid) // With ths statement we tell the controller to check the model for validations
			{
				_db.Users.Add(obj); // Here we're telling the controller that it needs to add the category to the table, but not saving yet
				_db.SaveChanges(); //Will save all the changes made, like doing a commit in SQL
				return RedirectToAction("Index", "Home"); // We are being explicit redirecting to Index action from the Category Controller, but if we are on the same controller you can just write the action

			}
			return View();
		}

		public bool IsActive(Users obj) // Checks if the user is actve
		{ return obj.ActiveFlag; }
	}
}
