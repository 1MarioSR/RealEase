using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEase.Domain.Entities
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public int Properties_Id { get; set; }
        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set; }

        public decimal Total_amount { get; set; }

    }
}
