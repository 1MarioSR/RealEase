using System.ComponentModel.DataAnnotations;

namespace RealEase.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
        public bool IsActive { get; set; }


    }
}