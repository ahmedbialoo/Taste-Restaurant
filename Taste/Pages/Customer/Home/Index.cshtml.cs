using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

namespace Taste.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuItem> menuItems { get; set; }
        public IEnumerable<Category> categories { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ShoppingCartCount = unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == userId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, ShoppingCartCount);

            menuItems = unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            categories = unitOfWork.Category.GetAll(null, c => c.OrderBy(o => o.DisplayOrder), null);

        }
    }
}
