using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Junto.Core.Context;
using Junto.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Junto.Domain
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsNoTrackingAsync();

        Task<User> GetByIdAsNoTrackingAsync(long id);

        Task<User> GetByIdTrackingAsync(long id);

        void Create(User user);

        void Remove(User user);

        Task SaveChangesAsync();

        Task<User> GetByLoginPasswordAsync(string login, string password);
    }
}
