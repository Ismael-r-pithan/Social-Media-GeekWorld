using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Users
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O E-mail deve ser válido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }


        public LoginUserRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
