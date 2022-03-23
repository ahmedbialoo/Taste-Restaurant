using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {

        void Update(OrderHeader orderHeader);
    }
}
