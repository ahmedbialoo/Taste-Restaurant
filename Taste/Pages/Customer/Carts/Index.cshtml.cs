using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Customer.Carts
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public OrderDetailsCart OrderDetailsCartVM { get; set; }

        public void OnGet()
        {
            OrderDetailsCartVM = new OrderDetailsCart()
            {
                OrderHeader = new Models.OrderHeader()
            };

            OrderDetailsCartVM.OrderHeader.OrderTotal = 0;

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier);


            IEnumerable<ShoppingCart> carts = _unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == UserId.Value);

            if (carts != null)
                OrderDetailsCartVM.CartList = carts.ToList();

            foreach (var cartList in OrderDetailsCartVM.CartList)
            {
                cartList.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(n => n.Id == cartList.MenuItemId);
                OrderDetailsCartVM.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
            }
        }


        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(n => n.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.save();

            return RedirectToPage("/Customer/Carts/Index");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(n => n.Id == cartId);
            if (cart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.save();

                var count = _unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCart, count);

            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                _unitOfWork.save();
            }
            return RedirectToPage("/Customer/Carts/Index");

        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(n => n.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.save();

            var count = _unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, count);


            return RedirectToPage("/Customer/Carts/Index");
        }
    }
}
