using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoriesForSelectList();

        void Update(Category category);
    }
}
