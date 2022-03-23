using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Admin.MenuItem
{
    [Authorize(Roles = SD.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public MenuItemVM menuItemVMObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            menuItemVMObj = new MenuItemVM()
            {
                menuItem = new Models.MenuItem(),
                categoryList = _unitOfWork.Category.GetCategoriesForSelectList(),
                foodTypeList = _unitOfWork.FoodType.GetFoodTypesForSelectList()
            };

            if (id != null)
            {
                menuItemVMObj.menuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == id);
                if (menuItemVMObj == null)
                    return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (menuItemVMObj == null)
                return BadRequest();

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                menuItemVMObj = new MenuItemVM()
                {
                    menuItem = new Models.MenuItem(),
                    categoryList = _unitOfWork.Category.GetCategoriesForSelectList(),
                    foodTypeList = _unitOfWork.FoodType.GetFoodTypesForSelectList()
                };
                return Page();
            }

            if (menuItemVMObj.menuItem.Id == 0)
            {
                var fileName = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
                var uploads = Path.Combine(webRootPath, @"images\menuitems");

                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                menuItemVMObj.menuItem.Image = @"\images\menuitems\" + fileName + extension;
                _unitOfWork.MenuItem.Add(menuItemVMObj.menuItem);
            }
            else
            {
                var objFromDb = _unitOfWork.MenuItem.Get(menuItemVMObj.menuItem.Id);
                if (files.Count > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension = Path.GetExtension(files[0].FileName);
                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    menuItemVMObj.menuItem.Image = @"\images\menuitems\" + fileName + extension;
                }
                else
                {
                    menuItemVMObj.menuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(menuItemVMObj.menuItem);
            }

            _unitOfWork.save();
            return RedirectToPage("./Index");
        }
    }
}