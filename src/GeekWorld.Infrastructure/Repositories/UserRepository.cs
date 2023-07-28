
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Enums;
using GeekWorld.Domain.Models;
using GeekWorld.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace GeekWorld.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                return null;
            }

            bool passwordAvailable = BCrypt.Net.BCrypt.Verify(password, user.Password);
            
            if (!passwordAvailable)
            {
                return null;
            }
            return user;
        }
        
        public async Task<IEnumerable<User>> GetAll(Guid meId, int page, int limit, string search)
        {

            List<User> users = search.Length != 0 ? await _context.Users.Where(u => u.Id != meId && (u.FullName == search || u.Email == search)).OrderByDescending(u => u.FullName)
                 .Skip((page - 1) * limit)
                 .Take(limit).ToListAsync() : await _context.Users.Where(u => u.Id != meId).OrderByDescending(u => u.FullName)
                 .Skip((page - 1) * limit)
                 .Take(limit).ToListAsync();

            users.ForEach(u => u.HidePassword());
            return users;
        }

 

        public async Task<User?> Update(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return null;
            }
            
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            existingUser.HidePassword();

            return existingUser;
        }

        public async Task<bool> ValidateEmailAlreadyExists(string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
        }

        public async Task<User?> GetById(Guid id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAllFriends(Guid meId, int page, int limit, string search)
        {
            var friends = search.Length != 0 ? await _context.Friendships
                .Where(f => f.UserId == meId && f.Status == StatusFriendship.ACCEPTED.ToString().ToUpper())
                .Select(f => f.Friend)
                .Where(u => u.FullName == search || u.Email == search)
                .OrderByDescending(u => u.FullName)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync() :
                await _context.Friendships
                .Where(f => f.UserId == meId && f.Status == StatusFriendship.ACCEPTED.ToString().ToUpper())
                .Select(f => f.Friend)
                .OrderByDescending(u => u.FullName)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();


            friends.ForEach(u => u.HidePassword());

            return friends;
        }


    }
}
