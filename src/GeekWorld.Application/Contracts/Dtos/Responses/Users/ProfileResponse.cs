using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Users
{
    public class ProfileResponse : Notifiable
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Nickname { get;  set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageProfile { get; set; }

        public ProfileResponse(Guid id, string fullName, string email, string nickname, DateTime dateOfBirth, string imageProfile)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            ImageProfile = imageProfile;
        }

        public ProfileResponse()
        {
        }
    }
}
