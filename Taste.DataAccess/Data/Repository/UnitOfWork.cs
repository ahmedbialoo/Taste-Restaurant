using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            Category = new CategoryRepository(db);
            FoodType = new FoodTypeRepository(db);
            MenuItem = new MenuItemRepository(db);
            ShoppingCart = new ShoppingCartRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
            OrderDetails = new OrderDetailsRepository(db);
        }

        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; private set; }

        public IMenuItemRepository MenuItem { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailsRepository OrderDetails { get; private set; }

        public void Dispose()
        {

            db.Dispose();
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
