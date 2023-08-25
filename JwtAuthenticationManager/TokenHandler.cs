using Microsoft.IdentityModel.Tokens;
using PMA_Core.Models;
using PMA_Core.Repositories;
using PMA_Data;
using PMA_Services.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class TokenHandler
    {
        // Constant string representing the secret key used for JWT token signing and validation.
        public const string JWT_SECURITY_KEY = "B5HuFJRWtByZXtgLJ6WpV0jZyxE4aDMy";
        // Constant defining the validity duration of the JWT token in minutes.
        private const int JWT_TOKEN_VALIDITY_MINS = 10;
        private List<PMA_User> _userAccountList;
        private readonly ProjectService _projectService;
        public TokenHandler(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public AuthResponse? GenerateJwtToken(AuthRequest authRequest)
        {
            // Check if the username or password is missing.
            if (string.IsNullOrWhiteSpace(authRequest.Email) || string.IsNullOrWhiteSpace(authRequest.Password))
                return null;

          

            var existingUser = _userAccountList.FirstOrDefault(x => x.Email == authRequest.Email);
            var passwordHash = VerifyAndGetPasswordAsync(existingUser.PasswordHash, authRequest.Password);

            // VALIDATION //
            // Find a user account that matches the provided username and password.
            var userAccount = _userAccountList.FirstOrDefault(x => x.Email == authRequest.Email && x.PasswordHash == passwordHash);
            if (userAccount == null) return null;

            // Calculate the expiration timestamp of the JWT token.
            var tokenExpTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

            // Convert the JWT security key to bytes for token signing.
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            // Create claims representing the user's identity and role.
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authRequest.Email),
                new Claim("Role", userAccount.UserRole.RoleName)
            });

            // Create signing credentials for the JWT token.
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            // Create a security token descriptor containing token information.
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpTimeStamp,
                SigningCredentials = signingCredentials
            };

            // Create a new instance of JwtSecurityTokenHandler.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            // Create a security token based on the token descriptor.
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            // Write the JWT token to a string.
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            // Return an AuthResponse containing token-related information.
            return new AuthResponse
            {
                Email = authRequest.Email,
                ExpiresIn = (int)tokenExpTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }

        private  string VerifyAndGetPasswordAsync(string hashedPasswordInDatabase, string providedPassword)
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
                if (string.Equals(hashedPasswordInDatabase, hashedProvidedPassword, StringComparison.OrdinalIgnoreCase))
                {
                    // Password is verified, return the hashed password from the database
                    return hashedPasswordInDatabase;
                }
                else
                {
                    // Password verification failed
                    return null; // or throw an exception, depending on your requirements
                }
            }
        }
    }
}
