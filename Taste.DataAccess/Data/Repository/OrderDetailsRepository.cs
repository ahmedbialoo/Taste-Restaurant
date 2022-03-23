using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public void Update(OrderDetails orderDetails)
        {
            var objFromDb = _context.OrderDetails.SingleOrDefault(f => f.Id == orderDetails.Id);

            _context.Update(orderDetails);

            _context.SaveChanges();
        }
    }
}
