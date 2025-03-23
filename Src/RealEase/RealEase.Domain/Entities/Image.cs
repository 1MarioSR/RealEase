using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEase.Domain.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public int Properties_Id { get; set; }
        public string Image_url { get; set; }
        

    }
}
