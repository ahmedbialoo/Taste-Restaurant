
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Taste.Models.ViewModels
{
    public class MenuItemVM
    {
        public MenuItem menuItem { get; set; }

        public IEnumerable<SelectListItem>? categoryList { get; set; }

        public IEnumerable<SelectListItem>? foodTypeList { get; set; }
    }
}
