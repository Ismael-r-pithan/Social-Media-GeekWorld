using GeekWorld.Application.Contracts.Dtos.Requests.Comments;
using GeekWorld.Application.Contracts.Dtos.Requests.Friendships;
using GeekWorld.Application.Contracts.Dtos.Requests.Posts;
using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Friendship;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Application.Validations;
using GeekWorld.WebAPI.Commons;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeekWorld.WebAPI.Controllers
{
    [Route("api/me")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly IMeService _meService;
        private readonly IPostService _postService;
        private readonly IFriendshipService _friendshipService;
        private readonly ICommentService _commentService;

        public MeController(IMeService meService, IPostService postService, IFriendshipService friendshipService, ICommentService commentService)
        {
            _meService = meService;
            _postService = postService;
            _friendshipService = friendshipService;
            _commentService = commentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProfileResponse>> Me()
        {
            ProfileResponse response = new();

            Guid userId = Guid.Parse(User.FindFirstValue("Id"));

            response = await _meService.Me(userId);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("posts")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> Index()
        {
            Guid userId = Guid.Parse(User.FindFirstValue("Id"));

            IEnumerable<PostResponse> response = await _postService.GetAll(userId, 0, 10);

            return Ok(response);
        }

        [HttpPost]
        [Route("posts")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> SignUp([FromBody] CreatePostRequest request)
        {
            Guid authorId = Guid.Parse(User.FindFirstValue("Id"));

            PostResponse response = await _postService.Add(request, authorId);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("posts/{id:guid}/comments")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> AddComment([FromBody] CreateCommentRequest request, [FromRoute] Guid id)
        {
            Guid authorId = Guid.Parse(User.FindFirstValue("Id"));

            await _commentService.AddCommentOnPost(request,id, authorId);

            return Ok();
        }

        [HttpPost]
        [Route("posts/{id:guid}/likes")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> LikePost([FromBody] AddLikePostRequest request, [FromRoute] Guid id)
        {
            Guid authorId = Guid.Parse(User.FindFirstValue("Id"));

            var response = await _postService.AddLikePost(authorId, id, request );

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("friendships")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetFriendships([FromQuery] Pageable pageable)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));

            return Ok(await _meService.GetAllFriends(meId, pageable.Page, pageable.Limit, pageable.Search));
        }

        [HttpGet]
        [Route("friendships/requests")]
        [Authorize]
        public async Task<ActionResult<FriendshipResponse>> FriendshipRequests()
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));

            return Ok(await _friendshipService.GetAllRequests(meId));
        }

        [HttpPost]
        [Route("friendships")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> RequestFriendship([FromBody] FriendshipRequest request)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));

            FriendshipResponse response = await _friendshipService.FriendshipRequest(meId, request);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<UserResponse>> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));

            UserResponse response = await _meService.Update(meId, request);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("friendships")]
        [Authorize]
        public async Task<ActionResult<PostResponse>> ResponseFriendship([FromBody] FriendshipInteractionRequest request)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));

            FriendshipResponse response = await _friendshipService.FriendshipResponse(meId, request);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }
    }
}
