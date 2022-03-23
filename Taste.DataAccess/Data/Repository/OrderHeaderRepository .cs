using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public void Update(OrderHeader orderHeader)
        {
            var objFromDb = _context.OrderHeaders.SingleOrDefault(f => f.Id == orderHeader.Id);

            _context.Update(orderHeader);

            _context.SaveChanges();
        }
    }
}
