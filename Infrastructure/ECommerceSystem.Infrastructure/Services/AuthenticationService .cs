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