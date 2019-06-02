using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyContract.Models
{
    public class CPaperOneModel
    {
        public string Id { get; set; }
        public string TenantName { get; set; }
        public string HouseOwnerName { get; set; }
        public string HouseOwnerId { get; set; }
        public string HouseId { get; set; }
        public string TenantNID { get; set; }
        public string TenantId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime AgreementDate { get; set; }
        [Required]
        public int Advance { get; set; }
        [Required]
        public int GasBill { get; set; }
        [Required]
        public int ElectricityBill { get; set; }
        [Required]
        public int MaintainCost {get;set;}
        public int PayTime {get;set;}
        [Required]
        public int IncreaseRate {get;set;}
        [Required]
        public DateTime IncreaseTime {get;set;}
    }
}
