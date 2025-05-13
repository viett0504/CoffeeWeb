using System.Data;
using MySql.Data.MySqlClient;
using MyWebApp.Models;
public class OrderService
{
    private readonly MssqlConnection _connection;

    public OrderService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQLConnection");
        _connection = new MssqlConnection(configuration);
    }

    public List<Order> GetAllOrders()
    {
        var ds = new List<Order>();
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM DonHang", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            ds.Add(new Order
            {
                MaDH = reader.IsDBNull(0) ? "" : reader.GetString(0),
                MaKH = reader.GetInt32(1),
                NgayDat = reader.GetDateTime(2),
                TongTien = reader.GetDecimal(3),
                TrangThai = reader.GetString(4),
            });
        }
        return ds;
    }
    public Order GetOrderById(string id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM DonHang WHERE MaDH = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Order
            {
                MaDH = reader.GetString(0),
                MaKH = reader.GetInt32(1),
                NgayDat = reader.GetDateTime(2),
                TongTien = reader.GetDecimal(3),
                TrangThai = reader.GetString(4),
            };
        }
        return null;
    }

    public void DeleteOrder(string id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("DELETE FROM DonHang WHERE MaDH = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<DetailsOrder> GetOrderDetails(string maDH)
    {
        var list = new List<DetailsOrder>();
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM ChiTietDonHang WHERE MaDH = @maDH", conn);
        cmd.Parameters.AddWithValue("@maDH", maDH);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new DetailsOrder
            {
                MaCTDH = reader.GetInt32(0),
                MaDH = reader.GetString(1),
                MaSP = reader.GetInt32(2),
                SoLuong = reader.GetInt32(3),
                DonGia = reader.GetDecimal(4),
            });
        }
        return list;
    }
    public void CreateOrder(Order order)
    {
        using var conn = _connection.GetConnection();
        conn.Open();

        // Lấy mã đơn hàng mới
        string newMaDH = GenerateNewMaDH(conn);

        var cmd = new MySqlCommand(@"
            INSERT INTO DonHang (MaDH, MaKH, NgayDat, TongTien, TrangThai)
            VALUES (@MaDH, @MaKH, @NgayDat, @TongTien, @TrangThai)", conn);

        cmd.Parameters.AddWithValue("@MaDH", newMaDH);
        cmd.Parameters.AddWithValue("@MaKH", order.MaKH);
        cmd.Parameters.AddWithValue("@NgayDat", order.NgayDat);
        cmd.Parameters.AddWithValue("@TongTien", order.TongTien);
        cmd.Parameters.AddWithValue("@TrangThai", order.TrangThai ?? "");
        cmd.ExecuteNonQuery();
    }


    private string GenerateNewMaDH(MySqlConnection conn)
    {
        var cmd = new MySqlCommand("SELECT MaDH FROM DonHang ORDER BY MaDH DESC LIMIT 1", conn);
        var result = cmd.ExecuteScalar()?.ToString();
        if (result != null && result.StartsWith("D"))
        {
            int number = int.Parse(result.Substring(1));
            return "D" + (number + 1);
        }
        return "D1"; // Nếu chưa có đơn nào
    }

    public List<Customer> GetAllCustomers()
    {
        var list = new List<Customer>();
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT MaKH, TenKH FROM KhachHang", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Customer
            {
                MaKH = reader.GetInt32(0),
                TenKH = reader.GetString(1)
            });
        }
        return list;
    }

}
