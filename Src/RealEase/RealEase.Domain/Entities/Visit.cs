using RealEase.Domain.Core;

namespace RealEase.Domain.Entities
{
    public class Visit : BaseEntity
    { 
        public int PropertyId { get; set; }
        public int UserId { get; set; } 
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

    }
}
