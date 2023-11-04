using MovieHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> GetAsync(string id);

        Task<User> LoginAsync(string email, string password);


        Task<bool> AddAsync(User user);
    }
}
