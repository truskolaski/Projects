using StudentsRegister.DataContexts;
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

        public static void GetHashedPassword(string email, string salt, ref string password, string passedPassword, ref int? status, ref string statusText)
        {
            try
            {
                using (var db = new StudentsRegisterDataContext())
                {
                    db.WWW_GetSalt(email, ref salt, ref status, ref statusText);
                }

                password = passedPassword;

                byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + password);
                data = MD5.Create().ComputeHash(data);
                password = Convert.ToBase64String(data);
            }
            catch(Exception ex)
            {
                status = -1;
                statusText = "Exception occurred: " + ex.Message;

                throw ex;
            }
        }
    }
}