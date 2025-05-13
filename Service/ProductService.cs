using System.Data;
using MySql.Data.MySqlClient;
using MyWebApp.Models;
public class ProductService
{
    private readonly MssqlConnection _connection;

    public ProductService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQLConnection");
        _connection = new MssqlConnection(configuration);
    }

    public List<Product> GetAllProducts()
    {
        var ds = new List<Product>();
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM SanPham WHERE IsDeleted = FALSE", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            ds.Add(new Product
            {
                MaSP = reader.GetInt32(0),
                TenSP = reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Gia = reader.GetDecimal(3),
                SoLuongTon = reader.GetInt32(4),
                LoaiSP = reader.GetString(5)
            });
        }
        return ds;
    }
    public Product GetProductById(int id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM SanPham WHERE MaSP = @id AND IsDeleted = FALSE", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Product
            {
                MaSP = reader.GetInt32(0),
                TenSP = reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Gia = reader.GetDecimal(3),
                SoLuongTon = reader.GetInt32(4),
                LoaiSP = reader.GetString(5)
            };
        }
        return null;
    }

    public void DeleteProduct(int id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("UPDATE SanPham SET IsDeleted = TRUE WHERE MaSP = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public DetailsProduct GetProductDetails(int productId)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM ChiTietSanPham WHERE MaSP = @productId", conn);
        cmd.Parameters.AddWithValue("@productId", productId);
        
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new DetailsProduct
            {
                MaCTSP = reader.GetInt32(0),
                MoTaChiTiet = reader.IsDBNull(1) ? null : reader.GetString(1),
                HinhAnh = reader.IsDBNull(2) ? null : reader.GetString(2),
                MaSP = reader.GetInt32(3)
            };
        }
        return null;
    }
    public void CreateProduct(Product product)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand(@"INSERT INTO SanPham (TenSP, MoTa, Gia, SoLuongTon, LoaiSP, IsDeleted)
                                    VALUES (@TenSP, @MoTa, @Gia, @SoLuongTon, @LoaiSP, FALSE)", conn);

        cmd.Parameters.AddWithValue("@TenSP", product.TenSP);
        cmd.Parameters.AddWithValue("@MoTa", product.MoTa ?? "");  // nếu null thì lưu ""
        cmd.Parameters.AddWithValue("@Gia", product.Gia);
        cmd.Parameters.AddWithValue("@SoLuongTon", product.SoLuongTon);
        cmd.Parameters.AddWithValue("@LoaiSP", product.LoaiSP ?? "");

        cmd.ExecuteNonQuery();
    }
    public void CreateProductWithImage(Product product, string hinhAnh)
    {
        CreateProduct(product); // Gọi tạo sản phẩm trước

        using var conn = _connection.GetConnection();
        conn.Open();

        // Lấy mã sản phẩm  vừa thêm (cách đơn giản nếu ID tăng tự động)
        var getIdCmd = new MySqlCommand("SELECT MAX(MaSP) FROM SanPham", conn);
        int maSP = Convert.ToInt32(getIdCmd.ExecuteScalar());

        var cmd = new MySqlCommand(@"INSERT INTO ChiTietSanPham (MoTaChiTiet, HinhAnh, MaSP)
                                    VALUES (@MoTaChiTiet, @HinhAnh, @MaSP)", conn);

        cmd.Parameters.AddWithValue("@MoTaChiTiet", product.MoTa ?? "");
        cmd.Parameters.AddWithValue("@HinhAnh", hinhAnh);
        cmd.Parameters.AddWithValue("@MaSP", maSP);

        cmd.ExecuteNonQuery();
    }

}
