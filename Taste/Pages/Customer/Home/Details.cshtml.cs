using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }

        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart()
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(n => n.Id == id, "Category,FoodType"),
                MenuItemId = id,
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                var objFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(n => n.ApplicationUserId == ShoppingCartObj.ApplicationUserId
                                                                            && n.MenuItemId == ShoppingCartObj.MenuItemId);
                if (objFromDb == null)
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                else
                    _unitOfWork.ShoppingCart.IncrementCount(objFromDb, ShoppingCartObj.Count);

                _unitOfWork.save();

                return RedirectToPage("Index");
            }
            else
            {

                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(n => n.Id == ShoppingCartObj.MenuItemId, "Category,FoodType");
                return Page();
            }
        }
    }
}
