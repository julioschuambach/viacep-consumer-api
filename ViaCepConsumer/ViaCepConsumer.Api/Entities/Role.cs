using System.Text.Json.Serialization;

namespace ViaCepConsumer.Api.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }

        [JsonIgnore]
        public IList<User> Users { get; private set; }

        public Role(string name)
        {
            Name = name;
            Users = new List<User>();
        }
    }
}
