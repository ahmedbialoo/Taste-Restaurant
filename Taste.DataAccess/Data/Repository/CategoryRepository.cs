using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<SelectListItem> GetCategoriesForSelectList()
        {
            return db.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objFromDb = db.Categories.FirstOrDefault(c => c.Id == category.Id);

            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;

            db.SaveChanges();
        }
    }
}