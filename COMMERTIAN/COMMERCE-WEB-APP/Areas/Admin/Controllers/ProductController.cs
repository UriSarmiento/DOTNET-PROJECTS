using Microsoft.AspNetCore.Mvc;
using ECOMM.Models;
using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECOMM.Models.ViewModels;

namespace COMMERCE_WEB_APP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        /*
   
        */
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create() 
        {

			// Projection in EF Core, can be used to get specific parameters from a model
            /*
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select
				(
					u => new SelectListItem
					{
						Text = u.Name,
						Value = u.CategoryId.ToString()
					}
				);//Getting all categories, their Id and name*/
            //ViewBag.CategoryList = CategoryList; //Transfer data to view
            //ViewData["CategoryList"] = CategoryList; // Asignando un valor a viewdata
            ProductVM productVM= new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select
				(
					u => new SelectListItem
					{
						Text = u.Name,
						Value = u.CategoryId.ToString()
					}
				),
			Product = new Product()
            };
			return View(productVM);
        }
        [HttpPost] 
        public IActionResult Create(ProductVM productVM) // We pass the view model
        {
			if (_unitOfWork.Product.Get(u => u.ISBN == productVM.Product.ISBN && u.ProductId != productVM.Product.ProductId) is not null) 
			{
				ModelState.AddModelError("ISBN", "There cannot be repeated ISBN codes.");
			}
			if (ModelState.IsValid) 
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product " + productVM.Product.Title + " created successfully"; 
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Edit(int? ProductId) 
        {
            if (ProductId == null || ProductId == 0)
            {
                return NotFound();
            }
          
            Product? ProductFromDB = _unitOfWork.Product.Get(u => u.ProductId == ProductId);

            if (ProductFromDB == null)
            {
                return NotFound();
            }
            return View(ProductFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (_unitOfWork.Product.Get(u => u.ISBN == obj.ISBN && u.ProductId != obj.ProductId) is not null)
            {
                ModelState.AddModelError("ISBN", "There cannot be repeated ISBN codes."); 
            }
            if (ModelState.IsValid) 
            {

                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product " + obj.Title + " updated successfully";
                return RedirectToAction("Index", "Product"); 
            }
            return View();
        }
        public IActionResult Delete(int? ProductId) 
        {
            if (ProductId == null || ProductId == 0)
            {
                return NotFound();
            }
            Product? ProductFromDB = _unitOfWork.Product.Get(u => u.ProductId == ProductId); 
            if (ProductFromDB == null)
            {
                return NotFound();
            }
            return View(ProductFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? ProductId) 
        {
            string? TempName;


            Product? obj = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
            TempName = obj.Title;

            if (obj == null)
            {
                return NotFound();
            }
            else
            {

         
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product " + TempName + " deleted successfully";
                return RedirectToAction("Index", "Product");

            }

        }
    }
}
