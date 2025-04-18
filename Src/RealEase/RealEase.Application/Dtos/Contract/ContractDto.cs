namespace RealEase.Application.Dtos.Contract
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyAmount { get; set; }
        public string Status { get; set; }

    }
}
