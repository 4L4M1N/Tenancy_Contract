using System;
using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Entities
{
    public class Moderator
    {
       
        public string Id { get; set; }
    
        public string Name { get; set; }
        public string Image { get; set; }
 
        public string Gender { get; set; }
   
        public string AddressDivision { get; set; }
 
        public string AddressDistrict { get; set; }
   
        public string AddressThana { get; set; }

        public string AddressPO { get; set; }

        public string AddressVillage { get; set; }
        public string AddressRoad { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NID { get; set; }

        public DateTime DOB { get; set; }
        public string PasswordHash {get;set;}
    }
}