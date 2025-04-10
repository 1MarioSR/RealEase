using System.ComponentModel.DataAnnotations;

namespace RealEase.API.Dtos
{
    public class PropertieDto
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public decimal Price { get; set; }
            public string PropertyType { get; set; }
            public string Status { get; set; }
            public int OwnerId { get; set; }
    }

}

