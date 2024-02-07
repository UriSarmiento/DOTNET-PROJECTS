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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

		}


        /*
   
        */
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? ProductId) //update and insert 
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
            ProductVM productVM = new()
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
            if (ProductId == null || ProductId == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u=>u.ProductId== ProductId);
				return View(productVM);
			}
			
        }
        [HttpPost] 
        public IActionResult Upsert(ProductVM productVM, IFormFile? file) // We pass the view model
        {
			if (_unitOfWork.Product.Get(u => u.ISBN == productVM.Product.ISBN && u.ProductId != productVM.Product.ProductId) is not null) 
			{
				ModelState.AddModelError("ISBN", "There cannot be repeated ISBN codes.");
			}
			if (ModelState.IsValid) 
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); //We change the filename to a random guid plus extension
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); // here the route where the file will be saved is defined

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\')); // The trim removes the first backslash in the route

                        if (System.IO.Path.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    
                    
                    using (var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create)) // Does the creation of the file in the route
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }


                if(productVM.Product.ProductId == 0) //This way we hande both insert and update
                {
					_unitOfWork.Product.Add(productVM.Product);
				}
                else
                {
					_unitOfWork.Product.Update(productVM.Product);
				}

                _unitOfWork.Save();
                TempData["success"] = "Product " + productVM.Product.Title + " created successfully"; 
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select
               (
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.CategoryId.ToString()
                   }
               );
				return View(productVM);
			}
			
            
        }
 /*      
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
 */
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int? ProductId) 
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
    public IActionResult Delete (int? ProductId)
        {

            var productToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == ProductId);

            if(productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
				Path.Combine(_webHostEnvironment.WebRootPath, 
                productToBeDeleted.ImageUrl.TrimStart('\\')); // The trim removes the first backslash in the route

			if (System.IO.Path.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            //List<Product> objsuccessProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { success = true, message = "Delete Successful" });
		}
        #endregion

    }
}
