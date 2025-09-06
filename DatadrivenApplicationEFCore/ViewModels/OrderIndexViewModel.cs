using DatadrivenApplicationEFCore.Models;

namespace DatadrivenApplicationEFCore.ViewModels
{
    public class OrderIndexViewModel
    {
        public IEnumerable<Order>? Orders { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
        public IEnumerable<Cake>? Cakes { get; set; }
        public int? SelectedOrderId { get; set; }
        public int? SelectedOrderIdDetailId { get; set; }
    }
}
