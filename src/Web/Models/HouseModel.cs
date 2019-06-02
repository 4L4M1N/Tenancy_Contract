using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyContract.Models
{
    public class HouseModel
    {
      
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int ThanaId { get; set; }
        
        [Required]
        public string DagNo { get; set; }
        [Required]
        public string AddressRoad { get; set; }
        [Required]
        public string AddressVillage { get; set; }
        [Required]
        public string HouseNo { get; set; }
        public string AddressPO { get; set; }
        public string CityCorporation { get; set; }
    }
}
