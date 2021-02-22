namespace XUnitTestJunto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Junto.Domain;
    using Junto.Domain.Model;
    using Junto.Domain.Service;

    /// <summary>>
    /// User Service
    /// </summary>
    public class UserRepositoryFake : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepositoryFake()
        {
            _users = new List<User>()
            {
                new User { Id = 1, Login = "Junto", Password = "JuntoPassword"},
                new User { Id = 2, Login = "QualquerCoisa", Password = "QualquerCoisaSenha"},
            };
        }

        public Task<List<User>> GetAllAsNoTrackingAsync()
        {
            return Task.FromResult(_users);
        }

        public Task<User> GetByIdAsNoTrackingAsync(long id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
        }

        public Task<User> GetByIdTrackingAsync(long id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
        }

        public void Create(User user)
        {
            user.Id = new Random().Next(0, int.MaxValue);
            _users.Add(user);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public void Remove(User user)
        {
            _users.Remove(user);
        }

        public Task<User> GetByLoginPasswordAsync(string login, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Login == login && x.Password == password));
        }
    }
}
