namespace GeekWorld.Domain.Models
{
    public class Post
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Content { get; private set; }
        public string Visibility { get; private set; }  
        public DateTime CreatedAt { get; private set; }
        public DateTime DeletedAt { get; private set; }

        public Guid AuthorId { get; private set; }
        public User Author { get; private set; }


        public List<LikePost> LikePosts { get; private set; }
        public List<Comment> Comments { get; private set; }
        public Post() { }

        public Post(string content, string visibility, DateTime deletedAt, Guid authorId, User author)
        {
            Content = content;
            Visibility = visibility;
            CreatedAt = DateTime.Now;
            DeletedAt = deletedAt;
            AuthorId = authorId;
            Author = author;
            LikePosts = new List<LikePost>();
            Comments = new List<Comment>();
        }

        public void AddAuthor(User author)
        {
            Author = author;
            AuthorId = author.Id;
        }
    }
}
