using PMA_Core.Models;
using PMA_Core.Repositories;
using PMA_Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class UserLoginService
    {
        private readonly IUserRegistrationRepository _userRepository;

        public UserLoginService(IUserRegistrationRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<PMA_User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (!VerifyPassword(user.PasswordHash, password))
            {
                throw new Exception("Invalid password.");
            }

            return user;
        }

        // Implement the VerifyPassword method
        private bool VerifyPassword(string hashedPasswordInDatabase, string providedPassword)
        {
            // Compare the provided password with the hashed password in the database
            return VerifyPasswordAsync(hashedPasswordInDatabase, providedPassword).Result;
        }

        private async Task<bool> VerifyPasswordAsync(string hashedPasswordInDatabase, string providedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute the hash from the provided password bytes
                var passwordBytes = Encoding.UTF8.GetBytes(providedPassword);
                var hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a hexadecimal string
                var hashStringBuilder = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    hashStringBuilder.Append(hashByte.ToString("x2"));
                }

                var hashedProvidedPassword = hashStringBuilder.ToString();

                // Compare the two hashes to verify the password
                return string.Equals(hashedPasswordInDatabase, hashedProvidedPassword, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}

