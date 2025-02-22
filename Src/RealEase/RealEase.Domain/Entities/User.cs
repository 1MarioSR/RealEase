using System.ComponentModel.DataAnnotations;

namespace RealEase.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public bool IsActive { get; set; }
        public string Address { get; set; }


    }
}