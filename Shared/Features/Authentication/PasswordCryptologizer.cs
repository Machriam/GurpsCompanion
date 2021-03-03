using System.Security.Cryptography;
using System.Text;

namespace GurpsCompanion.Shared.Features.Authentication
{
    public interface IPasswordCryptologizer
    {
        string EncryptString(string payload);
    }

    public class PasswordCryptologizer : IPasswordCryptologizer
    {
        public string EncryptString(string payload)
        {
            using var sha512 = SHA512Managed.Create();
            var bytes = Encoding.UTF8.GetBytes(payload);
            var hash = sha512.ComputeHash(bytes);
            var stringBuilder = new StringBuilder();
            foreach (var obj in hash) stringBuilder.Append(obj.ToString("X2"));
            return stringBuilder.ToString();
        }
    }
}
