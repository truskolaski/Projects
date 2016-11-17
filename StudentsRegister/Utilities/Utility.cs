using StudentsRegister.Models.Register;
using System;
using System.Security.Cryptography;

namespace StudentsRegister.Utilities
{
    public static class Utility
    {
        public static void SetSaltAndPassword(out string salt, out string password, RegisterModel registerModel)
        {
            salt = Guid.NewGuid().ToString();
            password = registerModel.Password;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + password);
            data = MD5.Create().ComputeHash(data);
            password = Convert.ToBase64String(data);
        }
    }
}