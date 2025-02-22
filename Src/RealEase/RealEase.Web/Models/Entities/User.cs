using System.ComponentModel.DataAnnotations;

namespace RealEase.Web.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public string address { get; set; }


    }
}