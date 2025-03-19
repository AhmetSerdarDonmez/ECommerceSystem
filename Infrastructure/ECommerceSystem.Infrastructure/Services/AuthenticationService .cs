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
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserReadRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<(bool Success, string? Token)> AuthenticateAsync(string username, string password)
        {
            // Asenkron olarak kullanıcıyı çekiyoruz
            var user = await _userRepository.GetWhere(u => u.UserName == username).FirstOrDefaultAsync();

            // Eğer kullanıcı bulunamazsa veya şifre doğrulanamazsa false döner
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return (false, null);
            }

            // Kullanıcının rol bilgisini string'e çeviriyoruz
            var roleId = user.Role != null ? user.Role.RoleId : 1;
            var roles = new List<string> { roleId.ToString() };

            // Token üretimi: Kullanıcı id, kullanıcı adı ve roller parametre olarak veriliyor
            var token = _tokenService.GenerateToken(user.UserId.ToString(), user.UserName, roles);
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
