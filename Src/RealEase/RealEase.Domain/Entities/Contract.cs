using RealEase.Domain.Core;

namespace RealEase.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyAmount { get; set; }
        public string Status { get; set; }

    }
}
