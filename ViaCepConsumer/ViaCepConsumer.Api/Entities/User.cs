using ViaCepConsumer.Api.Models;

namespace ViaCepConsumer.Api.Entities
{
    public class User : EntityBase
    {
        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; private set; } = DateTime.Now;
        public IList<Role> Roles { get; private set; } = new List<Role>();

        public User() { }

        public User(RegisterUserModel model, string encryptedPassword)
        {
            Username = model.Username;
            Email = model.Email;
            Password = encryptedPassword;
        }
    }
}
