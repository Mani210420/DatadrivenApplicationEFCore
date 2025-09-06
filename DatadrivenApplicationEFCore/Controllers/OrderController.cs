using DatadrivenApplicationEFCore.Models;
using DatadrivenApplicationEFCore.Models.Repositories;
using DatadrivenApplicationEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatadrivenApplicationEFCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index(int? orderId, int? orderDetailId)
        {
            OrderIndexViewModel orderIndexViewModel = new()
            {
                Orders = await _orderRepository.GetAllOrdersDetailsAsync()
            };
            if (orderId != null) 
            {
                var selectedOrder = orderIndexViewModel.Orders.Where(o => o.OrderId == orderId).Single();
                orderIndexViewModel.OrderDetails = selectedOrder.OrderDetails;
                orderIndexViewModel.SelectedOrderId = orderId;
            }
            if (orderDetailId != null)
            {
                var selectedOrderDetail = orderIndexViewModel.OrderDetails.Where(o => o.OrderDetailId == orderDetailId).Single();
                orderIndexViewModel.Cakes = new List<Cake>() { selectedOrderDetail.Cake };
                orderIndexViewModel.SelectedOrderIdDetailId = orderDetailId;
            }
            return View(orderIndexViewModel);
        }
    }
}
