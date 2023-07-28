namespace GeekWorld.Domain.Models
{
    public class LikePost
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool Value { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Guid PostId { get; private set; }
        public Post Post { get; private set; }

        public LikePost() { }

        public LikePost(bool value, Guid userId, User user, Guid postId, Post post)
        {
            Value = value;
            UserId = userId;
            User = user;
            PostId = postId;
            Post = post;
        }


        public void AddLikePost(Post post, User author)
        {
            Post = post;
            User = author;
            PostId = post.Id;
            UserId = author.Id;
        }
    }
}
