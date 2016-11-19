using StudentsRegister.Models.Register;
using System;
using System.Security.Cryptography;

namespace StudentsRegister.Utilities
{
    public static class Utility
    {
        public static void SetSaltAndPassword(out string salt, out string password, string passedPassword)
        {
            salt = Guid.NewGuid().ToString();
            password = passedPassword;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + password);
            data = MD5.Create().ComputeHash(data);
            password = Convert.ToBase64String(data);
        }

        public static void GetHashedPassword(string salt, out string password, string passedPassword)
        {
            password = passedPassword;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + password);
            data = MD5.Create().ComputeHash(data);
            password = Convert.ToBase64String(data);
        }
    }
}