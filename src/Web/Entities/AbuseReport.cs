using System;
using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Entities
{
    public class AbuseReport
    {
   
        public int Id { get; set; }
      
        public string SenderId { get; set; }
    
        public string ReportText { get; set; }
      
        public DateTime ReportTime { get; set; } 
    }
}