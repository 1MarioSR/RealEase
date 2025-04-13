using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEase.Infrastructure.Models
{
    internal class ContractModel
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
