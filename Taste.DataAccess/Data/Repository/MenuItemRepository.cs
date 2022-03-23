using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetMenuItemsForSelectList()
        {
            return _context.MenueItems.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDb = _context.MenueItems.SingleOrDefault(f => f.Id == menuItem.Id);

            objFromDb.Name = menuItem.Name;
            objFromDb.Description = menuItem.Description;
            objFromDb.Price = menuItem.Price;
            objFromDb.FoodTypeId = menuItem.FoodTypeId;
            objFromDb.CategoryId = menuItem.CategoryId;
            objFromDb.Image = menuItem.Image;

            _context.SaveChanges();
        }
    }
}
