using System;
using System.Data.SQLite;

namespace _06_UserLogin
{
    class DAO
    {
        private static DAO instance = null;
        private SQLiteConnection connection;

        private DAO()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            connection.Open();

            string sql = "create table users (login varchar(50), password varchar(100))";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            sql = "insert into users (login, password) values " +
                "('robert', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=')," +
                "('root', 'SBNJTRN+FjG7owHVrKtue7eqdM4RhdRWVl71HXN2d7I=')," +
                "('admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=')," +
                "('mySecureAccount', 'jw4vduIrQ+KFUYmHfn3B4efZjCJsldskfNHVR5KDNKk=')";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static DAO GetInstance()
        {
            if (instance == null)
            {
                instance = new DAO();
            }

            return instance;
        }

        public Tuple<string, string> GetUserBylogin(string login)
        {
            string sql = "select * from users where login = @login";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@login", login);
            SQLiteDataReader reader = command.ExecuteReader();

            string loginFromDb = "";
            string password = "";
            if (reader.Read())
            {
                loginFromDb = (string)reader["login"];
                password = (string)reader["password"];
            }
            return new Tuple<string, string>(loginFromDb, password);
        }
    }
}
