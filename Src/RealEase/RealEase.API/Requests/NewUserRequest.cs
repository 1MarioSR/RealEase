using System.ComponentModel.DataAnnotations;

namespace RealEase.API.Requests
{
    public class NewUserRequest
    {
     
            public string Name { get; set; }
            public string Lastname { get; set; }
            public bool IsActive { get; set; }
            public string Address { get; set; }
           
     }

}



