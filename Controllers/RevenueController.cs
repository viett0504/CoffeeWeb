using Microsoft.AspNetCore.Mvc;
using MyWebApp.Services;

namespace MyWebApp.Controllers
{
    [Route("ThongKe")]
    public class RevenueController : Controller
    {
        private readonly RevenueService _revenueService;

        public RevenueController(RevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        public IActionResult Revenue()
        {
            var data = _revenueService.GetRevenues();
            return View(data);
        }
    }
}
