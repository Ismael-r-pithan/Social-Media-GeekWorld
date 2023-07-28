using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Implementations
{
    public class MeServiceImp : IMeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MeServiceImp(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllFriends(Guid meId, int page, int limit, string search)
        {
            var users = await _userRepository.GetAllFriends(meId, page, limit, search);
            IEnumerable<UserResponse> response = users.Select(user => _mapper.Map<UserResponse>(user)).ToList();
            return response;
        }

        public async Task<ProfileResponse> Me(Guid id)
        {
            ProfileResponse response = new();

            try
            {
                User? user = await _userRepository.GetById(id);

                if (user == null)
                {
                    response.AddNotification(new Validations.Notification("Seu Usuário não foi encontrado, enctre em contato com supp@email.com"));
                    return response;
                }

                return _mapper.Map<ProfileResponse>(user);
            }
            catch
            {
                response.AddNotification(new Validations.Notification("Servidor indisponível no momento, tente novamente mais tarde"));
                return response;
            }
        }

        public async Task<UserResponse> Update(Guid meId, UpdateProfileRequest request)
        {
            UserResponse response = new();
            User? user = await _userRepository.GetById(meId);

            if (user == null)
            {
                response.AddNotification(new Validations.Notification("Usuário não encontrado"));
                return response;
            }
            try
            {
                user.UpdateProfile(request.Nickname, request.ImageProfile);
                await _userRepository.Update(user);
            } catch
            {
                response.AddNotification(new Validations.Notification("Servidor indisponível no momento, tente novamente mais tarde"));
                return response;
            }
            
            response = _mapper.Map<UserResponse>(user);
            
            return response;
        }
    }
}
