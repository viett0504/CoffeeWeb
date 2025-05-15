using MySql.Data.MySqlClient;

public class AccountService : IAccountService
{
    private readonly MssqlConnection _db;

    public AccountService(MssqlConnection db)
    {
        _db = db;
    }

    public bool CheckLogin(string email, string password)
    {
        using (var conn = _db.GetConnection())
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email AND MatKhau = @Password";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                var result = Convert.ToInt32(cmd.ExecuteScalar());
                return result > 0;
            }
        }
    }
}
