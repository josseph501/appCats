using Application.Interfaces;
using Application.Users.DTOs;
using Domain.Contracts;
using Domain.Entities;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> RegisterAsync(RegisterUserDto dto)
        {

            try
            {
                var existing = await _repository.GetByEmailAsync(dto.Email);
                if (existing != null)
                    throw new InvalidOperationException("Usuario ya existe");

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = dto.Email,
                    Username = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                await _repository.CreateAsync(user);
                return user;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear Usuario: " + ex.Message);
            }

        }

        public async Task<User> LoginAsync(LoginUserDto dto)
        {

            try
            {
                var user = await _repository.GetByEmailAsync(dto.Email);
                if (user == null)
                    throw new Exception("No existe usuario");

                var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
                if (!valid)
                    throw new Exception("usuario invalido");

                return user;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar logiarse: " + ex.Message);
            }

        }

    }
}