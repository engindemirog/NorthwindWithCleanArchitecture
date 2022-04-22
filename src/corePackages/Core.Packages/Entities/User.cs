using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

        public List<UserOperationClaim> UserOperationClaims { get; set; }

        public User()
        {
            UserOperationClaims = new List<UserOperationClaim>();
        }
    }
}
