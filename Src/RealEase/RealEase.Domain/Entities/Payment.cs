using RealEase.Domain.Core;

namespace RealEase.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int ContractId { get; set; }
        public int TenantId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }

    }
}
