using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Users;

public class UserResponse : Notifiable
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ImageProfile { get; set; }

    public UserResponse(Guid id, string fullName, string email, DateTime dateOfBirth, string imageProfile)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        DateOfBirth = dateOfBirth;
        ImageProfile = imageProfile;
    }

    public UserResponse()
    {
    }
}
