using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _06_UserLogin
{
    class Program
    {
        delegate bool IsValidCredentials(string login, string password);
        static readonly IDictionary<string, string> users = new Dictionary<string, string>()
        {
            { "robert", "password123" },
            { "root", "root" },
            { "admin", "admin" },
            { "mySecureAccount", "passw0rd" },
        };

        static readonly IDictionary<string, string> usersHash = new Dictionary<string, string>()
        {
            { "robert", "75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=" },
            { "root", "SBNJTRN+FjG7owHVrKtue7eqdM4RhdRWVl71HXN2d7I=" },
            { "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=" },
            { "mySecureAccount", "jw4vduIrQ+KFUYmHfn3B4efZjCJsldskfNHVR5KDNKk=" },
        };

        static readonly IDictionary<string, string> usersFromFile = LoadFromFile("data.json");

        private static IDictionary<string, string> LoadFromFile(string file)
        {
            string content = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
        }

        static void Main(string[] args)
        {
            string login, password;
            Console.WriteLine("=== SETUP ===");
            Console.WriteLine("Recreate database ? (y/n)");
            string userInput = Console.ReadLine();
            if (userInput.StartsWith("y"))
            {
                DAO.GetInstance().DropAndCreateDb();
            }

            Console.WriteLine("Add a new user ? (y/n)");
            userInput = Console.ReadLine();
            while (userInput.StartsWith("y"))
            {
                AskForLoginAndPassword(out login, out password);
                bool success = DAO.GetInstance().AddUser(login, hash(password));
                Console.WriteLine(success ? "Success" : "Error");

                Console.WriteLine("Add a new user ? (y/n)");
                userInput = Console.ReadLine();
            }


            Console.WriteLine("=== LOGIN ===");
            AskForLoginAndPassword(out login, out password);
            IsValidCredentials credentialValidator = new IsValidCredentials(DatabaseCredentialChecker);

            if (credentialValidator(login, password))
            {
                Console.WriteLine("Login success !");
            }
            else
            {
                Console.WriteLine("Login failed :(");
            }

            Console.ReadLine();
        }

        private static bool HardcodedCredentialChecker(string login, string password)
        {
            return login == "robert" && password == "password123";
        }

        private static bool DictionaryCredentialChecker(string login, string password)
        {
            return users.ContainsKey(login) && users[login] == password;
        }

        private static bool HashCredentialChecker(string login, string password)
        {
            return usersHash.ContainsKey(login) && usersHash[login] == hash(password);
        }

        private static string hash(string input)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        private static bool FileCredentialChecker(string login, string password)
        {
            return usersFromFile.ContainsKey(login) && usersFromFile[login] == hash(password);
        }

        private static bool DatabaseCredentialChecker(string login, string password)
        {
            var user = DAO.GetInstance().GetUserBylogin(login);
            return user.Item2 == hash(password);
        }

        private static void AskForLoginAndPassword(out string login, out string password)
        {
            Console.WriteLine("Enter login : ");
            login = Console.ReadLine();
            Console.WriteLine("Enter password : ");
            password = Console.ReadLine();
        }
    }
}
