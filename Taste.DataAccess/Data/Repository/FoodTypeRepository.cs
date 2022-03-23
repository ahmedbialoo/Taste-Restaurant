using Microsoft.AspNetCore.Mvc.Rendering;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public FoodTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetFoodTypesForSelectList()
        {
            return _context.FoodTypes.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
        }

        public void Update(FoodType foodType)
        {
            var objFromDb = _context.FoodTypes.SingleOrDefault(f => f.Id == foodType.Id);

            objFromDb.Name = foodType.Name;
            objFromDb.Id = foodType.Id;

            _context.SaveChanges();
        }
    }
}
