using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories.Interfaces;
using MovieHub.Utilities.Configurations.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            MovieHubContext<User> context,
            IOptions<ShowCollection> options,
            ILogger<BaseRepository<User>> logger)
            : base(context, options.Value.CollectionName, logger)
        {
        }

        public async Task<bool> AddAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) return false;
            try
            {
                user.Password = HashPassword(user.Password);
                await _data.InsertOneAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<User> GetAsync(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                return await _data.Find(filter).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                var hashedPassword = HashPassword(password);
                var filter = Builders<User>.Filter.Eq(x => x.Email, email)
                    & Builders<User>.Filter.Eq(u => u.Password, hashedPassword);

                return await _data.Find(filter).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
