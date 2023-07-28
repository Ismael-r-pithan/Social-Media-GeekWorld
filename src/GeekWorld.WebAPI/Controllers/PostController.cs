using GeekWorld.Application.Contracts.Dtos.Responses.Comments;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GeekWorld.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public PostController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("posts/{id:guid}/comments")]
        [Authorize]
        public async Task<ActionResult<CommentResponse>> AddComment([FromRoute] Guid id)
        {
            var comments = await _commentService.GetCommentsByPost(id);

            return Ok(comments);
        }
    }
}
