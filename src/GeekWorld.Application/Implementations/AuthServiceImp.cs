using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Implementations
{
    public class AuthServiceImp : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthServiceImp(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService; 
        }

        public async Task<UserResponse> Login(LoginUserRequest request)
        {
            UserResponse response = new();

            try
            {
                User? user = await _userRepository.Login(request.Email, request.Password);

                if (user == null)
                {
                    response.AddNotification(new Validations.Notification("E-mail e/ou Senha incorretos"));
                    return response;
                }

                response = _mapper.Map<UserResponse>(user);

                return response;
            }
            catch
            {
                response.AddNotification(new Validations.Notification("Servidor indisponível no momento, tente novamente mais tarde"));
                return response;
            }
        }

        public async Task<UserResponse> SignUp(CreateUserRequest request)
        {
            bool emailAlreadyExists = await _userService.ValidateEmailAlreadyExists(request.Email);

            UserResponse response = new();

            if (emailAlreadyExists)
            {
                response.AddNotification(new Validations.Notification("E-mail já cadastrado"));
                return response;
            }

            User user = _mapper.Map<User>(request);

            try
            {
                await _userRepository.Add(user);
                response = _mapper.Map<UserResponse>(user);

                return response;
            }
            catch
            {
                response.AddNotification(new Validations.Notification("Servidor indisponível no momento, tente novamente mais tarde"));
                return response;

            }
        }
    }
}
