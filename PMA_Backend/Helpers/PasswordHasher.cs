using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace PMA_Backend.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute the hash from the password bytes
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a hexadecimal string
                var hashStringBuilder = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    hashStringBuilder.Append(hashByte.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Create an instance of PasswordHasher<TUser>
            var passwordHasher = new PasswordHasher<IdentityUser>();

            // Verify the provided password against the hashed password
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);

            // Check the result and return true if the password matches
            return result == PasswordVerificationResult.Success;
        }
    }
}
