using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Note.Core.Helpers
{
    public static class SecurityHelper
    {
        public static string EncryptPassword(string password, string salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: Encoding.ASCII.GetBytes(salt),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
        }

        public static string GetNewSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string GetNewRefreshToken()
        {
            byte[] randomNumber = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
    }
}
