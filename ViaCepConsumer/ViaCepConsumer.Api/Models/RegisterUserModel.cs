using System.ComponentModel.DataAnnotations;

namespace ViaCepConsumer.Api.Models
{
    public class RegisterUserModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must contain 3 to 100 characters.")]
        public string Username { get; private set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email must contain less than 100 characters.")]
        public string Email { get; private set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must contain at least 8 characters, and less than 100 characters.")]
        public string Password { get; private set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must contain at least 8 characters, and less than 100 characters.")]
        public string ConfirmPassword { get; private set; }

        public RegisterUserModel(string username, string email, string password, string confirmPassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public bool IsValid()
            => string.Equals(Password, ConfirmPassword);
    }
}
