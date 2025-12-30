using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User user);
    }
}