using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class DetailsOrder  {
    [Key]
     public int MaCTDH { get; set; }
    public string? MaDH { get; set; }
    public int MaSP { get; set; }
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
}