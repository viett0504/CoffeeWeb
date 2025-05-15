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
        public IActionResult Create(Product product, IFormFile HinhAnh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = null;

            // Nếu có ảnh được upload
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", "sanpham");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnh.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnh.CopyTo(stream);
                }

                imagePath = "/Img/sanpham/" + fileName;
            }

            if (imagePath != null)
            {
                _productService.CreateProductWithImage(product, imagePath);
            }
            else
            {
                _productService.CreateProduct(product);
            }

            return RedirectToAction(nameof(Product));
        }

        [HttpPost("Update")]
        public IActionResult Update(Product product, string MoTaChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return View(product);  
            }

            _productService.UpdateProduct(product, MoTaChiTiet);
            
            // Redirect về trang chi tiết sau khi cập nhật thành công
            return RedirectToAction("Details", new { id = product.MaSP });
        }
    }