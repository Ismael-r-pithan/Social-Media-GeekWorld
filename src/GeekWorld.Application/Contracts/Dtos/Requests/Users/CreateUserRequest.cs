using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Users
{
    public class CreateUserRequest
    {
        public CreateUserRequest(string fullName, string email, string nickname, DateTime dateOfBirth, string cep, string password, string imageProfile)
        {
            FullName = fullName;
            Email = email;
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            CEP = cep;
            Password = password;
            ImageProfile = imageProfile;
        }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O E-mail deve ser válido")]
        public string Email { get; set; }

        public string? Nickname { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string CEP { get; set; }


        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        public string? ImageProfile { get; set; }

    }
}
