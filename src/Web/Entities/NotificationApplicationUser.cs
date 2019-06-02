using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenancyContract.Entities;
namespace Web.Entities
{
    public class NotificationApplicationUser
    {
        [Key]
        public int NotificationId  { get; set; }
        [ForeignKey("NotificationId")]
        public Notification Notification {get; set;}
        public string TenantNID {get;set;}
     
        public string HouseOwnerNID {get;set;}
       
        public bool IsRead {get;set;} = false;
        
    }
}