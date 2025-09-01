namespace DatadrivenApplicationEFCore.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int CakeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Cake Cake { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
