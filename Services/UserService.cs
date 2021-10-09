using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;
using vtb_backend.Models;

namespace vtb_backend.Services
{
    public static class UserService
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
                    Email = reader.GetString("email"), Name = reader.GetString("user_id"),
                    Token = reader.GetString("hash")
                };
            }

            dbase.Close();
            return user;
        }

        public static bool UserExists(string username)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select username from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
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

        public static User CreateUser(User userWithoutPwd, string password)
        {
            var dbase = new DBManager();
            if (UserExists(userWithoutPwd.Email)) return null;
            var (key, value) = GenerateHash(password);
            var cmd = new MySqlCommand(
                $"insert into accounts(username,email,hash,value) values(@username,@email,'{key}','{value}',@miscrating')");
            // cmd.Parameters.AddStringsWithValues(new[]
            // {
            //     "@username", user.Username, "@fullname", user.FullName, "@email", user.Email, "@miscrating",
            //     user.MiscRating
            // });
            dbase.InsertCommand(cmd);
            dbase.Close();
            return userWithoutPwd;
        }

        public static bool ChangeUser(string oldUsername, User user)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"update accounts set username=@username, email=@email' where username='{oldUsername}'");
            cmd.Parameters.AddStringsWithValues(new[]
            {
                "@username", user.Name, "@email", user.Email
            });
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        public static bool DeleteUser(string username)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("delete from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        public static bool ChangeUserPassword(string username, string pwd)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select salt from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            var reader = dbase.GetReader(cmd);
            reader.Read();
            var salt = reader.GetString(0);
            var hash = GenerateHashFromSalt(pwd, salt);
            dbase.Close();
            dbase = new DBManager();
            cmd = new MySqlCommand($"update accounts set pwd='{hash}' where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        private static void AddStringsWithValues(this MySqlParameterCollection collection,
            IReadOnlyList<string> queries)
        {
            for (var i = 0; i < queries.Count; i += 2)
                collection.AddWithValue(queries[i], queries[i + 1]);
        }
    }
}