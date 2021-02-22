using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Junto.Core.Context;
using Junto.Domain;
using Junto.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Junto.Core.Repository
{
    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository: IUserRepository
    {
        /// <summary>
        /// JuntoContext
        /// </summary>
        private readonly JuntoContext _objdbContext;

        public UserRepository(JuntoContext dbContext)
        {
            _objdbContext = dbContext;
        }

        public Task<List<User>> GetAllAsNoTrackingAsync()
        {
            return _objdbContext.Set<User>().AsNoTracking().ToListAsync();
        }

        public Task<User> GetByIdAsNoTrackingAsync(long id)
        {
            return _objdbContext.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<User> GetByIdTrackingAsync(long id)
        {
            return _objdbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(User user)
        {
            _objdbContext.Add(user);
        }

        public void Remove(User user)
        {
            _objdbContext.Remove(user);
        }

        public Task SaveChangesAsync()
        {
            return _objdbContext.SaveChangesAsync();
        }

        public Task<User> GetByLoginPasswordAsync(string login, string password)
        {
            return _objdbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }
    }
}
