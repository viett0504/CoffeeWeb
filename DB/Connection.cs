using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

public class MssqlConnection
{
    private readonly string _connectionString;

    public MssqlConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection") ?? throw new ArgumentNullException("Connection string not found.");
    }

    public object TaiKhoan { get; internal set; }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

}
