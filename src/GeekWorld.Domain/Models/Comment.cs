namespace GeekWorld.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public Guid AuthorId { get ; private set; }
        public User Author { get; private set; }

        public Guid PostId { get; private set; }
        public Post Post { get; private set; }

        public Comment()
        {
        }

        public Comment(string content, DateTime createdAt, Guid authorId, User author, Guid postId, Post post)
        {
            Content = content;
            CreatedAt = createdAt;
            AuthorId = authorId;
            Author = author;
            PostId = postId;
            Post = post;
        }

        public void CommentPost(User author, Post post)
        {
            Author = author;
            Post = post;
            AuthorId = author.Id;
            PostId = post.Id;
        }

        public void AddContent(string content)
        {
            Content = content;
        }
    }
}
