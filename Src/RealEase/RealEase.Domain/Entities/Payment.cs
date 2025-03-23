using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEase.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int Contracts_Id { get; set; }
        public int Users_Id { get; set; }
        public decimal Amount { get; set; }

    }
}
