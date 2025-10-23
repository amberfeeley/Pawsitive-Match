using MySql.Data.MySqlClient;

public class DbConnection
{
    private DbConnection()
    {
    }

    public string DbName = "pawsitive_match";
    public static DbConnection _instance = null;
    public MySqlConnection Connection { get; set;  }
    public static DbConnection CreateInstance()
    {
        if (_instance == null)
        {
            _instance = new DbConnection();
        }
        return _instance;
    }
    
    // TODO: Update connection string
    public string myConnectionString = $"server=localhost:8888/phpMyAdmin5;uid=root;pwd=root;database=pawsitive_match";

    public bool IsConnected()
    {
        if (Connection == null)
        {
            if (string.IsNullOrEmpty(DbName))
            {
                return false;
            }
            Connection = new MySqlConnection(myConnectionString);
            Connection.Open();
        }
        return true;
    }

    public void Close()
    {
        Connection.Close();
    }
}