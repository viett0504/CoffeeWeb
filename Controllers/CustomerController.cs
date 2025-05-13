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
    public IActionResult Create(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _customerService.CreateCustomer(customer);
        return RedirectToAction(nameof(Customer));
    }
}
