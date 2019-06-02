using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Entities
{
    public class LogInInfo
    {
   
        public string Id { get; set; }
   
        public string Password { get; set; }
  
        public string UserType { get; set; }     
  
        public string UserStatus { get; set; }
    }
}