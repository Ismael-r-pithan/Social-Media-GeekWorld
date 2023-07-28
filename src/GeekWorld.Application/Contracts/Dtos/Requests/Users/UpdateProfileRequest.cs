namespace GeekWorld.Application.Contracts.Dtos.Requests.Users
{
    public class UpdateProfileRequest
    {
        public string ImageProfile { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;

        public UpdateProfileRequest(string imageProfile, string nickname)
        {
            ImageProfile = imageProfile;
            Nickname = nickname;
        }
    }
}
