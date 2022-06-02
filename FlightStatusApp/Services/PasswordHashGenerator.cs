using System.Security.Cryptography;
using System.Text;

namespace FlightStatusApp.Services;

public class PasswordHashGenerator
{
    public static string HashPassword(string password)
    {
        using (var hash = SHA1.Create())
        {
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }
    }

    public static bool IsValidPassword(string hash, string password)
    {
        var passwordHash = HashPassword(password);
        return hash == passwordHash;
    }
}