using MySql.Data.MySqlClient;
using MyWebApp.Models;
public class HomeService
{
    private readonly MssqlConnection _connection;

    public HomeService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQLConnection");
        _connection = new MssqlConnection(configuration);
    }
    public int GetTotalProducts()
    {
        int total = 0;
        using (var connection = _connection.GetConnection())
        {
            connection.Open();
            var query = "SELECT COUNT(*) FROM SanPham WHERE IsDeleted = 0";
            using (var command = new MySqlCommand(query, connection))
            {
                total = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return total;
    }
    public int GetTotalOrders()
    {
        int total = 0;
        using (var connection = _connection.GetConnection())
        {
            connection.Open();
            var query = "SELECT COUNT(*) FROM DonHang ";
            using (var command = new MySqlCommand(query, connection))
            {
                total = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return total;
    }
    public int GetTotalCustomer()
    {
        int total = 0;
        using (var connection = _connection.GetConnection())
        {
            connection.Open();
            var query = "SELECT COUNT(*) FROM KhachHang ";
            using (var command = new MySqlCommand(query, connection))
            {
                total = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return total;
    }
    public decimal GetTotalRevenue()
    {
        decimal total = 0;

        using (var connection = _connection.GetConnection())
        {
            connection.Open();
            string query = "SELECT SUM(TongDoanhThu) FROM ThongKeDoanhThu";
            using var command = new MySqlCommand(query, connection);
            var result = command.ExecuteScalar();

            if (result != DBNull.Value)
                total = Convert.ToDecimal(result);

            return total;
        }
        
    }
}