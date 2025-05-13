using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    [Route("DonHang")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Order()
        {
            var list = _orderService.GetAllOrders();
            var customers = _orderService.GetAllCustomers();
            ViewBag.CustomerList = customers;
            
            return View(list);
        }

        [HttpGet("Detail/{id}")]
        public IActionResult Details(string id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();

            var details = _orderService.GetOrderDetails(id);

            return View("DetailsOrder", (order, details)); 
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost("Delete/{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Order));
        }

        [HttpPost("Create")]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Nếu order.MaDH đang NULL hoặc rỗng thì tự tạo mã mới
            if (string.IsNullOrEmpty(order.MaDH))
            {
                var existingOrders = _orderService.GetAllOrders();
                int maxNumber = 0;
                foreach (var existingOrder in existingOrders)
                {
                    if (!string.IsNullOrEmpty(existingOrder.MaDH) && existingOrder.MaDH.StartsWith("D"))
                    {
                        if (int.TryParse(existingOrder.MaDH.Substring(1), out int number))
                        {
                            if (number > maxNumber)
                            {
                                maxNumber = number;
                            }
                        }
                    }
                }

                order.MaDH = "D" + (maxNumber + 1); // Gán mã đơn mới
            }

            _orderService.CreateOrder(order);

            return RedirectToAction(nameof(Order));
        }
    }
}
