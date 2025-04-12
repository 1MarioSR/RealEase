using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEase.Domain.Entities
{
    public class Visit
    { 
        [Key]

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; } 
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

    }
}
