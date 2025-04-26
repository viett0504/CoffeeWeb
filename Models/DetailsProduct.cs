using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class DetailsProduct  {
    [Key]
     public int MaCTSP { get; set; }
    public string MoTaChiTiet { get; set; }
    public string HinhAnh { get; set; }
    public decimal MaSP { get; set; }
}