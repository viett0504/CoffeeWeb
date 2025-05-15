using MyWebApp.Models;
using MySql.Data.MySqlClient;

namespace MyWebApp.Services
{
    public class RevenueService
    {
        private readonly MssqlConnection _connection;

        public RevenueService(MssqlConnection connection)
        {
            _connection = connection;
        }

        public List<Revenue> GetRevenues()
        {
            var list = new List<Revenue>();

            using var conn = _connection.GetConnection();
            conn.Open();

            var query = "SELECT MaTK, Thang, Nam, TongDoanhThu FROM ThongKeDoanhThu ORDER BY Nam DESC, Thang DESC";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Revenue
                {
                    MaTK = reader.GetInt32("MaTK"),
                    Thang = reader.GetInt32("Thang"),
                    Nam = reader.GetInt32("Nam"),
                    TongDoanhThu = reader.GetDecimal("TongDoanhThu")
                });
            }

            return list;
        }

        public List<Revenue> GetMonthlyRevenue()
        {
            var list = new List<Revenue>();
            using var connection = _connection.GetConnection();
            connection.Open();

            string query = "SELECT Thang, Nam, TongDoanhThu FROM ThongKeDoanhThu ORDER BY Nam, Thang";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Revenue
                {
                    Thang = reader.GetInt32("Thang"),
                    Nam = reader.GetInt32("Nam"),
                    TongDoanhThu = reader.GetDecimal("TongDoanhThu")
                });
            }

            return list;
        }
    }
}
