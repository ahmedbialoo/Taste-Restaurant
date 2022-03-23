using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        IEnumerable<SelectListItem> GetMenuItemsForSelectList();

        void Update(MenuItem menuItem);
    }
}
