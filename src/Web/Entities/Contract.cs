using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenancyContract.Entities
{
    public class Contract
    {
   
        public string Id { get; set; }
 
        public string HouseOwnerId { get; set; }
        [ForeignKey("HouseOwnerId")]
        public HouseOwner houseOwner { get; set; }
        
        public string HouseId { get; set; }
        [ForeignKey("HouseId")]
        public House House { get; set; }
   
        public string TenantId { get; set; }
        [ForeignKey("TenantId")]
        public Tenant tenant { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AgreementDate { get; set; }
        public int Advance {get;set;}
        public int GasBill {get;set;}
        public int ElectricityBill {get; set;}
        public string TenantNID { get; set; }
        public string HouseOwnerNID { get; set; }
        public int MaintainCost {get;set;}
        public int IncreaseRate {get;set;}
        public DateTime IncreaseTime {get;set;}
        public bool AcceptTenant {get;set;}
        public bool AcceptHO {get;set;}
         
    }
}