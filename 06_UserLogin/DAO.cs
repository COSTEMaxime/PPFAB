using System;
using System.Data.SQLite;
using System.IO;

namespace _06_UserLogin
{
    class DAO
    {
        private static DAO instance = null;
        private static readonly string connectionString = "Data Source=MyDatabase.sqlite;Version=3;";
        private static readonly string databaseFile = "MyDatabase.sqlite";

        private DAO()
        {
            if (!File.Exists(databaseFile))
            {
                DropAndCreateDb();
            }
        }

        public static DAO GetInstance()
        {
            if (instance == null)
            {
                instance = new DAO();
            }

            return instance;
        }

        public void DropAndCreateDb()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = "create table users (login varchar(50), password varchar(100))";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();

                sql = "insert into users (login, password) values " +
                    "('robert', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=')," +
                    "('root', 'SBNJTRN+FjG7owHVrKtue7eqdM4RhdRWVl71HXN2d7I=')," +
                    "('admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=')," +
                    "('mySecureAccount', 'jw4vduIrQ+KFUYmHfn3B4efZjCJsldskfNHVR5KDNKk=')";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        public Tuple<string, string> GetUserBylogin(string login)
        {
            string loginFromDb = "";
            string password = "";
            string sql = "select * from users where login = @login";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@login", login);
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    loginFromDb = (string)reader["login"];
                    password = (string)reader["password"];
                }
            }
            return new Tuple<string, string>(loginFromDb, password);
        }

        public bool AddUser(string login, string password)
        {
            string sql = "insert into users (login, password) values (@login, @password)";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                return Convert.ToBoolean(command.ExecuteNonQuery());
            }
        }
    }
}
