using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Mongo;
using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(MongoContext context, MongoSettings settings)
        {
            _collection = context.Database.GetCollection<User>(
                settings.UsersCollectionName
            );
        }

        public async Task<User?> GetByEmailAsync(string email)
        {

            try
            {
                return await _collection
                    .Find(u => u.Email == email)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> GetByIdAsync(string id)
        {

            try
            {
                return await _collection
                    .Find(u => u.Id == id)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await _collection.InsertOneAsync(user);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}