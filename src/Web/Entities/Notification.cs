using System.ComponentModel.DataAnnotations;

namespace Web.Entities
{
    public class Notification
    {
        
        [Key]
        public int Id { get; set; }
        public string  Text { get; set; }
    }
}