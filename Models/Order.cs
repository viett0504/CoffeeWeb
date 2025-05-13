using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Order  {
    public string? MaDH {get; set; }
    public int MaKH {get; set; }
    public DateTime NgayDat {get; set; }
    public decimal TongTien {get; set; }
    public string? TrangThai {get; set; }
    
}