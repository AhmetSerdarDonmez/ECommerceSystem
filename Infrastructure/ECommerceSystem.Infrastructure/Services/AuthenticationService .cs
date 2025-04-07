using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Services;
using ECommerceSystem.Domain.Entities.Users;
using ECommerceSystem.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserReadRepository _userRepository;
        private readonly IJwtTokenService _tokenService;

        public AuthenticationService(IUserReadRepository userRepository, IJwtTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<(bool Success, string? Token)> AuthenticateAsync(string username, string password)
        {
            // Asenkron olarak kullanıcıyı çekiyoruz ve Role bilgisini de yüklüyoruz
            var user = await _userRepository.GetWhere(u => u.UserName == username)
                                            .Include(u => u.Role) // This line is crucial for loading the Role
                                            .FirstOrDefaultAsync();

            // Eğer kullanıcı bulunamazsa veya şifre doğrulanamazsa false döner
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return (false, null);
            }

            // Kullanıcının rol bilgisini string'e çeviriyoruz
            var roleId = user.Role?.RoleId ?? 1; // Use null-conditional operator to avoid potential null exception

            // Token üretimi: Kullanıcı id, kullanıcı adı ve roller parametre olarak veriliyor
            var token = _tokenService.GenerateToken(user.UserId.ToString(), user.UserName, roleId.ToString());
            return (true, token);
        }


        // Add this method INSIDE the ECommerceSystem.Infrastructure.Services.AuthenticationService class

        public async Task<(bool Success, string? Token)> GenerateTokenForUserAsync(User user)
        {
            if (user == null)
            {
                // Log error: Cannot generate token for null user
                return (Success: false, Token: null);
            }

            // IMPORTANT: The GenerateToken method needs the RoleId.
            // Ensure the Role is loaded on the 'user' object passed in.
            // If unsure if the caller loaded it, it's safer to reload the user here.
            User userWithRole = user; // Assume role might be loaded by caller for now
            if (user.Role == null) // Check if Role is actually loaded
            {
                // If not loaded by the caller, reload the user with their Role included
                userWithRole = await _userRepository.GetWhere(u => u.UserId == user.UserId) // Assuming UserId is unique key
                                                     .Include(u => u.Role)
                                                     .FirstOrDefaultAsync();
                if (userWithRole == null)
                {
                    // Log error: User not found when reloading for token generation
                    return (Success: false, Token: null);
                }
            }

            // Use null-conditional operator and default RoleId if necessary (matches AuthenticateAsync)
            var roleId = userWithRole.Role?.RoleId ?? 1; // Default to RoleId 1 if null

            try
            {
                // Directly call the token service with the user details
                var token = _tokenService.GenerateToken(userWithRole.UserId.ToString(), userWithRole.UserName, roleId.ToString());

                if (string.IsNullOrEmpty(token))
                {
                    // Log error: Token service returned empty token
                    return (Success: false, Token: null);
                }
                return (Success: true, Token: token);
            }
            catch (Exception ex)
            {
                // Log exception ex during token generation
                Console.WriteLine($"Error in GenerateTokenForUserAsync: {ex.Message}"); // Use proper logging
                return (Success: false, Token: null);
            }
        }

        /// <summary>
        /// Şifre doğrulaması yapar.
        /// </summary>
        /// <param name="password">Kullanıcının girdiği şifre</param>
        /// <param name="storedPassword">Veritabanından alınan şifre (hashlenmemiş)</param>
        /// <returns>Şifre doğrulaması başarılı ise true, aksi halde false</returns>
        private bool VerifyPassword(string password, string storedPassword)
        {
            // Bu örnekte şifreler hashlenmemiş olarak saklanmaktadır.
            // Üretimde, kesinlikle hashlenmiş ve salt kullanılarak saklanmalıdır.
            return password == storedPassword;
        }
    }
}