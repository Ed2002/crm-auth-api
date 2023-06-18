using CrmAuth.Domain.Util;
using System.Security.Cryptography;
using System.Text;

namespace CrmAuth.Utils
{
    public class Util
    {
        public Password CreatePasswordHash(string Password)
        {
            Password pass = new();
            using(var hmac = new HMACSHA512())
            {
                pass.PasswordSalt = hmac.Key;
                pass.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
            return pass;
        }

        public byte[] ConvertStringToByteArray(string Hex)
        {
            string[] hexValues = Hex.Split('-');
            byte[] byteArray = new byte[hexValues.Length];

            for (int i = 0; i < hexValues.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hexValues[i], 16);
            }

            return byteArray;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)    
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computHash.SequenceEqual(passwordHash);
            }
        }
    }
}