using MySql.Data.MySqlClient;
public class UserService : IUserService
{
    private readonly MssqlConnection _connection;

    public UserService(MssqlConnection connection)
    {
        _connection = connection;
    }

    public UserInfo GetUserInfoById(int maND)
    {
        using var conn = _connection.GetConnection();
        conn.Open();

        string query = "SELECT HoTen, NgaySinh, VaiTro FROM NguoiDung WHERE MaND = @MaND";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@MaND", maND);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new UserInfo
            {
                HoTen = reader.GetString("HoTen"),
                NgaySinh = reader.GetDateTime("NgaySinh"),
                VaiTro = reader.GetString("VaiTro")
            };
        }

        throw new Exception("Không tìm thấy người dùng.");
    }
}
