using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Revenue  {
    public int MaTK {get; set; }
    public int Thang {get; set; }
    public int Nam {get; set; }
    public Decimal TongDoanhThu {get; set; }
}