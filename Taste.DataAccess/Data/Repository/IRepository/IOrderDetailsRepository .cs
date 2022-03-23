using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {

        void Update(OrderDetails orderDetails);
    }
}
