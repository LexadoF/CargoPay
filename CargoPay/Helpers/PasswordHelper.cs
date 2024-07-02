using System.Security.Cryptography;
using System.Text;

namespace CargoPay.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = MD5.HashData(inputBytes);
            return string.Concat(hashBytes.Select(b => b.ToString("X2")));
        }
    }
}