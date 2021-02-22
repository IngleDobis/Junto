namespace Junto.Domain.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Junto.Domain.Model;

    /// <summary>
    /// User service interface
    /// </summary>
    public interface IUserService
    {
        Task<List<User>> GetAllAsNoTrackingAsync();

        Task<User> GetByIdAsNoTrackingAsync(long id);

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task RemoveAsync(long id);

        Task<string> GenerateTokenAsync(User user);

        Task ChangePasswordAsync(long id, ChangePasswordDto objDto);
    }
}
