
using Microsoft.EntityFrameworkCore;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatadrivenApplicationDbContext _context;

        public OrderRepository(DatadrivenApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersDetailsAsync()
        {
            return await _context.Orders.Include(o  => o.OrderDetails).ThenInclude(o => o.Cake).OrderBy(o => o.OrderId).ToListAsync();
        }

        public async Task<Order?> GetOrderDetailsAsync(int? orderId)
        {
            if(orderId != null)
            {
                var order = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Cake).OrderBy(o => o.OrderId).Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
                return order;
            }
            return null;
        }
    }
}
