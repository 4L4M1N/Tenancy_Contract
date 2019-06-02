using System;
using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Entities
{
    public class Review
    {
    
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewTime { get; set; } 
    }
}