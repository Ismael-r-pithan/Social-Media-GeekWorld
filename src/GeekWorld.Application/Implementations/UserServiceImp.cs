using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using AutoMapper;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Implementations
{
    public class UserServiceImp : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServiceImp(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAll(Guid meId, int page, int limit, string search)
        {
            var users = await _userRepository.GetAll(meId, page, limit, search);
            IEnumerable<UserResponse> response = users.Select(user => _mapper.Map<UserResponse>(user)).ToList();
            return response;
        }

        public async Task<bool> ValidateEmailAlreadyExists(string email)
        {
            return await _userRepository.ValidateEmailAlreadyExists(email);
        }

        public async Task<UserResponse> GetById(Guid userId)
        {
            UserResponse response = new();
            User? user = await _userRepository.GetById(userId);
            if (user == null)
            {
                 response.AddNotification(new Validations.Notification("Seu Usuário não foi encontrado, enctre em contato com supp@email.com"));
                 return response;

            }
            return _mapper.Map<UserResponse>(user);
        }
    }
}
