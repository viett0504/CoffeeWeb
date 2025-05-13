using System.Data;
using MySql.Data.MySqlClient;
using MyWebApp.Models;

public class CustomerService
{
    private readonly MssqlConnection _connection;

    public CustomerService(IConfiguration configuration)
    {
        _connection = new MssqlConnection(configuration);
    }

    public List<Customer> GetAllCustomers()
    {
        var list = new List<Customer>();
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM KhachHang WHERE IsDeleted = FALSE", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Customer
            {
                MaKH = reader.GetInt32("MaKH"),
                TenKH = reader.GetString("TenKH"),
                Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                SoDienThoai = reader.IsDBNull("SoDienThoai") ? null : reader.GetString("SoDienThoai"),
                DiaChi = reader.IsDBNull("DiaChi") ? null : reader.GetString("DiaChi"),
                HinhAnhKH = reader.IsDBNull("HinhAnhKH") ? null : reader.GetString("HinhAnhKH")
            });
        }
        return list;
    }

    public Customer GetCustomerById(int id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM KhachHang WHERE MaKH = @id AND IsDeleted = FALSE", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Customer
            {
                MaKH = reader.GetInt32("MaKH"),
                TenKH = reader.GetString("TenKH"),
                Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                SoDienThoai = reader.IsDBNull("SoDienThoai") ? null : reader.GetString("SoDienThoai"),
                DiaChi = reader.IsDBNull("DiaChi") ? null : reader.GetString("DiaChi"),
                HinhAnhKH = reader.IsDBNull("HinhAnhKH") ? null : reader.GetString("HinhAnhKH")
            };
        }
        return null;
    }

    public void DeleteCustomer(int id)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("UPDATE KhachHang SET IsDeleted = TRUE WHERE MaKH = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void CreateCustomer(Customer customer)
    {
        using var conn = _connection.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand(@"INSERT INTO KhachHang (TenKH, Email, SoDienThoai, DiaChi, HinhAnhKH, IsDeleted)
                                    VALUES (@TenKH, @Email, @SoDienThoai, @DiaChi, @HinhAnhKH, FALSE)", conn);

        cmd.Parameters.AddWithValue("@TenKH", customer.TenKH ?? "");
        cmd.Parameters.AddWithValue("@Email", customer.Email ?? "");
        cmd.Parameters.AddWithValue("@SoDienThoai", customer.SoDienThoai ?? "");
        cmd.Parameters.AddWithValue("@DiaChi", customer.DiaChi ?? "");
        cmd.Parameters.AddWithValue("@HinhAnhKH", customer.HinhAnhKH ?? "");

        cmd.ExecuteNonQuery();
    }
}
