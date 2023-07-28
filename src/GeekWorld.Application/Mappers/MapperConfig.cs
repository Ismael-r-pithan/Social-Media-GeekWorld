using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Comments;
using GeekWorld.Application.Contracts.Dtos.Requests.Friendships;
using GeekWorld.Application.Contracts.Dtos.Requests.Posts;
using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Comments;
using GeekWorld.Application.Contracts.Dtos.Responses.Friendship;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Domain.Models;


namespace GeekWorld.Application.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserResponse>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, ProfileResponse>();

            CreateMap<CreatePostRequest, Post>();
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            CreateMap<FriendshipRequest, Friendship>();
            CreateMap<Friendship, FriendshipResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            CreateMap<AddLikePostRequest, LikePost>();
            CreateMap<LikePost, LikePostResponse>();
        }
    }
}
