using System;
using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Entities
{
    public class Message
    {
   
        public string Id { get; set; }
   
        public string SenderId { get; set; }
  
        public string ReceiverId { get; set; }
     
        public string MessageText { get; set; }
    
        public DateTime MessageTime { get; set; }        

    }
}