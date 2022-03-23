using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Customer.Carts
{
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public SummaryModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [BindProperty]
        public OrderDetailsCart OrderDetailsCartVM { get; set; }

        public IActionResult OnGet(string userId)
        {
            OrderDetailsCartVM = new OrderDetailsCart()
            {
                OrderHeader = new OrderHeader()
            };

            OrderDetailsCartVM.OrderHeader.OrderTotal = 0;

            OrderDetailsCartVM.OrderHeader.UserId = userId;

            IEnumerable<ShoppingCart> carts = _unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == userId);

            if (carts != null)
                OrderDetailsCartVM.CartList = carts.ToList();

            foreach (var cartList in OrderDetailsCartVM.CartList)
            {
                cartList.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(n => n.Id == cartList.MenuItemId);
                OrderDetailsCartVM.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
            }
            var user = _userManager.Users.FirstOrDefault(n => n.Id == userId);

            OrderDetailsCartVM.OrderHeader.PickName = user.FullName;
            OrderDetailsCartVM.OrderHeader.PickUpTime = DateTime.Now;
            OrderDetailsCartVM.OrderHeader.PhoneNumber = user.PhoneNumber;

            return Page();
        }


        public IActionResult OnPost(string stripeToken)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                OrderDetailsCartVM.CartList = _unitOfWork.ShoppingCart.GetAll(n => n.ApplicationUserId == userId).ToList();

                OrderDetailsCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                OrderDetailsCartVM.OrderHeader.OrderDate = DateTime.Now;
                OrderDetailsCartVM.OrderHeader.UserId = userId;
                OrderDetailsCartVM.OrderHeader.Status = SD.PaymentStatusPending;
                OrderDetailsCartVM.OrderHeader.PickUpTime = Convert.ToDateTime(OrderDetailsCartVM.OrderHeader.PickUpDate.ToShortDateString() + " " + OrderDetailsCartVM.OrderHeader.PickUpTime.ToShortTimeString());
                _unitOfWork.OrderHeader.Add(OrderDetailsCartVM.OrderHeader);
                _unitOfWork.save();

                List<OrderDetails> orderDetailsList = new List<OrderDetails>();

                foreach (var item in OrderDetailsCartVM.CartList)
                {
                    item.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == item.MenuItemId);

                    OrderDetails orderDetails = new OrderDetails
                    {
                        MenuItemId = item.MenuItemId,
                        Count = item.Count,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        OrderId = OrderDetailsCartVM.OrderHeader.Id
                    };
                    OrderDetailsCartVM.OrderHeader.OrderTotal += (orderDetails.Count * orderDetails.Price);
                    _unitOfWork.OrderDetails.Add(orderDetails);
                }
                _unitOfWork.ShoppingCart.RemoveRange(OrderDetailsCartVM.CartList);
                HttpContext.Session.Remove(SD.ShoppingCart);
                _unitOfWork.save();

                if (stripeToken != null)
                {


                    var options = new ChargeCreateOptions
                    {
                        Amount = Convert.ToInt32(OrderDetailsCartVM.OrderHeader.OrderTotal * 100),
                        Currency = "usd",
                        Description = "Order ID is : " + OrderDetailsCartVM.OrderHeader.Id,
                        Source = stripeToken
                    };
                    var service = new ChargeService();
                    Charge charge = service.Create(options);

                    OrderDetailsCartVM.OrderHeader.TransactionId = charge.Id;
                    if (charge.Status.ToLower() == "succeeded")
                    {
                        //email
                        OrderDetailsCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                        OrderDetailsCartVM.OrderHeader.Status = SD.StatusSubmitted;
                    }
                    else
                    {
                        OrderDetailsCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                    }

                }
                else
                {
                    OrderDetailsCartVM.OrderHeader.Status = SD.PaymentStatusRejected;
                }
                _unitOfWork.save();
                return RedirectToPage("/Customer/Carts/OrderConfirmation", new { id = OrderDetailsCartVM.OrderHeader.Id });
            }
            else
            {
                return Page();
            }

        }

    }
}
