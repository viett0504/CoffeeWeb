using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers;

[Route("KhachHang")]
public class CustomerController : Controller
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult Customer(int? id)
    {
        var customers = _customerService.GetAllCustomers();
        Customer selectedCustomer = null;

        if (id.HasValue)
        {
            selectedCustomer = _customerService.GetCustomerById(id.Value);
        }

        ViewBag.SelectedCustomer = selectedCustomer;
        return View(customers);
    }

    [HttpGet("Delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var customer = _customerService.GetCustomerById(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    [HttpPost("Delete/{id:int}"), ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _customerService.DeleteCustomer(id);
        return RedirectToAction(nameof(Customer));
    }

    [HttpPost("Create")]
    public IActionResult Create(Customer customer, IFormFile HinhAnhKH)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string? imagePath = null;

        // Nếu có ảnh được upload
        if (HinhAnhKH != null && HinhAnhKH.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", "khachhang");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnhKH.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                HinhAnhKH.CopyTo(stream);
            }

            imagePath = "/Img/khachhang/" + fileName;
        }

        if (imagePath != null)
        {
            customer.HinhAnhKH = imagePath;
        }
        _customerService.CreateCustomer(customer, imagePath);
        return RedirectToAction(nameof(Customer));
    }

}
