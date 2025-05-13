using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Product  {
    [Key]
    public int MaSP { get; set; }
    public string? TenSP { get; set; }
    public string? MoTa { get; set; }
    public decimal Gia { get; set; }
    public int SoLuongTon { get; set; }
    public string? LoaiSP { get; set; }

}