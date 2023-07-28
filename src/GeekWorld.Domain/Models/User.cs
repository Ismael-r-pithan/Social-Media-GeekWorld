namespace GeekWorld.Domain.Models
{
    public class User
    {

        public Guid Id { get; init; } = Guid.NewGuid();
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? Nickname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CEP { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string? ImageProfile { get; private set; }

        public List<Post> Posts { get; private set; } = new List<Post>();
        public List<LikePost> LikePosts { get; private set; } = new List<LikePost>();
        public List<Friendship> Friends { get; private set; } = new List<Friendship>();
        public List<Comment> Comments { get; private set; } = new List<Comment>();

        public User()
        {
        }
        public User(string fullName, string email, string nickname, DateTime dateOfBirth, string cep, string password, string imageProfile)
        {
            FullName = fullName;
            Email = email;
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            CEP = cep;
            Password = EncryptPassword(password);
            ImageProfile = imageProfile;
            Posts = new List<Post>();
            LikePosts = new List<LikePost>();
            Friends = new List<Friendship>();
            Comments = new List<Comment>();
        }

        public void UpdateProfile(string nickname, string imageProfile)
        {
            Nickname = nickname;
            ImageProfile = imageProfile;
        }

        private static string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void HidePassword()
        {
            Password = "";
        }

    }
}
