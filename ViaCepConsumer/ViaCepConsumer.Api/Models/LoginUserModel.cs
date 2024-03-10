using System.ComponentModel.DataAnnotations;

namespace ViaCepConsumer.Api.Models
{
    public class LoginUserModel
    {
        [Required]
        public string Username { get; private set; }

        [Required]
        public string Password { get; private set; }

        public LoginUserModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
