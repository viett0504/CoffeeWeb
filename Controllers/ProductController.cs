using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
namespace MyWebApp.Controllers;

[Route("SanPham")]
public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Product()
    {
        var list = _productService.GetAllProducts();
        return View(list);
    }

    [HttpGet("Detail/{id:int}")]
    public IActionResult Details(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null) return NotFound();

        var details = _productService.GetProductDetails(id);
        
        return View((product, details));
    }

    public IActionResult Delete(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost("Delete/{id:int}"), ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _productService.DeleteProduct(id);
        return RedirectToAction(nameof(Product));
    }
    [HttpPost("Create")]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _productService.CreateProduct(product);
        return RedirectToAction(nameof(Product));
    }
}