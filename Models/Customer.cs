using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Customer  {
    [Key]
    public int MaKH { get; set; }
    public string? TenKH { get; set; }
    public string? Email { get; set; }
    public string? SoDienThoai { get; set; }
    public string? DiaChi { get; set; }
    public string? HinhAnhKH { get; set; }
}