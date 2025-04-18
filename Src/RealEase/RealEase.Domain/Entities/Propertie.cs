using RealEase.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace RealEase.Domain.Entities
{
    public class Propertie : BaseEntity
    {
            [Required]
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            [Required]
            public string Address { get; set; }
            public decimal Price { get; set; }
            public string PropertyType { get; set; }
            public string Status { get; set; }
            public int OwnerId { get; set; }

    }
}
