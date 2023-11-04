using MongoDB.Bson.Serialization.Attributes;

namespace MovieHub.Types.User
{
    public class CreateUserInput
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
