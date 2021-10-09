using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;
using vtb_backend.Models;

namespace vtb_backend.Services
{
    public static class AccountService
    {
        public static User GetUser(string token)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select * from accounts where hash=@token");
            cmd.Parameters.AddWithValue("@token", token);
            var reader = dbase.GetReader(cmd);
            User user = null;
            if (reader.Read())
            {
                user = new User
                {
                    Email = reader.GetString("email"), Name = reader.GetString("name"), Password = ""
                };
            }

            dbase.Close();
            return user;
        }

        public static bool UserExists(string email)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select email from accounts where email=@email");
            cmd.Parameters.AddWithValue("@email", email);
            var reader = dbase.GetReader(cmd);
            var rvalue = reader.Read();
            dbase.Close();
            return rvalue;
        }

        private static KeyValuePair<string, string> GenerateHash(string s)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var stringSalt = Convert.ToBase64String(salt);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(s, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));
            return new KeyValuePair<string, string>(hash, stringSalt);
        }

        private static string GenerateHashFromSalt(string s, string strSalt) => Convert.ToBase64String(
            KeyDerivation.Pbkdf2(s, Convert.FromBase64String(strSalt), KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));


        //Returns: token
        public static string CreateUser(User user)
        {
            var dbase = new DBManager();
            if (UserExists(user.Email)) return null;
            var (hash, salt) = GenerateHash(user.Password);
            var cmd = new MySqlCommand(
                $"insert into accounts(name,email,hash,salt) values(@name,@email,'{hash}','{salt}')");
            cmd.Parameters.AddStringsWithValues(new[]
            {
                "@name", user.Name, "@email", user.Email
            });
            dbase.InsertCommand(cmd);
            dbase.Close();
            return hash;
        }

        private static void AddStringsWithValues(this MySqlParameterCollection collection,
            IReadOnlyList<string> queries)
        {
            for (var i = 0; i < queries.Count; i += 2)
                collection.AddWithValue(queries[i], queries[i + 1]);
        }
    }
}