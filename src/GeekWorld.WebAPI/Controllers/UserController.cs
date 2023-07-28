using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.WebAPI.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeekWorld.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IFriendshipService _friendshipService;

        public UserController(IUserService userService, IPostService postService, IFriendshipService friendshipService)
        {
            _userService = userService;
            _postService = postService;
            _friendshipService = friendshipService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<UserResponse> FindAll([FromQuery] Pageable pageable)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));
            return Ok(_userService.GetAll(meId, pageable.Page, pageable.Limit, pageable.Search));
        }

        [HttpGet("{id:guid}/status_friendship")]
        [Authorize]
        public ActionResult<UserResponse> FindStatusFriendship([FromRoute] Guid id)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));
            return Ok(_friendshipService.GetFriendshipStatus(meId, id));
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public ActionResult<UserResponse> FindUser([FromRoute] Guid id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpGet("{id:guid}/posts")]
        [Authorize]
        public ActionResult<PostResponse> FindPostsUser([FromRoute] Guid id, [FromQuery] Pageable pageable)
        {
            Guid meId = Guid.Parse(User.FindFirstValue("Id"));
            return Ok(_postService.GetPostsByUser(meId, id, pageable.Page, pageable.Limit));
        }



    }
}
