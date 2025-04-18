namespace RealEase.Application.Dtos.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public int TenantId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
