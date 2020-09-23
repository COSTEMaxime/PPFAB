using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        static void Main(string[] args)
        {
            Console.WriteLine("Enter login : ");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password : ");
            string password = Console.ReadLine();

            IsValidCredentials credentialValidator = new IsValidCredentials(hashCredentialChecker);

            if (credentialValidator(login, password))
            {
                Console.WriteLine("Login success !");
            }
            else
            {
                Console.WriteLine("Login failed :(");
            }
        }

        private static bool hardcodedCredentialChecker(string login, string password)
        {
            return login == "robert" && password == "password123";
        }

        private static bool dictionaryCredentialChecker(string login, string password)
        {
            return users.ContainsKey(login) && users[login] == password;
        }

        private static bool hashCredentialChecker(string login, string password)
        {
            return usersHash.ContainsKey(login) && usersHash[login] == hash(password);
        }

        private static string hash(string input)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
