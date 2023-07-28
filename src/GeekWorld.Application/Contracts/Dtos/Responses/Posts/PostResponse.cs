using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Posts
{
    public class PostResponse : Notifiable
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Visibility { get; set; }
        public UserResponse Author { get; set; }
        public DateTime CreatedAt { get; set; }

        public PostResponse(Guid id,string content, string visibility, UserResponse author, DateTime createdAt)
        {
            Id = id;
            Content = content;
            Visibility = visibility;
            Author = author;
            CreatedAt = createdAt;
        }

        public PostResponse()
        {
        }
    }
}
