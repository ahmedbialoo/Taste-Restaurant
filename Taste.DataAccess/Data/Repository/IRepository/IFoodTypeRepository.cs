using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypesForSelectList();

        void Update(FoodType foodType);
    }
}
