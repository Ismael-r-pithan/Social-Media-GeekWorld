using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Friendships;
using GeekWorld.Application.Contracts.Dtos.Responses.Friendship;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Enums;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Implementations
{
    public class FriendshipServiceImp : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FriendshipServiceImp(IFriendshipRepository friendshipRepository, IMapper mapper, IUserRepository userRepository)
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<FriendshipResponse> FriendshipRequest(Guid meId, FriendshipRequest request)
        {
            FriendshipResponse response = new();
            bool validFriendRequest = await _friendshipRepository.ValidFriendRequest(meId, request.FriendId);

            if (!validFriendRequest)
            {
                response.AddNotification(new Validations.Notification("Amizade ou solicitação de amizade já existe"));
                return response;
            }

            if (meId == request.FriendId)
            {
                response.AddNotification(new Validations.Notification("Você não pode enviar solicitações para si mesmo"));
                return response;
            }

            User? me = await _userRepository.GetById(meId);

            if (me == null)
            {
                response.AddNotification(new Validations.Notification("Seu usuário não foi encontrado, entre em contato com supp@email.com"));
                return response;
            }

            User? friend = await _userRepository.GetById(request.FriendId);
            if (friend == null)
            {
                response.AddNotification(new Validations.Notification("O usuário que você solicitou amizade não está mais na plataforma"));
                return response;
            }

            Friendship friendship = _mapper.Map<Friendship>(request);

            friendship.FriendshipInteraction(me, friend, StatusFriendship.PENDING.ToString());

            await _friendshipRepository.FriendshipRequest(friendship);

            return _mapper.Map<FriendshipResponse>(friendship);
        }

        public async Task<FriendshipResponse> FriendshipResponse(Guid meId, FriendshipInteractionRequest request)
        {
            FriendshipResponse response = new();
            Friendship? friendship = await _friendshipRepository.GetById(request.FriendshipId);

            if (friendship == null)
            {
                response.AddNotification(new Validations.Notification("Solicitação de amizade não encontrada"));
                return response;
            }
            if (friendship.UserId == meId)
            {
                response.AddNotification(new Validations.Notification("Você não pode aceitar suas próprias solicitações"));
                return response;
            }

            User? me = await _userRepository.GetById(meId);

            if (me == null)
            {
                response.AddNotification(new Validations.Notification("Seu usuário não foi encontrado, entre em contato com supp@email.com"));
                return response;
            }

            User? friend = await _userRepository.GetById(friendship.FriendId);
            if (friend == null)
            {
                response.AddNotification(new Validations.Notification("O usuário que solicitou a amizade não está mais na plataforma"));
                return response;
            }

            friendship.FriendshipResponse(request.Status);

            await _friendshipRepository.FriendshipResponse(friendship);

            return _mapper.Map<FriendshipResponse>(friendship);
        }

        public async Task<IEnumerable<FriendshipResponse>> GetAllRequests(Guid meId)
        {
            var requests = await _friendshipRepository.GetAllFriendshipRequests(meId);

            IEnumerable<FriendshipResponse> response = requests.Select(friendship => _mapper.Map<FriendshipResponse>(friendship));

            return response;
        }

        public async Task<string?> GetFriendshipStatus(Guid meId, Guid userId)
        {
            string? status = await _friendshipRepository.GetFriendshipStatus(meId, userId);
            if (status == null)
            {
                return StatusFriendship.NONE.ToString();
            }

            return status;
        }

        public Task<FriendshipResponse> ValidFriendRequest(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
