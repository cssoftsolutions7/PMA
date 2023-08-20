using Microsoft.AspNet.Identity;
using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class UserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        public async Task<PMA_User> GetUserProfileAsync(int userId)
        {
            return await _userProfileRepository.GetUserByIdAsync(userId);
        }

        public async Task UpdateUserProfileAsync(int userId, PMA_UserProfileUpdate updatedUser)
        {
            var existingUser = await _userProfileRepository.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                throw new ApplicationException("User not found");
            }

            // Update user properties based on your requirements
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.Address = updatedUser.Address;
            existingUser.BirthDate = updatedUser.BirthDate;
            existingUser.Gender = updatedUser.Gender;

            await _userProfileRepository.UpdateUserAsync(existingUser);
        }
    }
}
