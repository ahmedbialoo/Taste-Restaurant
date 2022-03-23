using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Taste.Pages.Customer.Carts
{
    public class OrderConfirmationModel : PageModel
    {
        [BindProperty]
        public int OrderId { get; set; }

        public void OnGet(int id)
        {
            OrderId = id;
        }
    }
}
