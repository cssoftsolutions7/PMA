using Microsoft.AspNet.Identity;
using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class UserRegistrationService
    {
        private readonly IUserRegistrationRepository _userRepository;


        public UserRegistrationService(IUserRegistrationRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PMA_User> RegisterUserAsync(PMA_User user, string password)
        {
            // Check if the user with the same email already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }
            var hashedPassword = HashPassword(password);
            user.PasswordHash = hashedPassword;

            // Hash the user's password before saving it to the database 
            var registeredUser = await _userRepository.CreateUserAsync(user);

            return registeredUser;
        }
        #region Hashing
        private string HashPassword(string password)
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

        #endregion
    }
}
