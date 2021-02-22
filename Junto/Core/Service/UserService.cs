namespace Junto.Core.Service
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Junto.Domain;
    using Junto.Domain.Model;
    using Junto.Domain.Service;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _objIUserRepository;

        public UserService(IUserRepository objIUserRepository)
        {
            _objIUserRepository = objIUserRepository;
        }

        public Task<List<User>> GetAllAsNoTrackingAsync()
        {
            return _objIUserRepository.GetAllAsNoTrackingAsync();
        }

        public Task<User> GetByIdAsNoTrackingAsync(long id)
        {
            return _objIUserRepository.GetByIdAsNoTrackingAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _objIUserRepository.Create(user);
            await _objIUserRepository.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var obj = await _objIUserRepository.GetByIdTrackingAsync(user.Id);

            if (obj != null)
            {
                obj.Login = user.Login;
                obj.Password = user.Password;
                await _objIUserRepository.SaveChangesAsync();
            }

            return user;
        }

        public async Task RemoveAsync(long id)
        {
            var obj = await _objIUserRepository.GetByIdTrackingAsync(id);

            if (obj != null)
            {
                _objIUserRepository.Remove(obj);
                await _objIUserRepository.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var obj = await _objIUserRepository.GetByLoginPasswordAsync(user.Login, user.Password);

            if (obj != null)
            {
                return GenerateJwtToken(user);
            }

            return null;
        }

        public async Task ChangePasswordAsync(long id, ChangePasswordDto objDto)
        {
            var obj = await _objIUserRepository.GetByIdTrackingAsync(id);

            if (obj != null && obj.Password == objDto.CurrentPassword)
            {
                obj.Password = objDto.NewPassword;
                await _objIUserRepository.SaveChangesAsync();
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                }),
                Expires = Startup.JwtExpires,
                Issuer = Startup.Issuer,
                Audience = Startup.Audience,
                SigningCredentials = Startup.SigningCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
