using RealEase.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace RealEase.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
        public bool IsActive { get; set; }


    }
}