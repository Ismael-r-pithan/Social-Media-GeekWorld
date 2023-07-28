using GeekWorld.Domain.Models;

namespace GeekWorld.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User?> Login(string email, string password);

        Task<User?> Add(User user);

        Task<User?> Update(User user);

        Task<IEnumerable<User>> GetAll(Guid meId, int page, int limit, string search);

        Task<IEnumerable<User>> GetAllFriends(Guid meId, int page, int limit, string search);

        Task<User?> GetById(Guid id);

        Task<Boolean> ValidateEmailAlreadyExists(string email);



    }
}
