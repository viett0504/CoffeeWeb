using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Product  {
    [Key]
    public int Id {get; set; }
    public required string? Name{get; set;}
    public string? ImgUrl{get; set;}
    public string? Descripstion{get; set;}
    public float Price{get; set;}
    public int Quantity{get; set;}

}