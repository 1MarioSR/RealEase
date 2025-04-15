namespace RealEase.Application.Dtos.Visit
{
    public class VisitDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

    }
}
