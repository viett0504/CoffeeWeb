using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        // Constructor để inject AccountService
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_accountService.CheckLogin(email, password))
            {
                // Giả sử lưu MaND hoặc Email vào Session ở đây
                HttpContext.Session.SetString("Email", email);
                // Đăng nhập thành công
                return RedirectToAction("Index", "Home");
            }

            // Đăng nhập thất bại
            ViewBag.Error = "Email hoặc mật khẩu không đúng.";
            return View();
        }

        // Action hiển thị trang Thông tin Tài khoản
        [HttpGet]
        public IActionResult Info()
        {
            var userId = HttpContext.Session.GetInt32("MaND");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var userInfo = _userService.GetUserInfoById(userId.Value);  // gọi đúng service
            if (userInfo == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            return View(userInfo);
        }
    }
}
