using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMA_Core.Models;
using PMA_Core.Repositories;
using PMA_Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Data.Implementations
{
    public class TokenHandlerRepository : ITokenHandlerRepository
    {
        // Constant string representing the secret key used for JWT token signing and validation.
        public const string JWT_SECURITY_KEY = "B5HuFJRWtByZXtgLJ6WpV0jZyxE4aDMy";
        // Constant defining the validity duration of the JWT token in minutes.
        private const int JWT_TOKEN_VALIDITY_MINS = 10;

        private readonly ApplicationDbContext _context;
        public TokenHandlerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResponse?> GenerateJwtToken(AuthRequest authRequest)
        {
            //Validation For Empty username or password
            if (string.IsNullOrWhiteSpace(authRequest.Email) || string.IsNullOrWhiteSpace(authRequest.Password))
            {
                return null;
            }

            //Validation based upon email user exists or not
            var existingUser =await _context.PMA_Users.Where(x => x.Email == authRequest.Email).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                return null;
            }
            var passwordHash = VerifyAndGetPasswordAsync(existingUser.PasswordHash, authRequest.Password);
            if(existingUser.PasswordHash == null)
            {
                return null;
            }
            if(existingUser.PasswordHash == passwordHash)
            {
                //only to get RoleName
                var user = await _context.PMA_Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.UserID == existingUser.UserID);
                string userRoleName = user.UserRole.RoleName;

                var tokenExpTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
                var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

                var claims = new List<Claim>
             {
                new Claim("Username", authRequest.Email),
                 new Claim("Role", userRoleName),
             };

                var claimsIdentity = new ClaimsIdentity(claims);
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature);

                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = tokenExpTimeStamp,
                    SigningCredentials = signingCredentials
                };

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                var token = jwtSecurityTokenHandler.WriteToken(securityToken);

                return new AuthResponse
                {
                    UserName = user.FirstName,
                    ExpiresIn = (int)tokenExpTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                    JwtToken = token,
                    UserID = user.UserID,
                    Role = userRoleName
                };
            }
            return null;            
        }

        #region VerifyPassword
        private string VerifyAndGetPasswordAsync(string hashedPasswordInDatabase, string providedPassword)
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
        #endregion
    }
}
