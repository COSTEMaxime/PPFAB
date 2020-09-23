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
            Console.WriteLine("Enter login : ");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password : ");
            string password = Console.ReadLine();

            IsValidCredentials credentialValidator = new IsValidCredentials(FileCredentialChecker);

            if (credentialValidator(login, password))
            {
                Console.WriteLine("Login success !");
            }
            else
            {
                Console.WriteLine("Login failed :(");
            }
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
    }
}
