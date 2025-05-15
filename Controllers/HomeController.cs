using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Services;

namespace MyWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HomeService _homeService;
    private readonly RevenueService _revenueService;
    public HomeController(ILogger<HomeController> logger, HomeService homeService, RevenueService revenueService)
    {
        _logger = logger;
        _homeService = homeService;
        _revenueService = revenueService;
    }

    public IActionResult Index()
    {
        int totalProducts = _homeService.GetTotalProducts(); 
        ViewBag.TotalProducts = totalProducts; 
        int totalOrders = _homeService.GetTotalOrders();
        ViewBag.TotalOrders = totalOrders; 
        int totalCustomer = _homeService.GetTotalCustomer();
        ViewBag.TotalCustomer= totalCustomer; 
        decimal totalRevenue = _homeService.GetTotalRevenue();
        ViewBag.totalRevenue= totalRevenue; 

        var monthlyRevenue = _revenueService.GetMonthlyRevenue();
        ViewBag.MonthlyLabels = monthlyRevenue.Select(r => $"Tháng {r.Thang}").ToList();
        ViewBag.MonthlyValues = monthlyRevenue.Select(r => r.TongDoanhThu).ToList();
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
