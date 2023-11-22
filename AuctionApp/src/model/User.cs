using AuctionApp.src.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.src.model
{
    public class User
    {

        private readonly string Name;
        private readonly string PassportNumber;
        private readonly string Login;
        private readonly string Password;

        public User (string name, string passportNumber, string password)
        {
            this.Name = name;
            this.PassportNumber = passportNumber;
            this.Password = password;
        }

        public static User ParseUser(string raw)
        {
            string[] data = raw.Split(';');
            if (data.Length < 3) throw new ArgumentException();

            return new User(data[0], data[1], data[2]);
        }

        public static User GetUser(DatabaseAccessor accessor, string login)
        {
            List<string> suitableUsers = accessor.ExecuteSelect($"SELECT * FROM Users WHERE login='{login}'");
            if (suitableUsers.Count == 0) return null;
            return ParseUser(suitableUsers[0]);
        }

        public string GetName()
        {
            return Name;
        }

        public string GetPassportNumber()
        {
            return PassportNumber;
        }
        
        public string GetLogin()
        {
            return Login;
        }

        public string GetPassword()
        {
            return Password;
        }

    }
}
